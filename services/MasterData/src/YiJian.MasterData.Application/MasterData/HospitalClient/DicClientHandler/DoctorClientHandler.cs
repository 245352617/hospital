using DotNetCore.CAP;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.MasterData.Doctors;
using YiJian.MasterData.MasterData.HospitalClient.Doctors;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：员工字典信息
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/17 10:18:17
    /// </summary>
    public class DoctorClientHandler : MasterDataAppService, IDistributedEventHandler<DoctorEto>,
    ITransientDependency
    {
        private readonly IDoctorRepository _doctorRepository;
        private ICapPublisher _capPublisher;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="doctorRepository"></param>
        /// <param name="capPublisher"></param>
        public DoctorClientHandler(IDoctorRepository doctorRepository
            , ICapPublisher capPublisher)
        {
            _doctorRepository = doctorRepository;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(DoctorEto eventData)
        {
            if (eventData == null) return;
            List<DoctorEto> list = new List<DoctorEto>() { eventData };

            List<Doctor> etoList = ObjectMapper.Map<List<DoctorEto>, List<Doctor>>(list);
            //获取已存在的信息
            List<Doctor> doctorList = await _doctorRepository.ToListAsync();
            //新增的
            List<Doctor> addDoctors = etoList.Where(x => doctorList.All(a => a.DoctorCode != x.DoctorCode))
                .ToList();
            //删除的
            List<Doctor> deleteDoctors = doctorList.Where(x => etoList.All(a => a.DoctorCode != x.DoctorCode))
                .ToList();
            //修改
            List<Doctor> updateDoctors = new List<Doctor>();
            etoList.RemoveAll(addDoctors);
            etoList.RemoveAll(deleteDoctors);
            etoList.ForEach(x =>
            {
                Doctor data = doctorList.FirstOrDefault(g => x.DoctorCode == g.DoctorCode
                                                          && (x.DoctorName != g.DoctorName
                                                              || x.BranchCode != g.BranchCode
                                                              || x.BranchName != g.BranchName
                                                              || x.DeptCode != g.DeptCode
                                                              || x.DeptName != g.DeptName
                                                              || x.Sex != g.Sex
                                                              || x.Skill != g.Skill
                                                              || x.DoctorTitle != g.DoctorTitle
                                                              || x.Telephone != g.Telephone
                                                              || x.Introdution != g.Introdution
                                                              || x.AnaesthesiaAuthority !=
                                                              g.AnaesthesiaAuthority
                                                              || x.DrugAuthority != g.DrugAuthority
                                                              || x.SpiritAuthority != g.SpiritAuthority
                                                              || x.AntibioticAuthority != g.AntibioticAuthority
                                                              || x.PracticeCode != g.PracticeCode
                                                              || x.IsActive != g.IsActive));
                if (data != null)
                {
                    data.Update(x.DoctorName, x.BranchCode, x.BranchName, x.DeptCode, x.DeptName, x.Sex,
                        x.DoctorTitle, x.Telephone, x.Skill, x.Introdution, x.AnaesthesiaAuthority,
                        x.DrugAuthority, x.SpiritAuthority, x.AntibioticAuthority, x.PracticeCode, x.IsActive,
                        x.DoctorType);
                    updateDoctors.Add(data);
                }
            });
            if (addDoctors.Any())
            {
                await _doctorRepository.InsertManyAsync(addDoctors);
                List<DoctorEto> doctorEtos = new List<DoctorEto>();
                foreach (Doctor doctor in addDoctors)
                {
                    DoctorEto doctorEto = new DoctorEto();
                    doctorEto.DoctorName = doctor.DoctorName;
                    doctorEto.DeptCode = doctor.DeptCode;
                    doctorEtos.Add(doctorEto);
                }
                await _capPublisher.PublishAsync("sync.doctorInfo.to.recipe", doctorEtos);
            }

            if (updateDoctors.Any())
            {
                await _doctorRepository.UpdateManyAsync(updateDoctors);
            }

            if (deleteDoctors.Any())
            {
                await _doctorRepository.DeleteManyAsync(deleteDoctors);
            }
        }
    }
}
