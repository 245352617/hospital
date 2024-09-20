using System;
using System.ComponentModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Handover
{
    public class DoctorPatientStatistic: AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 医生交班id
        /// </summary>
        [Description("医生交班id")]
        public Guid DoctorHandoverId { get;private set; }
        /// <summary>
        /// 接诊总人数
        /// </summary>
        [Description("接诊总人数")]
        public int Total { get;private set; }
        /// <summary>
        /// I级  (病危人数)
        /// </summary>
        [Description("I级  (病危人数)")]
        public int ClassI { get;private set; }
        /// <summary>
        /// II级  (病重人数)
        /// </summary>
        [Description("II级  (病重人数)")]
        public int ClassII { get;private set; }
        /// <summary>
        /// III级
        /// </summary>
        [Description("III级")]
        public int ClassIII { get;private set; }
        /// <summary>
        /// IV级
        /// </summary>
        [Description("IV级")]
        public int ClassIV { get;private set; }
        /// <summary>
        /// 预术人数
        /// </summary>
        [Description("预术人数")]
        public int PreOperation { get;private set; }
        /// <summary>
        /// 现有病人数
        /// </summary>
        [Description("现有病人数")]
        public int ExistingDisease { get;private set; }
        /// <summary>
        /// 出科人数
        /// </summary>
        [Description("出科人数")]
        public int OutDept { get;private set; }
        /// <summary>
        /// 抢救人数
        /// </summary>
        [Description("抢救人数")]
        public int Rescue { get;private set; }
        /// <summary>
        /// 出诊人数
        /// </summary>
        [Description("出诊人数")]
        public int Visit { get;private set; }
        /// <summary>
        /// 死亡人数
        /// </summary>
        [Description("死亡人数")]
        public int Death { get;private set; }
        /// <summary>
        /// 心肺复苏人数
        /// </summary>
        [Description("心肺复苏人数")]
        public int CPR { get;private set; }
        /// <summary>
        /// 收住院人数
        /// </summary>
       [Description("收住院人数")]
        public int Admission { get;private set; }
        #region constructor

        public DoctorPatientStatistic(Guid id, Guid doctorHandoverId, int total, int classI, int classII, int classIII, int classIV, int preOperation, int existingDisease, int outDept, int rescue, int visit, int death, int cPR, int admission) : base(id)
        {

            DoctorHandoverId = doctorHandoverId;

            Modify(total, classI, classII, classIII, classIV, preOperation, existingDisease, outDept, rescue, visit, death, cPR, admission);
        }
        #endregion

        #region Modify

        public void Modify(int total, int classI, int classII, int classIII, int classIV, int preOperation, int existingDisease, int outDept, int rescue, int visit, int death, int cPR, int admission)
        {   
            Total = total;
            
            ClassI = classI;
            
            ClassII = classII;
            
            ClassIII = classIII;
            
            ClassIV = classIV;
            
            PreOperation = preOperation;
            
            ExistingDisease = existingDisease;
            
            OutDept = outDept;
            
            Rescue = rescue;
            
            Visit = visit;
            
            Death = death;
            
            CPR = cPR;
            
            Admission = admission;
            
        }
        #endregion

        #region constructor
        private DoctorPatientStatistic()
        {
            // for EFCore
        }
        #endregion
    }
}
