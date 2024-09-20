namespace YiJian.Handover
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Volo.Abp.Validation;

    [Serializable]
    public class DoctorPatientStatisticUpdate
    {            
        public Guid Id { get; set; }

        /// <summary>
        /// 接诊总人数
        /// </summary>
        
        public int  Total { get; set; }

        /// <summary>
        /// I级  (病危人数)
        /// </summary>
        
        public int  ClassI { get; set; }

        /// <summary>
        /// II级  (病重人数)
        /// </summary>
        
        public int  ClassII { get; set; }

        /// <summary>
        /// III级
        /// </summary>
        
        public int  ClassIII { get; set; }

        /// <summary>
        /// IV级
        /// </summary>
        
        public int  ClassIV { get; set; }

        /// <summary>
        /// 预术人数
        /// </summary>
        
        public int  PreOperation { get; set; }

        /// <summary>
        /// 现有病人数
        /// </summary>
        
        public int  ExistingDisease { get; set; }

        /// <summary>
        /// 出科人数
        /// </summary>
        
        public int  OutDept { get; set; }

        /// <summary>
        /// 抢救人数
        /// </summary>
        
        public int  Rescue { get; set; }

        /// <summary>
        /// 出诊人数
        /// </summary>
        
        public int  Visit { get; set; }

        /// <summary>
        /// 死亡人数
        /// </summary>
        
        public int  Death { get; set; }

        /// <summary>
        /// 心肺复苏人数
        /// </summary>
        
        public int  CPR { get; set; }

        /// <summary>
        /// 收住院人数
        /// </summary>
        
        public int  Admission { get; set; }
    }
}