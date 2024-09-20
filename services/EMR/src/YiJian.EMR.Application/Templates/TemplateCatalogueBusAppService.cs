using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.EMR.Templates.Dto;

namespace YiJian.EMR.Templates
{
    /// <summary>
    /// 病历模板
    /// </summary>
    public partial class TemplateCatalogueAppService
    {
        /// <summary>
        /// 模板目录排序
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("emr.template.items.sort")]
        public async Task TemplateItemsSortAsync(ModifyEmrItemDto model)
        {

            using var uow = UnitOfWorkManager.Begin();
            try
            {
                var list = await (await _templateCatalogueRepository.GetQueryableAsync()).Where(w => w.ParentId == model.ParentId && w.Sort > model.Sort).OrderBy(o => o.Sort).ToListAsync();

                if (!list.Any()) return;

                var sort = model.Sort + 1;
                list.ForEach(x =>
                {
                    x.Sort = sort++;
                });
                await _templateCatalogueRepository.UpdateManyAsync(list);
                 
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"模板目录排序异常:{ex.Message},请求参数：{JsonSerializer.Serialize(model)}");
                await uow.RollbackAsync();
                throw;
            }


           
        }



        ///// <summary>
        ///// 测试创建绑定会诊记录信息
        ///// </summary>
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task TestCreateBindConsultationRecordAsync()
        //{
        //    var eto = new BindConsultationRecordEto
        //    {
        //        AdmissionTime = DateTime.Now,
        //        DeptCode = "1001",
        //        DeptName = "急诊外科",
        //        Diagnosis = "我是诊断信息，这个是一个测试数据",
        //        DoctorCode = "3973",
        //        DoctorName = "谭强锋",
        //        OrgCode = "1",
        //        PatientName = "卢俊义",
        //        PatientNo = "4927733",
        //        Piid = new Guid("2EF366F8-6146-1B5C-A275-3A06FCCDCF09"),
        //        RegisterSerialNo = "5894873",
        //        VisitNo = "10677368",
        //        Title = "院内会诊记录", 
        //        Data = new ConsultationRecordDataEto
        //        {
        //            MedicalInfo = new MedicalInfoEto
        //            {
        //                ConsultationApplyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        //                ConsultationCategory = "普通会诊",
        //                ConsultationDept = "急诊外科",
        //                ConsultationRecord = "没有太多问题，回家睡一觉就好了",
        //                ConsultationResume = "多喝水，不要熬夜",
        //                ConsultationTime = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分"), 

        //            },
        //            PatientInfo = new PatientInfoEto
        //            {
        //                Age = "三十岁",
        //                ContactsPhone = "18510909878",
        //                PatientName = "卢俊义",
        //                Narrationname = "头疼，腰酸背痛，两眼昏花，还有点想吐，呼吸急促",
        //                Presentmedicalhistory = "过渡疲劳",
        //                SexName = "男",
        //                TriageDeptName = "急诊外科",
        //                VisitNo = "10677368", 
        //            }
        //        }

        //    }; 
        //    await _capPublisher.PublishAsync("yijian.emr.bindConsultationRecord", eto); 
        //    await Task.CompletedTask;
        //} 

    }
}
