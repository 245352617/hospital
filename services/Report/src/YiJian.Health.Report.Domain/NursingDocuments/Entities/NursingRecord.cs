using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// 护理记录
    /// </summary>
    [Comment("护理记录")]
    public class NursingRecord : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 护理记录
        /// </summary>
        private NursingRecord()
        {

        }

        /// <summary>
        /// 护理记录
        /// </summary>
        public NursingRecord(
            Guid id,
            DateTime recordTime,
            string t,
            string p,
            string hr,
            string r,
            string bp,
            string bp2,
            string spo2,
            [CanBeNull] string consciousness,
            [CanBeNull] string field1,
            [CanBeNull] string field2,
            [CanBeNull] string field3,
            [CanBeNull] string field4,
            [CanBeNull] string field5,
            [CanBeNull] string field6,
            [CanBeNull] string field7,
            [CanBeNull] string field8,
            [CanBeNull] string field9,
            [CanBeNull] string remark,
            [CanBeNull] string nurseCode,
            [CanBeNull] string nurse,
            int sheetIndex,
            Guid nursingDocumentId,
            [CanBeNull] string signature,
            [CanBeNull] string collator,
            [CanBeNull] string collatorCode,
            [CanBeNull] string collatorImage)
        {
            Id = id;
            RecordTime = recordTime;
            T = t;
            P = p;
            HR = hr;
            R = r;
            BP = bp;
            BP2 = bp2;
            SPO2 = spo2;
            Consciousness = consciousness;
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
            Field4 = field4;
            Field5 = field5;
            Field6 = field6;
            Field7 = field7;
            Field8 = field8;
            Field9 = field9;
            Remark = remark;
            NurseCode = nurseCode;
            Nurse = nurse;
            NursingDocumentId = nursingDocumentId;
            SheetIndex = sheetIndex;
            Signature = signature;
            Collator = collator;
            CollatorCode = collatorCode;
            CollatorImage = collatorImage;
        }

        /// <summary>
        /// 护理记录时间
        /// </summary>
        [Comment("护理记录时间")]
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 体温（单位℃）
        /// </summary>
        [Comment("体温（单位℃）")]
        public string T { get; set; }

        /// <summary>
        /// 脉搏P(次/min)
        /// </summary>
        [Comment("脉搏P(次/min)")]
        public string P { get; set; }

        /// <summary>
        /// 心率(次/min)
        /// </summary>
        [Comment("心率(次/min)")]
        public string HR { get; set; }

        /// <summary>
        /// 呼吸(次/min)
        /// </summary>
        [Comment("呼吸(次/min)")]
        public string R { get; set; }

        /// <summary>
        /// 血压BP收缩压(mmHg)
        /// </summary>
        [Comment("血压BP收缩压(mmHg)")]
        public string BP { get; set; }

        /// <summary>
        /// 血压BP舒张压(mmHg)
        /// </summary>
        [Comment("血压BP舒张压(mmHg)")]
        public string BP2 { get; set; }

        /// <summary>
        /// 血氧饱和度SPO2 
        /// </summary>
        [Comment("血氧饱和度SPO2 %")]
        public string SPO2 { get; set; }

        /// <summary>
        /// 指尖血糖(mmol/L)
        /// </summary>
        public virtual Mmol Mmol { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        [Comment("意识")]
        [StringLength(50)]
        public string Consciousness { get; set; }

        /// <summary>
        /// 瞳孔对光反应
        /// </summary>
        public virtual List<Pupil> Pupil { get; set; } = new List<Pupil>();

        /// <summary>
        /// 入量出量
        /// </summary>
        public virtual List<Intake> Intakes { get; set; } = new List<Intake>();

        /// <summary>
        /// 特殊护理记录
        /// </summary>
        public virtual List<SpecialNursingRecord> SpecialNursings { get; set; } = new List<SpecialNursingRecord>();

        /// <summary>
        /// 保留字段1（用json 存储，前段定义好字段名称和值）
        /// </summary>
        [Comment("保留字段1")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field1 { get; set; }

        /// <summary>
        /// 保留字段2（用json 存储，前段定义好字段名称和值）
        /// </summary>
        [Comment("保留字段2")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field2 { get; set; }

        /// <summary>
        /// 保留字段3 （用json 存储，前段定义好字段名称和值）
        /// </summary>
        [Comment("保留字段3")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field3 { get; set; }

        /// <summary>
        /// 保留字段4（用json 存储，前段定义好字段名称和值）
        /// </summary>
        [Comment("保留字段4")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field4 { get; set; }

        /// <summary>
        /// 保留字段5（用json 存储，前段定义好字段名称和值）
        /// </summary>
        [Comment("保留字段5")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field5 { get; set; }

        /// <summary>
        /// 保留字段6 （用json 存储，前段定义好字段名称和值）
        /// </summary>
        [Comment("保留字段6")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field6 { get; set; }

        /// <summary>
        /// 保留字段7
        /// </summary>
        [Comment("保留字段7")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field7 { get; set; }

        /// <summary>
        /// 保留字段8
        /// </summary>
        [Comment("保留字段8")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field8 { get; set; }

        /// <summary>
        /// 保留字段9
        /// </summary>
        [Comment("保留字段9")]
        [StringLength(100, ErrorMessage = "字符不能超过100字")]
        public string Field9 { get; set; }

        /// <summary>
        /// 特殊情况记录
        /// </summary>
        [Comment("特殊情况记录")]
        [StringLength(2000, ErrorMessage = "特殊情况记录字符不能超过2000字")]
        public string Remark { get; set; }

        /// <summary>
        /// 操作护士
        /// </summary> 
        [Comment("操作护士")]
        [StringLength(50)]
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士签名
        /// </summary>
        [Comment("护士签名")]
        [StringLength(50)]
        public string Nurse { get; set; }

        [Comment("签名图片")]
        [DataType(DataType.Text)]
        public string Signature { get; set; }

        /// <summary>
        /// 核对人名称
        /// </summary> 
        [Comment("核对人名称")]
        [StringLength(50)]
        public string Collator { get; set; }

        /// <summary>
        /// 核对人code
        /// </summary>
        [Comment("核对人code")]
        [StringLength(50)]
        public string CollatorCode { get; set; }

        /// <summary>
        /// 核对人签名图片Base64
        /// </summary>
        [Comment("核对人签名图片")]
        [DataType(DataType.Text)]
        public string CollatorImage { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary>
        [Comment("新建页索引")]
        public int SheetIndex { get; set; }

        /// <summary>
        /// 护理单Id
        /// </summary>
        [Comment("护理单Id")]
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 护理单
        /// </summary>
        public virtual NursingDocument NursingDocument { get; set; }

        /// <summary>
        /// 患者特殊护理特征记录
        /// </summary>
        public virtual List<Characteristic> Characteristic { get; set; } = new List<Characteristic>();

        public void Update(
            DateTime recordTime,
            string t,
            string p,
            string hr,
            string r,
            string bp,
            string bp2,
            string spo2,
            [CanBeNull] string consciousness,
            [CanBeNull] string field1,
            [CanBeNull] string field2,
            [CanBeNull] string field3,
            [CanBeNull] string field4,
            [CanBeNull] string field5,
            [CanBeNull] string field6,
            [CanBeNull] string field7,
            [CanBeNull] string field8,
            [CanBeNull] string field9,
            [CanBeNull] string remark,
            [CanBeNull] string signature,
            [CanBeNull] string collator,
            [CanBeNull] string collatorCode,
            [CanBeNull] string collatorImage)
        {
            RecordTime = recordTime;
            T = t;
            P = p;
            HR = hr;
            R = r;
            BP = bp;
            BP2 = bp2;
            SPO2 = spo2;
            Consciousness = consciousness;
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
            Field4 = field4;
            Field5 = field5;
            Field6 = field6;
            Field7 = field7;
            Field8 = field8;
            Field9 = field9;
            Remark = remark;
            Signature = signature;
            Collator = collator;
            CollatorCode = collatorCode;
            CollatorImage = collatorImage;
        }

        /// <summary>
        /// 签名 
        /// </summary>
        public void Sign(string signature)
        {
            Signature = signature;
        }

    }
}
