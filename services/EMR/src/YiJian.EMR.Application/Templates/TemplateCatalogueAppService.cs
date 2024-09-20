using Castle.Core.Internal;
using DotNetCore.CAP;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.EmrPermissions.Contracts;
using YiJian.EMR.Enums;
using YiJian.EMR.Libs;
using YiJian.EMR.Libs.Dto;
using YiJian.EMR.Templates.Contracts;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.XmlHistories.Contracts;
using YiJian.EMR.XmlHistories.Entities;
using static YiJian.EMR.Permissions.EMRPermissions;
using YiJian.EMR.Libs.Entities;
using YiJian.ECIS.ShareModel.Utils;
using System.Diagnostics;
using YiJian.EMR.EmrPermissions.Entities;

namespace YiJian.EMR.Templates
{
    /// <summary>
    /// 病历模板
    /// </summary>
    [Authorize]
    public partial class TemplateCatalogueAppService : EMRAppService, ITemplateCatalogueAppService, ICapSubscribe
    {
        private const string TEMPLATE_ITEMS_SORT = "emr.template.items.sort";

        private readonly ILogger<TemplateCatalogueAppService> _logger;
        private readonly IMyXmlTemplateRepository _myXmlTemplateRepository;
        private readonly IInpatientWardRepository _inpatientWardRepository;
        private readonly ITemplateCatalogueRepository _templateCatalogueRepository;
        private readonly IXmlTemplateRepository _xmlTemplateRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICatalogueRepository _catalogueRepository;
        private readonly IXmlHistoryRepository _xmlHistoryRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMergeTemplateWhiteListRepository _mergeTemplateWhiteListRepository;

        private readonly IConfiguration _configuration;

        private readonly ICapPublisher _capPublisher;
        private readonly IDataFilter<ISoftDelete> _softDeleteFilter;
        /// <summary>
        /// 病历模板
        /// </summary>
        public TemplateCatalogueAppService(
            ILogger<TemplateCatalogueAppService> logger,
            IMyXmlTemplateRepository myXmlTemplateRepository,
            IInpatientWardRepository inpatientWardRepository,
            ITemplateCatalogueRepository templateCatalogueRepository,
            IXmlTemplateRepository xmlTemplateRepository,
            IDepartmentRepository departmentRepository,
            ICatalogueRepository catalogueRepository,
            IXmlHistoryRepository xmlHistoryRepository,
            IPermissionRepository permissionRepository,
            IMergeTemplateWhiteListRepository mergeTemplateWhiteListRepository,
            IConfiguration configuration,
            ICapPublisher capPublisher,
            IDataFilter<ISoftDelete> softDeleteFilter
        )
        {
            _logger = logger;
            _myXmlTemplateRepository = myXmlTemplateRepository;
            _inpatientWardRepository = inpatientWardRepository;
            _templateCatalogueRepository = templateCatalogueRepository;
            _xmlTemplateRepository = xmlTemplateRepository;
            _departmentRepository = departmentRepository;
            _catalogueRepository = catalogueRepository;
            _xmlHistoryRepository = xmlHistoryRepository;
            _permissionRepository = permissionRepository;
            _mergeTemplateWhiteListRepository = mergeTemplateWhiteListRepository;
            _configuration = configuration;
            _capPublisher = capPublisher;
            _softDeleteFilter = softDeleteFilter;
        }

        #region 获取目录节点结构树

        /// <summary>
        /// 获取目录分组信息【所属分组】
        /// </summary>
        /// <see cref="CatalogueRootRequestDto"/>
        /// <returns></returns>
        //[Authorize(EMRPermissions.TemplateCatalogues.List)]
        public async Task<ResponseBase<List<CatalogueRootDto>>> CatalogueRootOptionsAsync(CatalogueRootRequestDto model)
        {
            if (model.ContainDeleted)
            {
                // 如果需要包含已删除项，则禁用软删除过滤器
                using (_softDeleteFilter.Disable())
                {
                    return await CatalogueRootOptionsAsync(model.Lv, model.TemplateType, model.Classify, model.DeptCode,
                        model.DoctorCode);
                }
            }

            return await CatalogueRootOptionsAsync(model.Lv, model.TemplateType, model.Classify, model.DeptCode,
                model.DoctorCode);
        }

        private async Task<ResponseBase<List<CatalogueRootDto>>> CatalogueRootOptionsAsync(int lv,
            ETemplateType templateType,
            EClassify classify, string deptCode, string doctorCode)
        {
            var list = await (await _templateCatalogueRepository.GetQueryableAsync())
                .Where(w => w.IsFile == false && w.Lv <= lv && w.TemplateType == templateType &&
                            w.Classify == classify)
                .WhereIf(templateType == ETemplateType.Department, w => w.DeptCode == deptCode)
                .WhereIf(templateType == ETemplateType.Personal, w => w.DoctorCode == doctorCode)
                .Select(s => new CatalogueRootDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    ParentId = s.ParentId,
                    Sort = s.Sort
                })
                .ToListAsync();

            var root = list.Where(w => w.ParentId == null).OrderBy(o => o.Sort).ToList();
            if (root.Count == 0)
            {
                return await Task.FromResult(new ResponseBase<List<CatalogueRootDto>>(EStatusCode.CNULL));
            }

            foreach (var item in root)
            {
                RecursiveCatalogueNodes(list, item);
            }

            return await Task.FromResult(new ResponseBase<List<CatalogueRootDto>>(EStatusCode.COK, root));
        }

        /// <summary>
        /// 递归获取目录树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param>
        private void RecursiveCatalogueNodes(List<CatalogueRootDto> list, CatalogueRootDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id).OrderBy(s => s.Sort).ToList();
            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Catalogues.AddRange(sub);

