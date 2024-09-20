using System;
using System.Collections.Generic;
using YiJian.ECIS.ShareModel.Utils;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 新增诊疗项操作
    /// </summary>
    public class AddTreatDto
    {
        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        public ModifyDoctorsAdviceBaseDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 诊疗项
        /// </summary>
        public TreatDto Items { get; set; }

        /// <summary>
        /// 急诊的诊疗项频次信息置空
        /// </summary> 
        public void SetFrequencyNull()
        {
            if (DoctorsAdvice.PlatformType == ECIS.ShareModel.Enums.EPlatformType.EmergencyTreatment)
            {
                Items.FrequencyCode = "";
                Items.FrequencyName = "";
            }
        }

        /// <summary>
        /// 是否是附加项，默认是false, 当是附加项的时候需要单独处理
        /// </summary>
        public bool IsAddition { get; set; } = false;

    }

    /// <summary>
    /// 患者信息(龙岗中心医院的算法)
    /// </summary>
    public class PatientInfoDto
    {
        /// <summary>
        /// 患者年龄
        /// </summary>
        public int? PatientAge { get; set; }

        /// <summary>
        /// 患者身份证
        /// </summary>
        public string PatientIDCard { get; set; }

        /// <summary>
        /// 是否是儿童
        /// </summary>
        /// <returns></returns>
        public bool IsChildren()
        {
            try
            {
                if (!PatientIDCard.IsNullOrEmpty())
                {
                    var idcard = IDCard.IDCard.Verify(PatientIDCard);
                    if (idcard.IsVerifyPass) return idcard.DayOfBirth.IsChildren();
                }
                if (PatientAge.HasValue && (PatientAge.Value > 0 && PatientAge < 6)) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }


}
