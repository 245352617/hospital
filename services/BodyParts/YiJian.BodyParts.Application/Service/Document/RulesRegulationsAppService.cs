using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.EntityFrameworkCore;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.IService;
using YiJian.BodyParts.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using Minio;
using Serilog;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;

using Microsoft.EntityFrameworkCore;
using ICIS.Extras.DatabaseAccessor.Minio.Repositories;
using System.Linq.Expressions;
using ICIS.Extras.DatabaseAccessor.Minio.Model;

namespace YiJian.BodyParts.Service
{
    /// <summary>
    /// 上传下载图片
    /// </summary>
    [DbDescription("上传下载图片")]
    public class RulesRegulationsAppService : ApplicationService, IRulesRegulationsAppService
    {
        private readonly IGuidGenerator _guidGenerator;

        private readonly IFileRecordRepository _FileRecordRepository;


        private readonly IMinioRepository _minioRepository;

        private static readonly Lazy<IDictionary<string, string>> _contentTypeMap = new Lazy<IDictionary<string, string>>(AddContentTypeMappings);

        public RulesRegulationsAppService(IGuidGenerator guidGenerator, IFileRecordRepository FileRecordRepository, IMinioRepository minioRepository)
        {
            _guidGenerator = guidGenerator;
            _FileRecordRepository = FileRecordRepository;
            _minioRepository = minioRepository;
        }