                foreach (var subItem in item.Catalogues)
                {
                    RecursiveCatalogueNodes(list, subItem);
                }
            }
        }

        #endregion

        #region 获取所有的通用模板


        /// <summary>
        /// 获取所有的通用模板
        /// </summary>
        /// <param name="classify"> 电子文书分类(0=电子病历,1=文书)</param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.TemplateCatalogues.List)]
        [AllowAnonymous]
        public async Task<ResponseBase<List<GeneralCatalogueTreeDto>>> GetGeneralCataloguesAsync(EClassify classify, bool isBackground = true)
        {
            List<GeneralCatalogueTreeDto> tree = new();

            var list = (await _templateCatalogueRepository.GetQueryableAsync())
                .Where(w => w.TemplateType == ETemplateType.General && w.Classify == classify)
                .WhereIf(!isBackground, w => w.IsEnabled)
                .Select(s => new GeneralCatalogueTreeDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Code = s.Code,
                    IsEnabled = s.IsEnabled,
                    IsFile = s.IsFile,
                    ParentId = s.ParentId,
                    TemplateType = s.TemplateType,
                    Sort = s.Sort,
                    Lv = s.Lv,
                    CatalogueId = s.CatalogueId,
                    CatalogueTitle = s.CatalogueTitle,
                    OriginId = s.OriginId,
                })
                .ToList();

            var root = list.Where(w => !w.ParentId.HasValue).OrderBy(o => o.Sort).ToList();

            if (root.Count == 0)
            {
                return new ResponseBase<List<GeneralCatalogueTreeDto>>(EStatusCode.CNULL);
            }

            foreach (var item in root)
            {
                RecursiveCatalogue(list, item);
            }

            return new ResponseBase<List<GeneralCatalogueTreeDto>>(EStatusCode.COK, root);
        }

        /// <summary>
        /// 递归获取目录树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param>
        private void RecursiveCatalogue(List<GeneralCatalogueTreeDto> list, GeneralCatalogueTreeDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id).OrderBy(o => o.Sort).ToList();
            List<GeneralCatalogueTreeDto> subData = new();

            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Catalogues.AddRange(sub);

                foreach (var subItem in item.Catalogues)
                {
                    if (subItem.IsFile)
                    {
                        continue;
                    }
                    RecursiveCatalogue(list, subItem);
                }
            }
        }

        #endregion

        #region 获取当前用户的科室模板目录

        /// <summary>
        /// 获取当前用户的科室模板目录
        /// </summary>
        /// <param name="role">权限： 0:职工，1:医生，2:护士，3:其他,301:司机,302:担架员,303:抢救员</param>
        /// （期望展示的权限，
        /// 如果当前用户既有admin权限也有doctor权限，前端权限有指定需求， 
        /// 可以通过这个参数传递过来进行权限控制，如果没有，后端以默认最高权限处理） 
        /// <param name="deptCode">科室编码</param>
        /// <param name="deptName"></param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.TemplateCatalogues.List)] 
        public async Task<ResponseBase<List<DepartmentCatalogueGroupDto>>> GetDepartmentCataloguesAsync(EExpectedRole role, [NotNull] string deptCode, string deptName, bool isBackground = true)
        {
            if (role == EExpectedRole.Admin || role == EExpectedRole.Doctor)
            {
                return await DepartmentCataloguesAsync(deptCode, deptName, isBackground);
            }
            return new ResponseBase<List<DepartmentCatalogueGroupDto>>(EStatusCode.CNULL, message: "您没有访问该电子病历的权限");
        }

        /// <summary>
        /// 获取当前用户的科室模板目录[管理员视图]
        /// </summary>
        /// <param name="deptId">科室编码</param>
        /// <param name="deptName"></param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        private async Task<ResponseBase<List<DepartmentCatalogueGroupDto>>> DepartmentCataloguesAsync(string deptId, string deptName, bool isBackground = true)
        {
            var dataList = new List<DepartmentCatalogueGroupDto>();

            var data = new List<DepartmentCatalogueTreeDto>();
            //var emergency = _configuration.GetSection("Departments:Emergency").Value;

            //var department = CurrentUser.FindClaim("Department");
            //var emergency = JObject.Parse(department.Value)["DeptCode"].ToString();
            //var jwtDeptName = JObject.Parse(department.Value)["DeptName"].ToString();

            var list = await GetDepartmentCatalogueListAsync(deptId, isBackground);

            list = await InitDepartmentsAreaAsync(deptId, deptName, list, isBackground); //初始化科室病区数据

            var depts = list.Where(w => w.DeptCode != "" || w.DeptCode != null).Select(s => s.DeptCode).Distinct().ToList();

            foreach (var deptCode in depts)
            {
                var deptList = list.Where(w => w.DeptCode == deptCode).ToList();

                var root = deptList.Where(w => w.ParentId == null).FirstOrDefault();
                if (root == null) continue;

                //第一层是科室
                var deptRoot = new DepartmentCatalogueGroupDto(
                    root.Id, /*root.Title*/root.DeptName, root.DeptCode, root.IsFile, root.ParentId,
                    root.TemplateType, root.IsEnabled, root.Sort, root.Lv,
                    root.DeptCode, root.DeptName, root.InpatientWardId, root.OriginId);

                var area = deptList.Where(w => w.ParentId == null).ToList();

                var areaRoots = new List<DepartmentCatalogueTreeDto>();

                foreach (var item in area)
                {
                    RecursiveDepartmentCatalogue(deptList, item);
                    areaRoots.Add(item);
                }

                deptRoot.Catalogues.AddRange(areaRoots);
                dataList.Add(deptRoot);
            }

            return new ResponseBase<List<DepartmentCatalogueGroupDto>>(EStatusCode.COK, dataList);
        }

        private async Task<List<DepartmentCatalogueTreeDto>> InitDepartmentsAreaAsync(string deptId, string deptName, List<DepartmentCatalogueTreeDto> list, bool isBackground = true)
        {
            if (!list.Any())
            {
                List<TemplateCatalogue> templateCatalogues = new();
                var inpatientWards = await _inpatientWardRepository.GetListAsync();
                int sort = 1;
                foreach (var inpatientWard in inpatientWards)
                {
                    var entity = new TemplateCatalogue(
                        id: GuidGenerator.Create(),
                        title: inpatientWard.WardName,
                        code: null,
                        sort: sort++,
                        deptCode: deptId,
                        inpatientWardId: inpatientWard.Id,
                        parentId: null,
                        classify: EClassify.EMR);
                    templateCatalogues.Add(entity);
                }
                var dept = await (await _departmentRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DeptCode == deptId);
                if (dept == null) await _departmentRepository.InsertAsync(new Department(GuidGenerator.Create(), deptId, deptName), true);
                if (templateCatalogues.Any()) await _templateCatalogueRepository.InsertManyAsync(templateCatalogues, true);

                list = await GetDepartmentCatalogueListAsync(deptId, isBackground);
            }

            return list;
        }

        /// <summary>
        /// 递归获取目录树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param>
        private void RecursiveDepartmentCatalogue(List<DepartmentCatalogueTreeDto> list, DepartmentCatalogueTreeDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id).OrderBy(o => o.Sort).ToList();
            List<DepartmentCatalogueTreeDto> subData = new List<DepartmentCatalogueTreeDto>();

            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Catalogues.AddRange(sub);

                foreach (var subItem in item.Catalogues)
                {
                    if (subItem.IsFile)
                    {
                        continue;
                    }
                    RecursiveDepartmentCatalogue(list, subItem);
                }
            }
        }

        /// <summary>
        /// 获取科室目录记录
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        private async Task<List<DepartmentCatalogueTreeDto>> GetDepartmentCatalogueListAsync(string deptCode, bool isBackground = true)
        {
            var query = from t in (await _templateCatalogueRepository.GetQueryableAsync())
                            .Where(w => w.TemplateType == ETemplateType.Department && w.InpatientWardId.HasValue)
                            .WhereIf(!deptCode.IsNullOrWhiteSpace(), w => w.DeptCode == deptCode)
                            .WhereIf(deptCode.IsNullOrWhiteSpace(), w => w.DeptCode != "")
                            .WhereIf(!isBackground, w => w.IsEnabled)
                        join i in (await _inpatientWardRepository.GetQueryableAsync())
                        on t.InpatientWardId.Value equals i.Id
                        join d in (await _departmentRepository.GetQueryableAsync())
                        on t.DeptCode equals d.DeptCode
                        orderby t.Sort ascending
                        select new DepartmentCatalogueTreeDto
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Code = t.Code,
                            DeptCode = t.DeptCode,
                            DeptName = d.DeptName,
                            InpatientWardId = t.InpatientWardId,
                            WardName = i.WardName,
                            IsEnabled = t.IsEnabled,
                            IsFile = t.IsFile,
                            Lv = t.Lv,
                            ParentId = t.ParentId,
                            Sort = t.Sort,
                            TemplateType = t.TemplateType,
                            CatalogueId = t.CatalogueId,
                            CatalogueTitle = t.CatalogueTitle,
                            OriginId = t.OriginId,
                        };

            var list = query.ToList();
            return await Task.FromResult(list);
        }

        #endregion

        #region 获取当前用户的个人模板目录

        /// <summary>
        /// 获取当前用户的个人模板目录
        /// </summary>
        /// <param name="role">角色类型  -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士
        /// （期望展示的权限，
        /// 如果当前用户既有admin权限也有doctor权限，前端权限有指定需求， 
        /// 可以通过这个参数传递过来进行权限控制，如果没有，后端以默认最高权限处理）
        /// </param>
        /// <param name="doctorCode">医生编码</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.TemplateCatalogues.List)] 
        public async Task<ResponseBase<List<PersonalCatalogueGroupDto>>> GetPersonalCataloguesAsync(EExpectedRole role, string doctorCode, string doctorName, bool isBackground = true)
        {
            await InitPersonalTemplateAsync(doctorCode, doctorName);

            //权限包括管理员和医生的情况
            if (role == EExpectedRole.Admin)
            {
                return await GetAdminPersonalCataloguesAsync(isBackground);
            }
            else if (role == EExpectedRole.Doctor)
            {
                return await GetDoctorPersonalCataloguesAsync(doctorCode, isBackground);
            }
            else
            {
                return new ResponseBase<List<PersonalCatalogueGroupDto>>(EStatusCode.CNULL, message: "您没有访问该电子病历的权限");
            }
        }

        /// <summary>
        /// 初始化电子病历个人模板目录
        /// </summary>
        /// <param name="doctorCode"></param>
        /// <param name="doctorName"></param>
        /// <returns></returns>
        private async Task<TemplateCatalogue> InitPersonalTemplateAsync(string doctorCode, string doctorName)
        {
            if (doctorName.IsNullOrEmpty()) doctorName = doctorCode;
            var catalogues = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.TemplateType == ETemplateType.Personal && w.DoctorCode == doctorCode && w.ParentId == null);
            if (catalogues != null) return catalogues;

            var entity = new TemplateCatalogue(id: GuidGenerator.Create(), title: doctorName, code: null, sort: 0, doctorCode: doctorCode, doctorName: doctorName);
            _ = await _templateCatalogueRepository.InsertAsync(entity, autoSave: true);
            return entity;
        }

        /// <summary>
        /// 获取当前用户的个人模板目录 [管理员视图]
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseBase<List<PersonalCatalogueGroupDto>>> GetAdminPersonalCataloguesAsync(bool isBackground = true)
        {
            var list = await GetPersonCatalogueListAsync("", isBackground); //获取所有人的个人电子病历模板目录

            if (!list.Any()) return new ResponseBase<List<PersonalCatalogueGroupDto>>(EStatusCode.CNULL);

            var doctors = list.GroupBy(g => g.DoctorCode).Select(g => new
            {
                DoctorCode = g.Key,
                DoctorName = g.Max(a => a.DoctorName)
            });

            var group = doctors.Select(s => new PersonalCatalogueGroupDto
            {
                DoctorCode = s.DoctorCode,
                DoctorName = s.DoctorName
            }).ToList();

            foreach (var item in group)
            {
                var subQuery = list.Where(w => w.DoctorCode == item.DoctorCode).ToList();
                var subList = ObjectMapper.Map<List<PersonalCatalogueListDto>, List<PersonalCatalogueTreeDto>>(subQuery);

                var rootQuery = subQuery.Where(w => w.ParentId == null).FirstOrDefault();
                var root = ObjectMapper.Map<PersonalCatalogueListDto, PersonalCatalogueTreeDto>(rootQuery);

                RecursiveDoctorPersonalCatalogues(subList, root);

                item.Update(root.Id, root.Title, root.Code, root.ParentId, root.TemplateType, root.IsEnabled, root.IsFile, root.Lv, root.Catalogues);

            }

            return new ResponseBase<List<PersonalCatalogueGroupDto>>(EStatusCode.COK, group);
        }

        /// <summary>
        /// 获取当前用户的个人模板目录 [医生视图]
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseBase<List<PersonalCatalogueGroupDto>>> GetDoctorPersonalCataloguesAsync(string doctorCode, bool isBackground = true)
        {
            var query = await GetPersonCatalogueListAsync(doctorCode, isBackground);

            if (!query.Any()) return new ResponseBase<List<PersonalCatalogueGroupDto>>(EStatusCode.CNULL);
            var rootQuery = query.Where(w => w.ParentId == null).FirstOrDefault();
            var root = ObjectMapper.Map<PersonalCatalogueListDto, PersonalCatalogueTreeDto>(rootQuery);
            if (root == null) return new ResponseBase<List<PersonalCatalogueGroupDto>>(EStatusCode.CNULL);
            var list = ObjectMapper.Map<List<PersonalCatalogueListDto>, List<PersonalCatalogueTreeDto>>(query);

            RecursiveDoctorPersonalCatalogues(list, root); //递归获取目录结构树

            var group = new List<PersonalCatalogueGroupDto>();
            var doctorItem = new PersonalCatalogueGroupDto
            {
                DoctorCode = rootQuery.DoctorCode,
                DoctorName = rootQuery.DoctorName
            };

            doctorItem.Update(root.Id, root.Title, root.Code, root.ParentId, root.TemplateType, root.IsEnabled, root.IsFile, root.Lv, root.Catalogues);

            group.Add(doctorItem);

            return new ResponseBase<List<PersonalCatalogueGroupDto>>(EStatusCode.COK, group);
        }

        /// <summary>
        /// 递归获取目录树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param>
        private void RecursiveDoctorPersonalCatalogues(List<PersonalCatalogueTreeDto> list, PersonalCatalogueTreeDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id).OrderBy(o => o.Sort).ToList();
            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Catalogues.AddRange(sub);
                foreach (var subItem in item.Catalogues)
                {
                    if (subItem.IsFile)
                    {
                        continue;
                    }
                    RecursiveDoctorPersonalCatalogues(list, subItem);
                }
            }
        }

        /// <summary>
        /// 获取医生的电子病历模板集合
        /// </summary>
        /// <param name="doctorCode">空或空字符串都不参与条件查询</param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        private async Task<List<PersonalCatalogueListDto>> GetPersonCatalogueListAsync(string doctorCode, bool isBackground = true)
        {
            var query = (await _templateCatalogueRepository.GetQueryableAsync())
                .Where(w => w.TemplateType == ETemplateType.Personal)
                .WhereIf(!string.IsNullOrWhiteSpace(doctorCode), w => w.DoctorCode == doctorCode)
                .WhereIf(!isBackground, w => w.IsEnabled)
                .OrderBy(o => o.Sort)
                .ThenByDescending(o => o.CreationTime)
                .Select(s =>
                    new PersonalCatalogueListDto
                    {
                        Id = s.Id,
                        IsEnabled = s.IsEnabled,
                        IsFile = s.IsFile,
                        ParentId = s.ParentId,
                        TemplateType = s.TemplateType,
                        Title = s.Title,
                        Code = s.Code,
                        Sort = s.Sort,
                        DoctorCode = s.DoctorCode,
                        DoctorName = s.DoctorName,
                        Lv = s.Lv,
                        CatalogueId = s.CatalogueId,
                        CatalogueTitle = s.CatalogueTitle,
                        OriginId = s.OriginId,
                    }).ToList();
            return await Task.FromResult(query);
        }

        #endregion

        #region 病例分组管理（新增和编辑）

        /// <summary>
        /// 校验病历目录编码是否存在
        /// </summary>
        /// <param name="modifyType">操作类型，add 新增，反之修改</param>
        /// <param name="entityId">修改主键Id</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public async Task<string> CheckCatalogueCodeIsExistAsync(string code, string? modifyType, Guid? entityId)
        {
            if (string.IsNullOrEmpty(code)) { return null; }

            var query = await _templateCatalogueRepository.GetQueryableAsync();
            var find = await query.Where(w => w.Code == code && (!entityId.HasValue || w.Id != entityId.Value)).ToListAsync();
            if (find.Any())
            {
                return $"编码已存在，{(modifyType == "add" ? "添加" : "更新")}失败";
            }
            return null;
        }




        /// <summary>
        /// 新增/编辑病历分组
        /// </summary>
        /// <param name="model">病例目录模型</param>
        /// <returns></returns>
        [UnitOfWork]
        //[Authorize(EMRPermissions.TemplateCatalogues.Modify)]
        public async Task<ResponseBase<Guid>> ModifyCatalogueItemAsync(ModifyCatalogueItemDto model)
        {
            //权限控制
            var (refused, message) = await RefusedAsync(model.DoctorCode, model.Classify, model.TemplateType, model.DeptCode);
            if (refused) return new ResponseBase<Guid>(EStatusCode.CFail, message: message);

            if (model.Id.HasValue) return await UpdateCatalogueItemAsync(model);
            return await AddCatalogueItemAsync(model);
        }

        /// <summary>
        /// 编辑病历分组
        /// </summary>
        /// <param name="model">病例目录模型</param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> UpdateCatalogueItemAsync(ModifyCatalogueItemDto model)
        {
            var entity = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id.Value);

            //校验病历目录编码是否存在
            var msg = await CheckCatalogueCodeIsExistAsync(model.Code, null, entity.Id);
            if (!string.IsNullOrEmpty(msg))
                return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), msg);

            entity.Update(model.Title, model.Code, model.Sort);
            var ret = await _templateCatalogueRepository.UpdateAsync(entity);
            return new ResponseBase<Guid>(EStatusCode.COK, ret.Id);
        }

        /// <summary>
        /// 新增病历分组
        /// </summary>
        /// <param name="model">病例目录模型</param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> AddCatalogueItemAsync(ModifyCatalogueItemDto model)
        {
            List<TemplateCatalogue> entities = new();

            int level = 0;
            if (model.ParentId.HasValue)
            {
                var parent = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.ParentId.Value && w.Classify == model.Classify);
                level = parent.Lv + 1;
            }

            //校验病历目录编码是否存在
            var msg = await CheckCatalogueCodeIsExistAsync(model.Code, "add", null);
            if (!string.IsNullOrEmpty(msg))
                return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), msg);

            switch (model.TemplateType)
            {
                case ETemplateType.General:
                    {
                        var generalEntity = new TemplateCatalogue(GuidGenerator.Create(), model.Title, model.Code, model.Sort, model.ParentId, model.Classify, level);
                        entities.Add(generalEntity);
                        break;
                    }
                case ETemplateType.Department:
                    {
                        if (model.DeptCode.IsNullOrEmpty() || model.DeptName.IsNullOrEmpty())
                        {
                            return new ResponseBase<Guid>(EStatusCode.ParameterIsMissing, message: "科室编码，科室名称必传");
                        }

                        var dept = await (await _departmentRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DeptCode == model.DeptCode);
                        if (dept == null)
                        {
                            _ = await _departmentRepository.InsertAsync(new Department(GuidGenerator.Create(), model.DeptCode, model.DeptName));
                        }

                        //如果确定该模板为科室模板，并且没有指定病区
                        //那么就去检查当前科室模板是否已经初始化目录
                        //并且处理，1.存在跳过，2.不存在初始化
                        if (model.InpatientWardId == null)
                        {
                            var anyDept = (await _templateCatalogueRepository.GetQueryableAsync()).Any(w => w.DeptCode == model.DeptCode);
                            if (anyDept)
                            {
                                return new ResponseBase<Guid>(EStatusCode.CNULL, message: "请将电子病历添加到具体的目录下");
                            }

                            var iws = await _inpatientWardRepository.GetListAsync();
                            foreach (var item in iws)
                            {
                                var tcEntity = new TemplateCatalogue(GuidGenerator.Create(), item.WardName, model.Code, model.Sort, model.DeptCode, item.Id, model.ParentId);
                                entities.Add(tcEntity);
                            }
                        }
                        else
                        {
                            var departmentEntity = new TemplateCatalogue(GuidGenerator.Create(), model.Title, model.Code, model.Sort, model.DeptCode, model.InpatientWardId.Value, model.ParentId);
                            entities.Add(departmentEntity);
                        }

                        break;
                    }
                case ETemplateType.Personal:
                    {
                        if (model.DoctorCode.IsNullOrWhiteSpace() || model.DoctorName.IsNullOrWhiteSpace())
                        {
                            return new ResponseBase<Guid>(EStatusCode.ParameterIsMissing, message: "医生编码，医生名称必传");
                        }

                        var anyDoctor = (await _templateCatalogueRepository.GetQueryableAsync()).Any(w => w.DoctorCode == model.DoctorCode);
                        if (!anyDoctor)
                        {
                            var doctorEntity = new TemplateCatalogue(GuidGenerator.Create(), model.DoctorName, model.Code, model.Sort, model.DoctorCode, model.DoctorName);
                            entities.Add(doctorEntity);
                        }
                        else
                        {
                            var personalEntity = new TemplateCatalogue(GuidGenerator.Create(), model.Title, model.Code, model.Sort, model.DoctorCode, model.DoctorName, level, model.ParentId);
                            entities.Add(personalEntity);
                        }

                        break;
                    }
                default:
                    return new ResponseBase<Guid>(EStatusCode.CNULL, message: "未定义分类");
            }

            if (entities.Any())
            {
                await _templateCatalogueRepository.InsertManyAsync(entities);
                return new ResponseBase<Guid>(EStatusCode.COK, entities[0].Id);
            }
            return new ResponseBase<Guid>(EStatusCode.CFail, message: "新增分组失败");
        }

        #endregion

        /// <summary>
        /// 删除病历模板或分组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorCode"></param>
        /// <param name="classify"></param>
        /// <param name="templateType"></param>
        /// <param name="deptCode"></param>
        [UnitOfWork]
        //[Authorize(EMRPermissions.TemplateCatalogues.Delete)]
        public async Task<ResponseBase<Guid>> RemoveCatalogueItemAsync(Guid id, string doctorCode, EClassify classify, ETemplateType templateType, string deptCode)
        {
            var entity = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null) return new ResponseBase<Guid>(EStatusCode.CNULL);

            //权限控制
            var (refused, message) = await RefusedAsync(doctorCode, classify, templateType, deptCode);
            if (refused) return new ResponseBase<Guid>(EStatusCode.CFail, message: message);

            var count = (await _templateCatalogueRepository.GetQueryableAsync()).Where(w => w.ParentId == id).Count();
            if (count > 0) return new ResponseBase<Guid>(EStatusCode.C10001);
            await _templateCatalogueRepository.DeleteAsync(id);
            await _myXmlTemplateRepository.DeleteAsync(w => w.TemplateCatalogueId == id);

            return new ResponseBase<Guid>(EStatusCode.COK, id);
        }

        #region 病例管理（新增和更新）

        /// <summary>
        /// 新增/编辑病历
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [UnitOfWork]
        //[Authorize(EMRPermissions.TemplateCatalogues.Modify)]
        public async Task<ResponseBase<Guid>> ModifyEmrItemAsync(ModifyEmrItemDto model)
        {
            //权限控制
            var (refused, message) = await RefusedAsync(model.DoctorCode, model.Classify, model.TemplateType, model.DeptCode);
            if (refused) return new ResponseBase<Guid>(EStatusCode.CFail, message: message);

            if (model.Id.HasValue) return await UpdateEmrItemAsync(model);
            return await AddEmrItemAsync(model);
        }

        /// <summary>
        /// 更新电子病例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> UpdateEmrItemAsync(ModifyEmrItemDto model)
        {
            var entity = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id.Value);

            //校验病历目录编码是否存在
            var msg = await CheckCatalogueCodeIsExistAsync(model.Code, null, entity.Id);
            if (!string.IsNullOrEmpty(msg))
                return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), msg);

            entity.Update(model.Title, model.Code, model.ParentId.Value, model.Lv, model.Sort, model.IsEnabled);

            XmlTemplate xmlModel = null;
            using (_softDeleteFilter.Disable())
            {
                xmlModel = await (await _xmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.CatalogueId == model.CatalogueId);
            }
            if (xmlModel == null)
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL, entity.Id, $"导入的{model.Title}电子病例xml文件不存在，请选择一个存在的电子病例库的模板");
            }

            if (model.CatalogueId.Equals(entity.CatalogueId))
            {
                _logger.LogInformation("关联的病历库一样，不需要更新");
            }
            else
            {
                _logger.LogInformation($"更新了病历模板: from {entity.CatalogueId} to {model.CatalogueId}");

                var myxmlRet = await (await _myXmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.TemplateCatalogueId == model.Id);
                if (myxmlRet == null)
                {
                    var myXmlEntity = new MyXmlTemplate(GuidGenerator.Create(), xmlModel.TemplateXml, entity.Id);
                    await _myXmlTemplateRepository.InsertAsync(myXmlEntity);
                }
                else
                {
                    //2.更新最新记录
                    myxmlRet.UpdateTemplateXml(xmlModel.TemplateXml);
                    await _myXmlTemplateRepository.UpdateAsync(myxmlRet);
                }
            }

            Catalogue catalogue = null;
            using (_softDeleteFilter.Disable())
            {
                catalogue = await (await _catalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.CatalogueId);
            }
            if (catalogue == null)
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL, entity.Id, $"导入的{model.Title}电子病例xml文件不存在，请选择一个存在的电子病例库的模板");
            }
            entity.SetCatalogue(model.CatalogueId, catalogue.Title, model.OriginId);
            var ret = await _templateCatalogueRepository.UpdateAsync(entity);

            _capPublisher.Publish(TEMPLATE_ITEMS_SORT, model); //推送到另一个服务处理，避免同步卡的问题

            return new ResponseBase<Guid>(EStatusCode.COK, ret.Id);
        }

        /// <summary>
        /// 添加电子病例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> AddEmrItemAsync(ModifyEmrItemDto model)
        {
            //校验病历目录编码是否存在
            var msg = await CheckCatalogueCodeIsExistAsync(model.Code, "add", null);
            if (!string.IsNullOrEmpty(msg))
                return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), msg);

            ETemplateType templateType = ETemplateType.General;
            var deptCode = string.Empty;
            var doctorCode = string.Empty;
            var doctorName = string.Empty;

            int level = 0;
            Guid? parentId = null;
            if (model.ParentId.HasValue)
            {
                var data = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.ParentId && w.Classify == model.Classify);
                level = data.Lv + 1;
                parentId = data.Id;
            }

            switch (model.TemplateType)
            {
                case ETemplateType.General:
                    templateType = ETemplateType.General;
                    break;
                case ETemplateType.Department:
                    templateType = ETemplateType.Department;
                    deptCode = model.DeptCode;
                    break;
                case ETemplateType.Personal:
                    templateType = ETemplateType.Personal;
                    doctorCode = model.DoctorCode;
                    doctorName = model.DoctorName;
                    break;
                default:
                    break;
            }

            var entity = new TemplateCatalogue(
                id: GuidGenerator.Create(),
                title: model.Title,
                code: model.Code,
                parentId: model.ParentId,
                lv: level,
                sort: model.Sort,
                templateType: templateType,
                deptCode: deptCode,
                inpatientWardId: model.InpatientWardId,
                doctorCode: doctorCode,
                doctorName: doctorName,
                isEnabled: model.IsEnabled,
                originId: model.OriginId,
                classify: model.Classify);

            var catalogue = await (await _catalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.CatalogueId);
            entity.SetCatalogue(model.CatalogueId, catalogue.Title, model.OriginId);
            var ret = await _templateCatalogueRepository.InsertAsync(entity); //添加电子病例

            //查找病例库病例文件
            var xmlModel = await (await _xmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.CatalogueId == model.CatalogueId);
            if (xmlModel == null)
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL, ret.Id, $"导入的{model.Title}电子病例xml文件不存在，请选择一个存在的电子病例库的模板");
            }
            var xml = new MyXmlTemplate(GuidGenerator.Create(), xmlModel.TemplateXml, ret.Id);
            await _myXmlTemplateRepository.InsertAsync(xml); //插入xml病例文件

            _capPublisher.Publish(TEMPLATE_ITEMS_SORT, model); //推送到另一个服务处理，避免同步卡的问题

            return new ResponseBase<Guid>(EStatusCode.COK, ret.Id);
        }

        #endregion

        /// <summary>
        /// 获取电子病历模板信息
        /// </summary>
        /// <param name="templateCatalogueId"></param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.TemplateCatalogues.Detail)]
        public async Task<ResponseBase<MyXmlTemplateDto>> GetMyXmlTemplateAsync(Guid templateCatalogueId)
        {
            var xml = await (await _myXmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.TemplateCatalogueId == templateCatalogueId);
            var model = ObjectMapper.Map<MyXmlTemplate, MyXmlTemplateDto>(xml);
            model.TemplateXml = XmlUtil.CleanWatermark(model.TemplateXml).Item2;
            return new ResponseBase<MyXmlTemplateDto>(EStatusCode.COK, model);
        }

        /// <summary>
        /// 更新当前电子病历模板
        /// </summary>
        /// <see cref="MyXmlTemplateDto"/>
        /// <returns></returns> 
        //[Authorize(EMRPermissions.TemplateCatalogues.Modify)]
        public async Task<ResponseBase<Guid>> UpdateTemplateXmlAsync(UpdateMyXmlTemplateDto model)
        {
            //权限控制
            var (refused, message) = await RefusedAsync(model.DoctorCode, model.Classify, model.TemplateType, model.DeptCode);
            if (refused) return new ResponseBase<Guid>(EStatusCode.CFail, message: message);

            var xml = await (await _myXmlTemplateRepository.GetQueryableAsync()).Include(i => i.TemplateCatalogue).FirstOrDefaultAsync(w => w.Id == model.Id);
            var templateCatalogue = xml.TemplateCatalogue;

            //1.xml 留痕
            var xmlHistoryEntity = new XmlHistory(
                id: GuidGenerator.Create(),
                xmlId: xml.Id,
                xmlCategory: EXmlCategory.Template,
                emrXml: xml.TemplateXml,
                doctorCode: templateCatalogue == null ? "" : templateCatalogue.DoctorCode,
                doctorName: templateCatalogue == null ? "" : templateCatalogue.DoctorName);
            _ = await _xmlHistoryRepository.InsertAsync(xmlHistoryEntity);

            //2.更新记录
            xml.UpdateTemplateXml(model.TemplateXml);
            _ = await _myXmlTemplateRepository.UpdateAsync(xml);
            return new ResponseBase<Guid>(EStatusCode.COK, model.Id);
        }

        /// <summary>
        /// 获取另存为新的电子病例模板的目录结构树
        /// </summary> 
        /// <param name="doctorCode">医生编码 [获取个人模板的时候需要]</param>
        /// <param name="deptCode">科室编码 [获取科室模板的时候需要]</param> 
        /// <param name="doctorName">医生名称 [获取个人模板的时候需要]</param>
        /// <returns></returns>
        public async Task<ResponseBase<SaveAsTemplateTreeDto>> GetSaveAsTemplateAsync(string doctorCode, string deptCode, string doctorName)
        {
            SaveAsTemplateTreeDto data = new();

            var query = (await _templateCatalogueRepository.GetQueryableAsync())
                                .Where(w => w.Classify == EClassify.EMR && w.IsEnabled && !w.IsDeleted && !w.IsFile)
                                .AsQueryable();

            if (!deptCode.IsNullOrEmpty())
            {
                //没有找到任何目录就默认生成一个 
                {
                    var catalogues = await (await _templateCatalogueRepository.GetQueryableAsync()).Where(w => w.TemplateType == ETemplateType.Personal && w.DoctorCode == doctorCode).ToListAsync();
                    var parent = catalogues.FirstOrDefault(w => w.ParentId == null);
                    if (parent == null)
                    {
                        parent = await InitPersonalTemplateAsync(doctorCode, doctorName);
                        //return new ResponseBase<SaveAsTemplateTreeDto>(EStatusCode.CFail, message: "请先初始化个人模板");
                        _logger.LogInformation($"{doctorName}-{doctorCode} 初始化个人模板");
                    }
                    //如果二级目录下没有记录,则添加新的一个默认记录
                    var any = catalogues.Any(w => w.Lv == 1 && (w.ParentId != null || w.ParentId != Guid.Empty));
                    if (!any)
                    {
                        var entity = new TemplateCatalogue(
                           id: GuidGenerator.Create(),
                           title: "我的个人模板",
                           code: null,
                           sort: 1,
                           doctorCode: doctorCode,
                           doctorName: doctorName, //doctorName,
                           lv: 1,
                           parentId: parent.Id,
                           classify: EClassify.EMR);
                        await _templateCatalogueRepository.InsertAsync(entity, true);
                    }
                }

                var personal = await query
                                .Where(w => w.TemplateType == ETemplateType.Personal && w.DoctorCode == doctorCode.Trim() && w.Lv == 1)
                                .OrderBy(o => o.Sort)
                                .Select(s => new SaveAsTemplateCatalogueDto
                                {
                                    Id = s.Id,
                                    Title = s.Title,
                                    Code = s.Code,
                                    Sort = s.Sort,
                                    InpatientWardId = s.InpatientWardId,
                                    DeptCode = s.DeptCode,
                                    DoctorCode = s.DoctorCode,
                                    TemplateType = s.TemplateType
                                })
                                .ToListAsync();

                data.Personal = personal;
            }

            if (!doctorCode.IsNullOrEmpty())
            {
                data.Department = await query.Where(w => w.TemplateType == ETemplateType.Department && w.DeptCode == deptCode.Trim())
                                .OrderBy(o => o.Sort)
                                .Select(s => new SaveAsTemplateCatalogueDto
                                {
                                    Id = s.Id,
                                    Title = s.Title,
                                    Code = s.Code,
                                    Sort = s.Sort,
                                    InpatientWardId = s.InpatientWardId,
                                    DeptCode = s.DeptCode,
                                    DoctorCode = s.DoctorCode,
                                    TemplateType = s.TemplateType
                                })
                                .ToListAsync();
            }

            data.General = await query
                                .Where(w => w.TemplateType == ETemplateType.General)
                                .OrderBy(o => o.Sort)
                                .Select(s => new SaveAsTemplateCatalogueDto
                                {
                                    Id = s.Id,
                                    Title = s.Title,
                                    Code = s.Code,
                                    Sort = s.Sort,
                                    InpatientWardId = s.InpatientWardId,
                                    DeptCode = s.DeptCode,
                                    DoctorCode = s.DoctorCode,
                                    TemplateType = s.TemplateType
                                })
                                .ToListAsync();

            return new ResponseBase<SaveAsTemplateTreeDto>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 另存为模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [UnitOfWork]
        public async Task<ResponseBase<Guid>> SaveAsTemplateAsync(SaveAsTemplateDto model)
        {
            //权限控制
            var (refused, message) = await RefusedAsync(model.DoctorCode, model.Classify, model.TemplateType, model.DeptCode);
            if (refused) return new ResponseBase<Guid>(EStatusCode.CFail, message: message);

            //校验另存为模板编码是否存在
            var msg = await CheckCatalogueCodeIsExistAsync(model.Code, "add", null);
            if (!string.IsNullOrEmpty(msg))
                return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), "编码已存在，另存为失败");

            var parent = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.ParentId);
            if (parent == null) return new ResponseBase<Guid>(EStatusCode.CNULL, "选择的目录不存在");

            int sort = 0;
            var count = await (await _templateCatalogueRepository.GetQueryableAsync()).Where(w => w.ParentId == parent.Id).CountAsync();
            if (count > 0)
            {
                var exist = await (await _templateCatalogueRepository.GetQueryableAsync()).AnyAsync(w => w.ParentId == parent.Id && w.Title == model.Title.Trim());
                if (exist) return new ResponseBase<Guid>(EStatusCode.CFail, message: "标题名称重复，请换一个名称");

                var lvMax = await (await _templateCatalogueRepository.GetQueryableAsync()).Where(w => w.ParentId == parent.Id).MaxAsync(x => x.Sort);
                sort = lvMax + 1;
            }

            var templateCatalogueId = GuidGenerator.Create();
            var entity = new TemplateCatalogue(
               id: templateCatalogueId,
               title: model.Title,
               code: model.Code,
               parentId: model.ParentId,
               lv: (parent.Lv + 1),
               sort: sort,
               templateType: model.TemplateType,
               deptCode: model.DeptCode,
               inpatientWardId: model.InpatientWardId,
               doctorCode: model.DoctorCode,
               doctorName: model.DoctorName,
               isEnabled: true,
               originId: model.OriginId,
               classify: EClassify.EMR);

            var templateCatalogue = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.OriginalId);
            if (templateCatalogue != null)
            {
                entity.SetCatalogue(templateCatalogue.CatalogueId, templateCatalogue.CatalogueTitle, model.OriginId);
            }
            else
            {
                // CatalogueId 是病历原始模板的id，另存模板的时候不关联当前模板的id，而是直接关联原始模板id，此处做兼容处理
                var templateCatalogue1 = await (await _templateCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.CatalogueId == model.OriginalId);
                if (templateCatalogue1 != null)
                {
                    entity.SetCatalogue(templateCatalogue1.CatalogueId, templateCatalogue1.CatalogueTitle, model.OriginId);
                }
                else
                {
                    entity.SetOrigin(model.OriginId);
                    entity.SetCatalogue(model.OriginId);
                }
            }
            var whiteList = await (await _mergeTemplateWhiteListRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.TemplateId == model.OriginalId);
            if (whiteList != null)
            {
                _ = await _mergeTemplateWhiteListRepository.InsertAsync(new MergeTemplateWhiteList(entity.Id, entity.Title));
                _logger.LogInformation($"新增的模板包含留观病程的模板 模板内容：templateId = {entity.Id} , templateName = {entity.Title}");
            }

            var xml = new MyXmlTemplate(GuidGenerator.Create(), model.TemplateXml, templateCatalogueId);
            await _templateCatalogueRepository.InsertAsync(entity);
            await _myXmlTemplateRepository.InsertAsync(xml); //插入xml病例文件  
            return new ResponseBase<Guid>(EStatusCode.COK, templateCatalogueId);
        }

        /// <summary>
        /// 根据配置的医生获取相应的权限
        /// </summary>
        /// <param name="doctorCode">配置的医生信息</param>
        /// <param name="permissionCode">期望获取的权限类型</param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        private async Task<bool> PermissionAsync(string doctorCode, EPermissionCode permissionCode, string deptCode = "")
        {
            var list = await _permissionRepository.GetByDoctorCodeAsync(doctorCode, permissionCode, deptCode);
            if (list.Any()) return true;
            return false;
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="doctorCode">医生编码</param>
        /// <param name="classify">0=电子病历，1=文书</param>
        /// <param name="templateType">模板类型，0=通用，1=科室，2=个人</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        private async Task<(bool, string)> RefusedAsync(string doctorCode, EClassify classify, ETemplateType templateType, string deptCode)
        {
            if (classify == EClassify.Document) return (false, string.Empty);

            if (classify != EClassify.EMR) return (false, string.Empty);
            if (templateType == ETemplateType.General)
            {
                var flag = await PermissionAsync(doctorCode, EPermissionCode.GeneralTemplate);
                if (!flag) return new(true, "您没有操作通用模板的权限，请联系相关人员授权");
            }
            else if (templateType == ETemplateType.Department)
            {
                var flag = await PermissionAsync(doctorCode, EPermissionCode.DepartmentTemplate, deptCode);
                if (!flag) return (true, "您没有操作该科室模板的权限，请联系相关人员授权");
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// 医生编码获取权限
        /// </summary>
        /// <param name="doctorCode"></param>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public async Task<List<RefusedResultDto>> RefusedBydoctorCodeAsync(string doctorCode, string templateName)
        {
            var permissions = await _permissionRepository.GetPermissionByDoctorCodeAsync(doctorCode);
            var result = permissions
                .Where(p => p.Module == templateName)
                .Select(p => new RefusedResultDto { TemplateType = p.Module, PermissionName = p.PermissionTitle })
                .ToList();

            return result;
        }


        /// <summary>
        /// 权限控制(true=拒绝，false=允许)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<RefusedResponseDto>>> RefusedAsync(RefusedRequestDto model)
        {
            List<RefusedResponseDto> data = new();
            var templateTypes = new List<ETemplateType>() { ETemplateType.General, ETemplateType.Department, ETemplateType.Personal };
            foreach (var templateType in templateTypes)
            {
                var refused = await RefusedAsync(model.DoctorCode, model.Classify, templateType, model.DeptCode);
                data.Add(new RefusedResponseDto { Refused = refused.Item1, Message = refused.Item2, TemplateType = templateType });
            }
            return new ResponseBase<List<RefusedResponseDto>>(EStatusCode.COK, data: data);
        }

    }
}
