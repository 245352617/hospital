using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.DoctorsAdvices.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.Hospitals.Dto;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Hospitals
{
    /// <summary>
    /// 提供给医院的接口
    /// </summary>
    public class HospitalAppService : ApplicationService, IHospitalAppService
    {
        private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;

        /// <summary>
        /// 提供给医院的接口
        /// </summary>
        /// <param name="doctorsAdviceRepository"></param>
        /// <param name="prescriptionRepository"></param>
        public HospitalAppService(IDoctorsAdviceRepository doctorsAdviceRepository, IPrescriptionRepository prescriptionRepository)
        {
            _doctorsAdviceRepository = doctorsAdviceRepository;
            _prescriptionRepository = prescriptionRepository;
        }

        /// <summary>
        /// 医嘱支付状态变更
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<bool> PaymentStatusChangeAsync(PaymentStatusDto model)
        {
            var entity = await (await _doctorsAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
            if (entity == null) Oh.Error("查不到医嘱记录");
            entity.PayStatus = model.PayStatus;
            await _doctorsAdviceRepository.UpdateAsync(entity);
            return true;
        }

        /// <summary>
        /// 推送医嘱状态信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRecordStatusAsync(List<UpdateRecordStatusDto> model)
        {
            if (model == null) Oh.Error("医嘱状态不能为空记录");
            var visSerialNos = model.Select(s => s.VisSerialNo).ToList();

            var group = model.GroupBy(g => g.VisSerialNo);

            var updateAdvices = new List<DoctorsAdvice>();
            var updatePrescriptions = new List<Prescription>();

            foreach (var item in group)
            {
                var channelBillIds = item.Select(s => s.ChannelBillId).ToList();
                var prescriptions = await (await _prescriptionRepository.GetQueryableAsync()).AsTracking().Where(w => w.VisSerialNo == item.Key && channelBillIds.Contains(w.MyPrescriptionNo)).ToListAsync();
                if (!prescriptions.Any()) continue;

                var adviceids = prescriptions.Select(s => s.DoctorsAdviceId).ToList();
                var partAdvices = await (await _doctorsAdviceRepository.GetQueryableAsync()).AsTracking().Where(w => adviceids.Contains(w.Id)).ToListAsync();

                foreach (var p in prescriptions)
                {
                    var first = model.Where(w => w.VisSerialNo == p.VisSerialNo && w.ChannelBillId == p.MyPrescriptionNo).FirstOrDefault();
                    if (first == null || first.BillState == 0) continue;
                    var advice = partAdvices.FirstOrDefault(w => w.Id == p.DoctorsAdviceId);
                    if (advice == null) continue;

                    p.Update(first.HisBillId, first.VisSerialNo, first.PatientName, first.DeptCode, first.DoctorCode, first.BillState);
                    updatePrescriptions.Add(p);

                    var status = ERecipeStatus.Submitted;
                    switch (first.BillState)
                    {
                        case 1: //已缴费
                            status = ERecipeStatus.PayOff;
                            advice.PayStatus = EPayStatus.HavePaid;
                            break;
                        case 2: //已执行
                            status = ERecipeStatus.Executed;
                            break;
                        case -1://已作废
                            status = ERecipeStatus.Cancelled;
                            break;
                        default:
                            break;
                    }

                    advice.Status = status;
                    updateAdvices.Add(advice);
                }
            }

            await _prescriptionRepository.UpdateManyAsync(updatePrescriptions);
            await _doctorsAdviceRepository.UpdateManyAsync(updateAdvices);
            return true;

        }

    }
}