        /// <summary>
        /// 上传多个文件上传，注意：不支持txt文件
        /// </summary>
        /// <param name="files">图片、PDF、world文档</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="PI_ID">患者ID</param>
        /// <param name="moduleType">regulation = ，literatur = 文献，skinpicture = 皮肤图片</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> BatchUpload(List<IFormFile> files, string deptCode, string PI_ID, string moduleType)
        {
            try
            {
                #region 文件名判重
                string errMsg = string.Empty;
                foreach (IFormFile file in files)
                {
                    var fileName = await _FileRecordRepository.Where(s => s.FileName == file.FileName && s.IsDel == false && s.BucketName == moduleType && s.ModuleType == moduleType)
                    .WhereIf(!string.IsNullOrWhiteSpace(deptCode), s => s.DeptCode == deptCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(PI_ID), s => s.PI_ID == PI_ID).ToListAsync();

                    if (fileName != null && fileName.Count() > 0)
                    {
                        errMsg += $"{file.FileName};";
                    }
                }
                if (errMsg != string.Empty)
                {
                    return JsonResult.Fail(msg: $"{errMsg}文件已存在");
                }
                #endregion

                foreach (IFormFile file in files)
                {
                    string buckName = moduleType;
                    var exists = await _minioRepository.BucketExistsAsync(buckName);
                    if (!exists)
                    {
                        // 2.2 创建文件桶，这里暂时先定义一个桶
                        await _minioRepository.MakeBucketAsync(buckName);
                    }

                    byte[] buffer = new byte[file.Length];
                    file.OpenReadStream().Read(buffer, 0, buffer.Length);
                    System.IO.MemoryStream filestream = new System.IO.MemoryStream(buffer);
                    string Init = Guid.NewGuid().ToString();
                    await _minioRepository.PutObjectAsync(new PutObjectDto()
                    {
                        BucketName = buckName,
                        ObjectName = $"{deptCode}{PI_ID}{ file.FileName}",
                        StreamSize = filestream.Length,
                        DataStream = filestream,
                        ContentType = GetContentType_New(file.FileName),
                        MetaData = null
                    });
                    // await _minioRepository.PutObjectAsync(buckName, file.FileName, filestream,
                    // filestream.Length, GetContentType_New(file.FileName)/*"application/octet-stream"*/);/*如果使用application/octet-stream为默认参数时，部分类型文件是没有办法预览的，所以必须要拿到文件对应的文件类型方可实现图片、PDF预览*/

                    var fileUrl = _minioRepository.PresignedGetObjectAsync(buckName, file.FileName/*, 60*/);

                    #region 上传信息写入库表

                    FileRecord fileInfo = new FileRecord(_guidGenerator.Create())
                    {
                        FileName = file.FileName,
                        FileSuffix = Path.GetExtension(file.FileName),
                        BucketName = buckName,
                        UploadTime = DateTime.Now,
                        DeptCode = deptCode,
                        PI_ID = PI_ID,
                        IsDel = false,
                        DelTime = null,
                        Size = file.Length,
                        ModuleType = moduleType
                    };

                    await _FileRecordRepository.InsertAsync(fileInfo);

                    #endregion
                }

                return JsonResult.Ok(msg: "上传成功");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return JsonResult.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 上传单个文件，注意：不支持txt文件
        /// </summary>
        /// <param name="file">图片、PDF、world文档</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="moduleType">regulation = ，literatur = 文献，skinpicture = 皮肤图片</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Upload(IFormFile file, string deptCode, string moduleType)
        {
            try
            {
                #region 文件名判重

                var fileName = await _FileRecordRepository.Where(s => s.FileName == file.FileName && s.IsDel == false && s.BucketName == moduleType && s.ModuleType == moduleType)
                    .WhereIf(!string.IsNullOrWhiteSpace(deptCode), s => s.DeptCode == deptCode).ToListAsync();
                if (fileName != null && fileName.Count() > 0)
                {
                    return JsonResult.Fail(data: file, msg: "文件名称已存在！");
                }

                #endregion

                string buckName = moduleType;
                var exists = await _minioRepository.BucketExistsAsync(buckName);
                if (!exists)
                {
                    // 2.2 创建文件桶，这里暂时先定义一个桶
                    await _minioRepository.MakeBucketAsync(buckName);
                }

                byte[] buffer = new byte[file.Length];
                file.OpenReadStream().Read(buffer, 0, buffer.Length);
                System.IO.MemoryStream filestream = new System.IO.MemoryStream(buffer);
                string Init = Guid.NewGuid().ToString();
                await _minioRepository.PutObjectAsync(new PutObjectDto()
                {
                    BucketName = buckName,
                    ObjectName = $"{deptCode}{ file.FileName}",
                    StreamSize = filestream.Length,
                    DataStream = filestream,
                    ContentType = GetContentType_New(file.FileName),
                    MetaData = null
                });
                // await _minioRepository.PutObjectAsync(buckName, file.FileName, filestream,
                // filestream.Length, GetContentType_New(file.FileName)/*"application/octet-stream"*/);/*如果使用application/octet-stream为默认参数时，部分类型文件是没有办法预览的，所以必须要拿到文件对应的文件类型方可实现图片、PDF预览*/

                var fileUrl = _minioRepository.PresignedGetObjectAsync(buckName, file.FileName/*, 60*/);

                #region 上传信息写入库表

                FileRecord fileInfo = new FileRecord(_guidGenerator.Create())
                {
                    FileName = file.FileName,
                    FileSuffix = Path.GetExtension(file.FileName),
                    BucketName = buckName,
                    UploadTime = DateTime.Now,
                    DeptCode = deptCode,
                    IsDel = false,
                    DelTime = null,
                    Size = file.Length,
                    ModuleType = moduleType
                };

                await _FileRecordRepository.InsertAsync(fileInfo);

                #endregion

                return JsonResult.Ok(msg: "上传成功");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return JsonResult.Fail(msg: ex.Message);
            }
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleType">regulation = ，literatur = 文献，skinpicture = 皮肤图片</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<string>> Download(string deptCode, string fileName, string moduleType)
        {
            try
            {
                //FileStreamResult fileStreamResult = null;
                // 1、创建MioIO客户端
                //MinioClient minioClient = new MinioClient("127.0.0.1:9000", "minioadmin", "minioadmin");

                //var fileStream = new MemoryStream();

                // 2、下载图片
                //minioClient.GetObjectAsync(buckName, fileName, stream => stream.CopyTo(fileStream)).Wait();
                //fileStreamResult = new FileStreamResult(fileStream, GetContentType_New(fileName));// 新版本

                var fileUrl = await _minioRepository.PresignedGetObjectAsync(moduleType, $"{deptCode}{fileName}");

                //fileStream.Position = 0;

                return JsonResult<string>.Ok(data: $"{fileUrl}#view=FitH,top#scrollbars=0&toolbar=0&statusbar=0");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return JsonResult<string>.Fail(msg: ex.Message);
            }
        }


        /// <summary>
        /// 批量下载文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="PI_ID">患者ID</param>
        /// <param name="moduleType">regulation = ，literatur = 文献，skinpicture = 皮肤图片</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<SkinPictureDto>>> BatchDownload(string deptCode, string PI_ID, string moduleType)
        {
            try
            {
                List<RulesRegulationsFileListDto> fileInfos = new List<RulesRegulationsFileListDto>();
                var fileInfoList = await _FileRecordRepository.Where(s => s.IsDel == false && s.BucketName == moduleType)
                    .WhereIf(!string.IsNullOrWhiteSpace(deptCode), s => s.DeptCode == deptCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(PI_ID), s => s.PI_ID == PI_ID).ToListAsync();

                //按日期分组
                List<SkinPictureDto> skinPictures = new List<SkinPictureDto>();
                List<DateTime> uploadTimes = fileInfoList.Select(x => x.UploadTime.Date).Distinct().ToList();
                foreach (DateTime ti in uploadTimes)
                {
                    SkinPictureDto skinPicture = new SkinPictureDto();
                    skinPicture.UploadTime = ti.ToString("yyyy-MM-dd");
                    var files = fileInfoList.Where(x => x.UploadTime.Date == ti).ToList();

                    List<SkinPictureItem> pictureItems = new List<SkinPictureItem>();
                    foreach (var fi in files)
                    {
                        SkinPictureItem pictureItem = new SkinPictureItem();
                        pictureItem.FileName = fi.FileName;

                        var fileUrl = await _minioRepository.PresignedGetObjectAsync(moduleType, $"{deptCode}{PI_ID}{fi.FileName}");
                        pictureItem.File = $"{fileUrl}#view=FitH,top#scrollbars=0&toolbar=0&statusbar=0";

                        pictureItems.Add(pictureItem);
                    }

                    skinPicture.pictureItems = pictureItems;
                    skinPictures.Add(skinPicture);
                }
                return JsonResult<List<SkinPictureDto>>.Ok(data: skinPictures);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return JsonResult<List<SkinPictureDto>>.Fail(msg: ex.Message);
            }
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="PI_ID">患者ID</param>
        /// <param name="moduleType">regulation = ，literatur = 文献，skinpicture = 皮肤图片</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DelFile(string deptCode, string PI_ID, string fileName, string moduleType)
        {
            try
            {
                // 1、创建MioIO客户端
                //MinioClient minioClient = new MinioClient("127.0.0.1:9000", "minioadmin", "minioadmin");

                // 2、删除
                await _minioRepository.RemoveObjectAsync(moduleType, $"{deptCode}{PI_ID}{fileName}");

                #region 删除文件记录数据

                var fileInfo = await _FileRecordRepository.Where(s => s.FileName == fileName && s.BucketName == moduleType)
                    .WhereIf(!string.IsNullOrWhiteSpace(deptCode), s => s.DeptCode == deptCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(PI_ID), s => s.PI_ID == PI_ID).ToListAsync();

                if (fileInfo != null && fileInfo.Count > 0)
                {
                    foreach (var item in fileInfo)
                    {
                        item.IsDel = true;
                        item.DelTime = DateTime.Now;
                    }
                    await _FileRecordRepository.UpdateRangeAsync(fileInfo);
                }

                #endregion 删除文件记录数据

                return JsonResult.Ok("删除成功");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return JsonResult.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 获取图片列表
        /// </summary>
        /// <param name="deptCode">文件名称</param>
        /// <param name="PI_ID">患者ID</param>
        /// <param name="fileName"></param>
        /// <param name="moduleType">regulation = ，literatur = 文献，skinpicture = 皮肤图片</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<RulesRegulationsFileListDto>>> SelectFileList(string deptCode, string PI_ID, string fileName, string moduleType)
        {
            try
            {
                //多条件拼接扩展
                Expression<Func<FileRecord, bool>> predicatePR = PredicateBuilder.True<FileRecord>();
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    predicatePR = predicatePR.And(s => s.IsDel == false && s.BucketName == moduleType);
                }
                else
                {
                    predicatePR = predicatePR.And(s => s.FileName.Contains(fileName) && s.IsDel == false && s.BucketName == moduleType);
                }


                List<RulesRegulationsFileListDto> fileInfos = new List<RulesRegulationsFileListDto>();
                var fileInfoList = await _FileRecordRepository.Where(predicatePR)
                    .WhereIf(!string.IsNullOrWhiteSpace(deptCode), s => s.DeptCode == deptCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(PI_ID), s => s.PI_ID == PI_ID).ToListAsync();

                ObjectMapper.Map(fileInfoList, fileInfos);

                return JsonResult<List<RulesRegulationsFileListDto>>.Ok(data: fileInfos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return JsonResult<List<RulesRegulationsFileListDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 获取文件后缀
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static string GetContentType_New(string fileName)
        {
            string extension = null;
            try
            {
                extension = Path.GetExtension(fileName);
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(extension))
            {
                return "application/octet-stream";
            }

            return _contentTypeMap.Value.TryGetValue(extension, out string contentType)
                ? contentType
                : "application/octet-stream";
        }

        /// <summary>
        /// 根据后缀获取对应的contentType字符串
        /// </summary>
        /// <returns></returns>
        private static IDictionary<string, string> AddContentTypeMappings()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {".323", "text/h323"},
                {".3g2", "video/3gpp2"},
                {".3gp", "video/3gpp"},
                {".3gp2", "video/3gpp2"},
                {".3gpp", "video/3gpp"},
                {".7z", "application/x-7z-compressed"},
                {".aa", "audio/audible"},
                {".AAC", "audio/aac"},
                {".aax", "audio/vnd.audible.aax"},
                {".ac3", "audio/ac3"},
                {".accda", "application/msaccess.addin"},
                {".accdb", "application/msaccess"},
                {".accdc", "application/msaccess.cab"},
                {".accde", "application/msaccess"},
                {".accdr", "application/msaccess.runtime"},
                {".accdt", "application/msaccess"},
                {".accdw", "application/msaccess.webapplication"},
                {".accft", "application/msaccess.ftemplate"},
                {".acx", "application/internet-property-stream"},
                {".AddIn", "text/xml"},
                {".ade", "application/msaccess"},
                {".adobebridge", "application/x-bridge-url"},
                {".adp", "application/msaccess"},
                {".ADT", "audio/vnd.dlna.adts"},
                {".ADTS", "audio/aac"},
                {".ai", "application/postscript"},
                {".aif", "audio/aiff"},
                {".aifc", "audio/aiff"},
                {".aiff", "audio/aiff"},
                {".air", "application/vnd.adobe.air-application-installer-package+zip"},
                {".amc", "application/mpeg"},
                {".anx", "application/annodex"},
                {".apk", "application/vnd.android.package-archive" },
                {".application", "application/x-ms-application"},
                {".art", "image/x-jg"},
                {".asa", "application/xml"},
                {".asax", "application/xml"},
                {".ascx", "application/xml"},
                {".asf", "video/x-ms-asf"},
                {".ashx", "application/xml"},
                {".asm", "text/plain"},
                {".asmx", "application/xml"},
                {".aspx", "application/xml"},
                {".asr", "video/x-ms-asf"},
                {".asx", "video/x-ms-asf"},
                {".atom", "application/atom+xml"},
                {".au", "audio/basic"},
                {".avi", "video/x-msvideo"},
                {".axa", "audio/annodex"},
                {".axs", "application/olescript"},
                {".axv", "video/annodex"},
                {".bas", "text/plain"},
                {".bcpio", "application/x-bcpio"},
                {".bmp", "image/bmp"},
                {".c", "text/plain"},
                {".caf", "audio/x-caf"},
                {".calx", "application/vnd.ms-office.calx"},
                {".cat", "application/vnd.ms-pki.seccat"},
                {".cc", "text/plain"},
                {".cd", "text/plain"},
                {".cdda", "audio/aiff"},
                {".cdf", "application/x-cdf"},
                {".cer", "application/x-x509-ca-cert"},
                {".cfg", "text/plain"},
                {".class", "application/x-java-applet"},
                {".clp", "application/x-msclip"},
                {".cmd", "text/plain"},
                {".cmx", "image/x-cmx"},
                {".cnf", "text/plain"},
                {".cod", "image/cis-cod"},
                {".config", "application/xml"},
                {".contact", "text/x-ms-contact"},
                {".coverage", "application/xml"},
                {".cpio", "application/x-cpio"},
                {".cpp", "text/plain"},
                {".crd", "application/x-mscardfile"},
                {".crl", "application/pkix-crl"},
                {".crt", "application/x-x509-ca-cert"},
                {".cs", "text/plain"},
                {".csdproj", "text/plain"},
                {".csh", "application/x-csh"},
                {".csproj", "text/plain"},
                {".css", "text/css"},
                {".csv", "text/csv"},
                {".cxx", "text/plain"},
                {".datasource", "application/xml"},
                {".dbproj", "text/plain"},
                {".dcr", "application/x-director"},
                {".def", "text/plain"},
                {".der", "application/x-x509-ca-cert"},
                {".dgml", "application/xml"},
                {".dib", "image/bmp"},
                {".dif", "video/x-dv"},
                {".dir", "application/x-director"},
                {".disco", "text/xml"},
                {".divx", "video/divx"},
                {".dll", "application/x-msdownload"},
                {".dll.config", "text/xml"},
                {".dlm", "text/dlm"},
                {".doc", "application/msword"},
                {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".dot", "application/msword"},
                {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
                {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
                {".dsw", "text/plain"},
                {".dtd", "text/xml"},
                {".dtsConfig", "text/xml"},
                {".dv", "video/x-dv"},
                {".dvi", "application/x-dvi"},
                {".dwf", "drawing/x-dwf"},
                {".dwg", "application/acad"},
                {".dxf", "application/x-dxf" },
                {".dxr", "application/x-director"},
                {".eml", "message/rfc822"},
                {".eot", "application/vnd.ms-fontobject"},
                {".eps", "application/postscript"},
                {".etl", "application/etl"},
                {".etx", "text/x-setext"},
                {".evy", "application/envoy"},
                {".exe.config", "text/xml"},
                {".fdf", "application/vnd.fdf"},
                {".fif", "application/fractals"},
                {".filters", "application/xml"},
                {".flac", "audio/flac"},
                {".flr", "x-world/x-vrml"},
                {".flv", "video/x-flv"},
                {".fsscript", "application/fsharp-script"},
                {".fsx", "application/fsharp-script"},
                {".generictest", "application/xml"},
                {".gif", "image/gif"},
                {".gpx", "application/gpx+xml"},
                {".group", "text/x-ms-group"},
                {".gsm", "audio/x-gsm"},
                {".gtar", "application/x-gtar"},
                {".gz", "application/x-gzip"},
                {".h", "text/plain"},
                {".hdf", "application/x-hdf"},
                {".hdml", "text/x-hdml"},
                {".hhc", "application/x-oleobject"},
                {".hlp", "application/winhlp"},
                {".hpp", "text/plain"},
                {".hqx", "application/mac-binhex40"},
                {".hta", "application/hta"},
                {".htc", "text/x-component"},
                {".htm", "text/html"},
                {".html", "text/html"},
                {".htt", "text/webviewhtml"},
                {".hxa", "application/xml"},
                {".hxc", "application/xml"},
                {".hxe", "application/xml"},
                {".hxf", "application/xml"},
                {".hxk", "application/xml"},
                {".hxt", "text/html"},
                {".hxv", "application/xml"},
                {".hxx", "text/plain"},
                {".i", "text/plain"},
                {".ico", "image/x-icon"},
                {".idl", "text/plain"},
                {".ief", "image/ief"},
                {".iii", "application/x-iphone"},
                {".inc", "text/plain"},
                {".ini", "text/plain"},
                {".inl", "text/plain"},
                {".ins", "application/x-internet-signup"},
                {".ipa", "application/x-itunes-ipa"},
                {".ipg", "application/x-itunes-ipg"},
                {".ipproj", "text/plain"},
                {".ipsw", "application/x-itunes-ipsw"},
                {".iqy", "text/x-ms-iqy"},
                {".isp", "application/x-internet-signup"},
                {".ite", "application/x-itunes-ite"},
                {".itlp", "application/x-itunes-itlp"},
                {".itms", "application/x-itunes-itms"},
                {".itpc", "application/x-itunes-itpc"},
                {".IVF", "video/x-ivf"},
                {".jar", "application/java-archive"},
                {".jck", "application/liquidmotion"},
                {".jcz", "application/liquidmotion"},
                {".jfif", "image/pjpeg"},
                {".jnlp", "application/x-java-jnlp-file"},
                {".jpe", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".jpg", "image/jpeg"},
                {".js", "application/javascript"},
                {".json", "application/json"},
                {".jsx", "text/jscript"},
                {".jsxbin", "text/plain"},
                {".latex", "application/x-latex"},
                {".library-ms", "application/windows-library+xml"},
                {".lit", "application/x-ms-reader"},
                {".loadtest", "application/xml"},
                {".lsf", "video/x-la-asf"},
                {".lst", "text/plain"},
                {".lsx", "video/x-la-asf"},
                {".m13", "application/x-msmediaview"},
                {".m14", "application/x-msmediaview"},
                {".m1v", "video/mpeg"},
                {".m2t", "video/vnd.dlna.mpeg-tts"},
                {".m2ts", "video/vnd.dlna.mpeg-tts"},
                {".m2v", "video/mpeg"},
                {".m3u", "audio/x-mpegurl"},
                {".m3u8", "audio/x-mpegurl"},
                {".m4a", "audio/m4a"},
                {".m4b", "audio/m4b"},
                {".m4p", "audio/m4p"},
                {".m4r", "audio/x-m4r"},
                {".m4v", "video/x-m4v"},
                {".mac", "image/x-macpaint"},
                {".mak", "text/plain"},
                {".man", "application/x-troff-man"},
                {".manifest", "application/x-ms-manifest"},
                {".map", "text/plain"},
                {".master", "application/xml"},
                {".mbox", "application/mbox"},
                {".mda", "application/msaccess"},
                {".mdb", "application/x-msaccess"},
                {".mde", "application/msaccess"},
                {".me", "application/x-troff-me"},
                {".mfp", "application/x-shockwave-flash"},
                {".mht", "message/rfc822"},
                {".mhtml", "message/rfc822"},
                {".mid", "audio/mid"},
                {".midi", "audio/mid"},
                {".mk", "text/plain"},
                {".mmf", "application/x-smaf"},
                {".mno", "text/xml"},
                {".mny", "application/x-msmoney"},
                {".mod", "video/mpeg"},
                {".mov", "video/quicktime"},
                {".movie", "video/x-sgi-movie"},
                {".mp2", "video/mpeg"},
                {".mp2v", "video/mpeg"},
                {".mp3", "audio/mpeg"},
                {".mp4", "video/mp4"},
                {".mp4v", "video/mp4"},
                {".mpa", "video/mpeg"},
                {".mpe", "video/mpeg"},
                {".mpeg", "video/mpeg"},
                {".mpf", "application/vnd.ms-mediapackage"},
                {".mpg", "video/mpeg"},
                {".mpp", "application/vnd.ms-project"},
                {".mpv2", "video/mpeg"},
                {".mqv", "video/quicktime"},
                {".ms", "application/x-troff-ms"},
                {".msg", "application/vnd.ms-outlook"},
                {".mts", "video/vnd.dlna.mpeg-tts"},
                {".mtx", "application/xml"},
                {".mvb", "application/x-msmediaview"},
                {".mvc", "application/x-miva-compiled"},
                {".mxp", "application/x-mmxp"},
                {".nc", "application/x-netcdf"},
                {".nsc", "video/x-ms-asf"},
                {".nws", "message/rfc822"},
                {".oda", "application/oda"},
                {".odb", "application/vnd.oasis.opendocument.database"},
                {".odc", "application/vnd.oasis.opendocument.chart"},
                {".odf", "application/vnd.oasis.opendocument.formula"},
                {".odg", "application/vnd.oasis.opendocument.graphics"},
                {".odh", "text/plain"},
                {".odi", "application/vnd.oasis.opendocument.image"},
                {".odl", "text/plain"},
                {".odm", "application/vnd.oasis.opendocument.text-master"},
                {".odp", "application/vnd.oasis.opendocument.presentation"},
                {".ods", "application/vnd.oasis.opendocument.spreadsheet"},
                {".odt", "application/vnd.oasis.opendocument.text"},
                {".oga", "audio/ogg"},
                {".ogg", "audio/ogg"},
                {".ogv", "video/ogg"},
                {".ogx", "application/ogg"},
                {".one", "application/onenote"},
                {".onea", "application/onenote"},
                {".onepkg", "application/onenote"},
                {".onetmp", "application/onenote"},
                {".onetoc", "application/onenote"},
                {".onetoc2", "application/onenote"},
                {".opus", "audio/ogg"},
                {".orderedtest", "application/xml"},
                {".osdx", "application/opensearchdescription+xml"},
                {".otf", "application/font-sfnt"},
                {".otg", "application/vnd.oasis.opendocument.graphics-template"},
                {".oth", "application/vnd.oasis.opendocument.text-web"},
                {".otp", "application/vnd.oasis.opendocument.presentation-template"},
                {".ots", "application/vnd.oasis.opendocument.spreadsheet-template"},
                {".ott", "application/vnd.oasis.opendocument.text-template"},
                {".oxt", "application/vnd.openofficeorg.extension"},
                {".p10", "application/pkcs10"},
                {".p12", "application/x-pkcs12"},
                {".p7b", "application/x-pkcs7-certificates"},
                {".p7c", "application/pkcs7-mime"},
                {".p7m", "application/pkcs7-mime"},
                {".p7r", "application/x-pkcs7-certreqresp"},
                {".p7s", "application/pkcs7-signature"},
                {".pbm", "image/x-portable-bitmap"},
                {".pcast", "application/x-podcast"},
                {".pct", "image/pict"},
                {".pdf", "application/pdf"},
                {".pfx", "application/x-pkcs12"},
                {".pgm", "image/x-portable-graymap"},
                {".pic", "image/pict"},
                {".pict", "image/pict"},
                {".pkgdef", "text/plain"},
                {".pkgundef", "text/plain"},
                {".pko", "application/vnd.ms-pki.pko"},
                {".pls", "audio/scpls"},
                {".pma", "application/x-perfmon"},
                {".pmc", "application/x-perfmon"},
                {".pml", "application/x-perfmon"},
                {".pmr", "application/x-perfmon"},
                {".pmw", "application/x-perfmon"},
                {".png", "image/png"},
                {".pnm", "image/x-portable-anymap"},
                {".pnt", "image/x-macpaint"},
                {".pntg", "image/x-macpaint"},
                {".pnz", "image/png"},
                {".pot", "application/vnd.ms-powerpoint"},
                {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
                {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
                {".ppa", "application/vnd.ms-powerpoint"},
                {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
                {".ppm", "image/x-portable-pixmap"},
                {".pps", "application/vnd.ms-powerpoint"},
                {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
                {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
                {".ppt", "application/vnd.ms-powerpoint"},
                {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                {".prf", "application/pics-rules"},
                {".ps", "application/postscript"},
                {".psc1", "application/PowerShell"},
                {".psess", "application/xml"},
                {".pst", "application/vnd.ms-outlook"},
                {".pub", "application/x-mspublisher"},
                {".pwz", "application/vnd.ms-powerpoint"},
                {".qht", "text/x-html-insertion"},
                {".qhtm", "text/x-html-insertion"},
                {".qt", "video/quicktime"},
                {".qti", "image/x-quicktime"},
                {".qtif", "image/x-quicktime"},
                {".qtl", "application/x-quicktimeplayer"},
                {".ra", "audio/x-pn-realaudio"},
                {".ram", "audio/x-pn-realaudio"},
                {".rar", "application/x-rar-compressed"},
                {".ras", "image/x-cmu-raster"},
                {".rat", "application/rat-file"},
                {".rc", "text/plain"},
                {".rc2", "text/plain"},
                {".rct", "text/plain"},
                {".rdlc", "application/xml"},
                {".reg", "text/plain"},
                {".resx", "application/xml"},
                {".rf", "image/vnd.rn-realflash"},
                {".rgb", "image/x-rgb"},
                {".rgs", "text/plain"},
                {".rm", "application/vnd.rn-realmedia"},
                {".rmi", "audio/mid"},
                {".rmp", "application/vnd.rn-rn_music_package"},
                {".roff", "application/x-troff"},
                {".rpm", "audio/x-pn-realaudio-plugin"},
                {".rqy", "text/x-ms-rqy"},
                {".rtf", "application/rtf"},
                {".rtx", "text/richtext"},
                {".ruleset", "application/xml"},
                {".s", "text/plain"},
                {".safariextz", "application/x-safari-safariextz"},
                {".scd", "application/x-msschedule"},
                {".scr", "text/plain"},
                {".sct", "text/scriptlet"},
                {".sd2", "audio/x-sd2"},
                {".sdp", "application/sdp"},
                {".searchConnector-ms", "application/windows-search-connector+xml"},
                {".setpay", "application/set-payment-initiation"},
                {".setreg", "application/set-registration-initiation"},
                {".settings", "application/xml"},
                {".sgimb", "application/x-sgimb"},
                {".sgml", "text/sgml"},
                {".sh", "application/x-sh"},
                {".shar", "application/x-shar"},
                {".shtml", "text/html"},
                {".sit", "application/x-stuffit"},
                {".sitemap", "application/xml"},
                {".skin", "application/xml"},
                {".skp", "application/x-koan" },
                {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
                {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
                {".slk", "application/vnd.ms-excel"},
                {".sln", "text/plain"},
                {".slupkg-ms", "application/x-ms-license"},
                {".smd", "audio/x-smd"},
                {".smx", "audio/x-smd"},
                {".smz", "audio/x-smd"},
                {".snd", "audio/basic"},
                {".snippet", "application/xml"},
                {".sol", "text/plain"},
                {".sor", "text/plain"},
                {".spc", "application/x-pkcs7-certificates"},
                {".spl", "application/futuresplash"},
                {".spx", "audio/ogg"},
                {".src", "application/x-wais-source"},
                {".srf", "text/plain"},
                {".SSISDeploymentManifest", "text/xml"},
                {".ssm", "application/streamingmedia"},
                {".sst", "application/vnd.ms-pki.certstore"},
                {".stl", "application/vnd.ms-pki.stl"},
                {".sv4cpio", "application/x-sv4cpio"},
                {".sv4crc", "application/x-sv4crc"},
                {".svc", "application/xml"},
                {".svg", "image/svg+xml"},
                {".swf", "application/x-shockwave-flash"},
                {".step", "application/step"},
                {".stp", "application/step"},
                {".t", "application/x-troff"},
                {".tar", "application/x-tar"},
                {".tcl", "application/x-tcl"},
                {".testrunconfig", "application/xml"},
                {".testsettings", "application/xml"},
                {".tex", "application/x-tex"},
                {".texi", "application/x-texinfo"},
                {".texinfo", "application/x-texinfo"},
                {".tgz", "application/x-compressed"},
                {".thmx", "application/vnd.ms-officetheme"},
                {".tif", "image/tiff"},
                {".tiff", "image/tiff"},
                {".tlh", "text/plain"},
                {".tli", "text/plain"},
                {".tr", "application/x-troff"},
                {".trm", "application/x-msterminal"},
                {".trx", "application/xml"},
                {".ts", "video/vnd.dlna.mpeg-tts"},
                {".tsv", "text/tab-separated-values"},
                {".ttf", "application/font-sfnt"},
                {".tts", "video/vnd.dlna.mpeg-tts"},
                {".txt", "text/plain"},
                {".uls", "text/iuls"},
                {".user", "text/plain"},
                {".ustar", "application/x-ustar"},
                {".vb", "text/plain"},
                {".vbdproj", "text/plain"},
                {".vbk", "video/mpeg"},
                {".vbproj", "text/plain"},
                {".vbs", "text/vbscript"},
                {".vcf", "text/x-vcard"},
                {".vcproj", "application/xml"},
                {".vcs", "text/plain"},
                {".vcxproj", "application/xml"},
                {".vddproj", "text/plain"},
                {".vdp", "text/plain"},
                {".vdproj", "text/plain"},
                {".vdx", "application/vnd.ms-visio.viewer"},
                {".vml", "text/xml"},
                {".vscontent", "application/xml"},
                {".vsct", "text/xml"},
                {".vsd", "application/vnd.visio"},
                {".vsi", "application/ms-vsi"},
                {".vsix", "application/vsix"},
                {".vsixlangpack", "text/xml"},
                {".vsixmanifest", "text/xml"},
                {".vsmdi", "application/xml"},
                {".vspscc", "text/plain"},
                {".vss", "application/vnd.visio"},
                {".vsscc", "text/plain"},
                {".vssettings", "text/xml"},
                {".vssscc", "text/plain"},
                {".vst", "application/vnd.visio"},
                {".vstemplate", "text/xml"},
                {".vsto", "application/x-ms-vsto"},
                {".vsw", "application/vnd.visio"},
                {".vsx", "application/vnd.visio"},
                {".vtx", "application/vnd.visio"},
                {".wav", "audio/wav"},
                {".wave", "audio/wav"},
                {".wax", "audio/x-ms-wax"},
                {".wbk", "application/msword"},
                {".wbmp", "image/vnd.wap.wbmp"},
                {".wcm", "application/vnd.ms-works"},
                {".wdb", "application/vnd.ms-works"},
                {".wdp", "image/vnd.ms-photo"},
                {".webarchive", "application/x-safari-webarchive"},
                {".webm", "video/webm"},
                {".webp", "image/webp"},
                {".webtest", "application/xml"},
                {".wiq", "application/xml"},
                {".wiz", "application/msword"},
                {".wks", "application/vnd.ms-works"},
                {".WLMP", "application/wlmoviemaker"},
                {".wlpginstall", "application/x-wlpg-detect"},
                {".wlpginstall3", "application/x-wlpg3-detect"},
                {".wm", "video/x-ms-wm"},
                {".wma", "audio/x-ms-wma"},
                {".wmd", "application/x-ms-wmd"},
                {".wmf", "application/x-msmetafile"},
                {".wml", "text/vnd.wap.wml"},
                {".wmlc", "application/vnd.wap.wmlc"},
                {".wmls", "text/vnd.wap.wmlscript"},
                {".wmlsc", "application/vnd.wap.wmlscriptc"},
                {".wmp", "video/x-ms-wmp"},
                {".wmv", "video/x-ms-wmv"},
                {".wmx", "video/x-ms-wmx"},
                {".wmz", "application/x-ms-wmz"},
                {".woff", "application/font-woff"},
                {".wpl", "application/vnd.ms-wpl"},
                {".wps", "application/vnd.ms-works"},
                {".wri", "application/x-mswrite"},
                {".wrl", "x-world/x-vrml"},
                {".wrz", "x-world/x-vrml"},
                {".wsc", "text/scriptlet"},
                {".wsdl", "text/xml"},
                {".wvx", "video/x-ms-wvx"},
                {".x", "application/directx"},
                {".xaf", "x-world/x-vrml"},
                {".xaml", "application/xaml+xml"},
                {".xap", "application/x-silverlight-app"},
                {".xbap", "application/x-ms-xbap"},
                {".xbm", "image/x-xbitmap"},
                {".xdr", "text/plain"},
                {".xht", "application/xhtml+xml"},
                {".xhtml", "application/xhtml+xml"},
                {".xla", "application/vnd.ms-excel"},
                {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
                {".xlc", "application/vnd.ms-excel"},
                {".xld", "application/vnd.ms-excel"},
                {".xlk", "application/vnd.ms-excel"},
                {".xll", "application/vnd.ms-excel"},
                {".xlm", "application/vnd.ms-excel"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
                {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".xlt", "application/vnd.ms-excel"},
                {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
                {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
                {".xlw", "application/vnd.ms-excel"},
                {".xml", "text/xml"},
                {".xmta", "application/xml"},
                {".xof", "x-world/x-vrml"},
                {".XOML", "text/plain"},
                {".xpm", "image/x-xpixmap"},
                {".xps", "application/vnd.ms-xpsdocument"},
                {".xrm-ms", "text/xml"},
                {".xsc", "application/xml"},
                {".xsd", "text/xml"},
                {".xsf", "text/xml"},
                {".xsl", "text/xml"},
                {".xslt", "text/xml"},
                {".xss", "application/xml"},
                {".xspf", "application/xspf+xml"},
                {".xwd", "image/x-xwindowdump"},
                {".z", "application/x-compress"},
                {".zip", "application/zip"}
            };
        }
    }
}
