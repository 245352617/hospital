using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Dto
{

    /// <summary>
    /// 医嘱信息参数
    /// </summary>
    public class ModifyDoctorsAdviceBaseDto : DoctorsAdviceRequestDto
    {
        public ModifyDoctorsAdviceBaseDto()
        {

        }

        //public ModifyDoctorsAdviceBaseDto(DoctorsAdviceRequestDto model, string recipeNo = "")
        //{
        //    PlatformType = model.PlatformType;
        //    PIID = model.PIID;
        //    PatientId = model.PatientId;
        //    PatientName = model.PatientName;
        //    ApplyDoctorCode = model.ApplyDoctorCode;lua
        //    ApplyDoctorName = model.ApplyDoctorName;
        //    ApplyDeptCode = model.ApplyDeptCode;
        //    ApplyDeptName = model.ApplyDeptName;
        //    TraineeCode = model.TraineeCode;
        //    TraineeName = model.TraineeName;
        //    IsChronicDisease = model.IsChronicDisease;
        //    Diagnosis = model.Diagnosis;

        //    if (PlatformType == ECIS.ShareModel.Enums.EPlatformType.PreHospital) RecipeNo = recipeNo; //只支持院前操作
        //}

        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary> 
        public EDoctorsAdviceItemType ItemType { get; set; }

        /// <summary>
        /// 构建医嘱分类
        /// </summary>
        public void BuildItemType(EDoctorsAdviceItemType itemType)
        {
            ItemType = ItemType;
        }

    }
}
