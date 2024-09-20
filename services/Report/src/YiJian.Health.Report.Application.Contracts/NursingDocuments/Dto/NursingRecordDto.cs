using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Models;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理记录
    /// </summary>
    public class NursingRecordDto : NursingRecordChangeDto
    {
    }

    /// <summary>
    /// 更改护理单记录信息
    /// </summary>
    public class NursingRecordChangeDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 护理记录时间
        /// </summary> 
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date
        {
            get
            {
                return RecordTime.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time
        {
            get
            {
                return RecordTime.ToString("HH:mm");
            }
        }

        /// <summary>
        /// 体温（单位℃）
        /// </summary> 
        public string T { get; set; }

        /// <summary>
        /// 脉搏P(次/min)
        /// </summary> 
        public string P { get; set; }

        /// <summary>
        /// 心率(次/min)
        /// </summary> 
        public string HR { get; set; }

        /// <summary>
        /// 呼吸(次/min)
        /// </summary> 
        public string R { get; set; }

        /// <summary>
        /// 血压BP收缩压(mmHg)
        /// </summary> 
        public string BP { get; set; }

        /// <summary>
        /// 血压BP舒张压(mmHg)
        /// </summary> 
        public string BP2 { get; set; }

        /// <summary>
        /// 血氧饱和度SPO2 
        /// </summary> 
        public string SPO2 { get; set; }

        /// <summary>
        /// 指尖血糖(mmol/L)
        /// </summary>
        public virtual MmolDto Mmol { get; set; }

        /// <summary>
        /// 意识
        /// </summary> 
        [StringLength(50)]
        public string Consciousness { get; set; }

        /// <summary>
        /// 瞳孔对光反应
        /// </summary>
        public virtual List<PupilDto> Pupil { get; set; } = new List<PupilDto>();

        /// <summary>
        /// 瞳孔对光反应（左）
        /// </summary>
        public PupilDto PupilLeft
        {
            get
            {
                return Pupil.FirstOrDefault(w => w.PupilType == EPupilType.Left);
            }
        }

        /// <summary>
        ///  瞳孔对光反应（右）
        /// </summary>
        public PupilDto PupilRight
        {
            get
            {
                return Pupil.FirstOrDefault(w => w.PupilType == EPupilType.Right);
            }
        }

        /// <summary>
        /// 入量出量
        /// </summary>
        [JsonPropertyName("intakes")]
        public virtual List<IntakeDto> IntakeDtos { get; set; } = new List<IntakeDto>();

        /// <summary>
        /// 入量
        /// </summary>
        public virtual List<IntakeDto> InIntakes
        {
            get
            {
                return IntakeDtos.Where(w => w.IntakeType == EIntakeType.In).ToList();
            }
        }

        /// <summary>
        /// 出量
        /// </summary>
        public virtual List<IntakeDto> OutIntakes
        {
            get
            {
                return IntakeDtos.Where(w => w.IntakeType == EIntakeType.Out).ToList();
            }
        }

        /// <summary>
        /// 特殊护理记录
        /// </summary>
        public virtual List<SpecialNursingRecordDto> SpecialNursings { get; set; } = new List<SpecialNursingRecordDto>();

        /// <summary>
        /// 保留字段1 
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field1 { get; set; }

        /// <summary>
        /// 保留字段2 
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field2 { get; set; }

        /// <summary>
        /// 保留字段3  
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field3 { get; set; }

        /// <summary>
        /// 保留字段4 
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field4 { get; set; }

        /// <summary>
        /// 保留字段5 
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field5 { get; set; }

        /// <summary>
        /// 保留字段6  
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field6 { get; set; }

        /// <summary>
        /// 保留字段7
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field7 { get; set; }

        /// <summary>
        /// 保留字段8
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field8 { get; set; }

        /// <summary>
        /// 保留字段9
        /// </summary> 
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field9 { get; set; }

        /// <summary>
        /// 特殊情况记录
        /// </summary> 
        [StringLength(2000, ErrorMessage = "特殊情况记录字符不能超过2000字")]
        public string Remark { get; set; }

        /// <summary>
        /// 护士签名
        /// </summary> 
        [StringLength(50)]
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士签名
        /// </summary> 
        [StringLength(50)]
        public string Nurse { get; set; }

        /// <summary>
        /// 签名数据
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary> 
        [Required]
        public int SheetIndex { get; set; }

        /// <summary>
        /// 护理单Id
        /// </summary> 
        [Required]
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 动态六项的内容列表
        /// </summary>
        public List<DynamicDataJsonDto> DynamicDataList { get; set; } = new List<DynamicDataJsonDto>();

        /// <summary>
        /// 操作人Id
        /// </summary>
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 核对人名称
        /// </summary> 
        public string Collator { get; set; }

        /// <summary>
        /// 核对人code
        /// </summary>
        public string CollatorCode { get; set; }

        /// <summary>
        /// 核对人签名图片Base64
        /// </summary>
        public string CollatorImage { get; set; }

        /// <summary>
        /// 是否为统计项
        /// </summary>
        public bool IsStatistics { get; set; }

        /// <summary>
        /// 入量总量
        /// </summary>
        public string InIntakesTotal { get; set; }

        /// <summary>
        /// 出量总量
        /// </summary>
        public string OutIntakesTotal { get; set; }

        /// <summary>
        /// 出入量统计ID
        /// </summary>
        public Guid? IntakeStatisticsId { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime Begintime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime Endtime { get; set; }
    }

}
