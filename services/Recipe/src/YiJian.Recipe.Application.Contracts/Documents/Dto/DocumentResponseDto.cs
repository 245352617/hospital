using System.Collections.Generic;
using System.Linq;
using YiJian.Recipe;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 打印返回数据结构
    /// </summary>
    public class DocumentResponseDto
    {
        public DocumentResponseDto()
        {
        }

        public DocumentResponseDto(List<MedicineAdviceDto> medicines)
        {
            Medicines = medicines;
        }

        public DocumentResponseDto(List<LisAdviceDto> lises)
        {
            Lises = lises;
        }

        public DocumentResponseDto(List<PacsAdviceDto> pacses)
        {
            Pacses = pacses;
        }

        public DocumentResponseDto(List<TreatAdviceDto> treats)
        {
            Treats = treats;
        }
        public DocumentResponseDto(List<GroupMedicineDto> group)
        {
            GroupMedicine = group;
        }
        /// <summary>
        /// 成组药品的显示
        /// </summary>
        public List<GroupMedicineDto> GroupMedicine { get; set; } = new List<GroupMedicineDto>();
        /// <summary>
        /// 药方医嘱
        /// </summary>
        public List<MedicineAdviceDto> Medicines { get; set; } = new List<MedicineAdviceDto>();

        /// <summary>
        /// 检验医嘱
        /// </summary>
        public List<LisAdviceDto> Lises { get; set; } = new List<LisAdviceDto>();

        // /// <summary>
        // /// 检验医嘱 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
        // /// </summary>
        // public List<LisAdviceDto> LisesDownsScreening { get; set; } = new List<LisAdviceDto>();

        /// <summary>
        /// 检验医嘱 14.新型冠状病毒RNA检测申请单
        /// </summary>
        public List<NovelCoronavirusRnaDto> LisesNovelCoronavirus { get; set; } = new List<NovelCoronavirusRnaDto>();

        // /// <summary>
        // /// 检验医嘱 13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
        // /// </summary>
        // public List<LisAdviceDto> LisesGeneScreening { get; set; } = new List<LisAdviceDto>();


        /// <summary>
        /// 检查医嘱
        /// </summary>
        public List<PacsAdviceDto> Pacses { get; set; } = new List<PacsAdviceDto>();

        /// <summary>
        /// 检查医嘱-附加单据，TCT细胞学检查申请单
        /// </summary>
        public List<PacsAdviceDto> PacsesTct { get; set; } = new List<PacsAdviceDto>();

        /// <summary>
        /// 检查医嘱-附加单据，病理检验申请单 
        /// </summary>
        public List<PacsAdviceDto> PacsesPathology { get; set; } = new List<PacsAdviceDto>();

        /// <summary>
        /// 检查医嘱-附加单据，门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用 
        /// </summary>
        public List<PacsAdviceDto> PacsesInsurance { get; set; } = new List<PacsAdviceDto>();

        /// <summary>
        /// 诊疗
        /// </summary>
        public List<TreatAdviceDto> Treats { get; set; } = new List<TreatAdviceDto>();

        /// <summary>
        /// 患者记录信息
        /// </summary>
        public List<AdmissionRecordDto> AdmissionRecords { get; set; } = new List<AdmissionRecordDto>();

        /// <summary>
        /// 病理条码信息
        /// </summary>
        public List<PacsItemNoDto> PacsItemNoDtos { get; set; } = new List<PacsItemNoDto>();

        /// <summary>
        /// 其他的信息
        /// </summary>
        public List<OtherInfoDto> OtherInfo { get; set; } = new List<OtherInfoDto>();

        /// <summary>
        /// 主要的汇总信息
        /// </summary>
        public List<MainInfoDto> MainInfo { get; set; }

        public DocumentResponseDto Push(List<MedicineAdviceDto> medicines)
        {
            if (Medicines == null) Medicines = new List<MedicineAdviceDto>();
            Medicines.AddRange(medicines);
            return this;
        }
        public DocumentResponseDto Push(List<GroupMedicineDto> group)
        {
            if (GroupMedicine == null) GroupMedicine = new List<GroupMedicineDto>();
            GroupMedicine.AddRange(group);
            return this;
        }

        public DocumentResponseDto Push(List<LisAdviceDto> lises)
        {
            if (Lises == null) Lises = new List<LisAdviceDto>();
            Lises.AddRange(lises);
            // LisesGeneScreening.AddRange(Lises.Where(x=>x.AddCard=="13"));
            // LisesDownsScreening.AddRange(Lises.Where(x=>x.AddCard=="15"));
            return this;
        }
        public DocumentResponseDto Push(List<NovelCoronavirusRnaDto> novel)
        {
            if (novel == null) novel = new List<NovelCoronavirusRnaDto>();
            LisesNovelCoronavirus.AddRange(novel);
            return this;
        }
        public DocumentResponseDto Push(List<PacsAdviceDto> pacses)
        {
            if (Pacses == null) Pacses = new List<PacsAdviceDto>();
            Pacses.AddRange(pacses);
            PacsesTct.AddRange(Pacses.Where(x => x.AddCard == "12"));
            PacsesPathology.AddRange(Pacses.Where(x => x.AddCard == "11"));
            //一级医保患者才需要 门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用 
            if (AdmissionRecords != null && "Faber_002,Faber_013,Faber_014,Faber_019".Contains(AdmissionRecords[0].ChargeType))//TODO 医保一档
            {
                PacsesInsurance.AddRange(Pacses.Where(x => x.AddCard == "16"));
            }
            return this;
        }

        public DocumentResponseDto Push(List<TreatAdviceDto> treats)
        {
            if (Treats == null) Treats = new List<TreatAdviceDto>();
            Treats.AddRange(treats);
            return this;
        }

        public DocumentResponseDto Push(AdmissionRecordDto admissionRecord)
        {
            if (AdmissionRecords == null) AdmissionRecords = new List<AdmissionRecordDto>();
            AdmissionRecords.Add(admissionRecord);
            return this;
        }

        public DocumentResponseDto Push(PacsItemNoDto pacsItemNoDto)
        {
            if (PacsItemNoDtos == null) PacsItemNoDtos = new List<PacsItemNoDto>();
            PacsItemNoDtos.Add(pacsItemNoDto);
            return this;
        }

        public DocumentResponseDto Push(OtherInfoDto otherInfo)
        {
            if (OtherInfo == null) OtherInfo = new List<OtherInfoDto>();
            OtherInfo.Add(otherInfo);
            return this;
        }

        public DocumentResponseDto Push(MainInfoDto mainInfo)
        {
            if (MainInfo == null) MainInfo = new List<MainInfoDto>();
            MainInfo.Add(mainInfo);
            return this;
        }
    }
}