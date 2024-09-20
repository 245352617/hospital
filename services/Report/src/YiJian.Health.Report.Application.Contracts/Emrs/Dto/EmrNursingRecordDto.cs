using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;
using YiJian.Health.Report.NursingDocuments.Dto;

namespace YiJian.Health.Report.Emrs.Dto
{
    /// <summary>
    /// 护理记录
    /// </summary>
    public class EmrNursingRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 护理记录时间
        /// </summary> 
        public DateTime RecordTime { get; set; }

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
        public virtual MmolBaseDto Mmol { get; set; }

        /// <summary>
        /// 意识
        /// </summary> 
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
        public virtual List<IntakeDto> Intakes { get; set; } = new List<IntakeDto>();

        /// <summary>
        /// 入量
        /// </summary>
        public virtual List<IntakeDto> InIntakes
        {
            get
            {
                return Intakes.Where(w => w.IntakeType == EIntakeType.In).ToList();
            }
        }

        /// <summary>
        /// 出量
        /// </summary>
        public virtual List<IntakeDto> OutIntakes
        {
            get
            {
                return Intakes.Where(w => w.IntakeType == EIntakeType.Out).ToList();
            }
        }


        /// <summary>
        /// 特殊情况记录
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        /// 操作护士
        /// </summary>  
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士签名
        /// </summary> 
        public string Nurse { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary> 
        public int SheetIndex { get; set; }

        /// <summary>
        /// 护理单Id
        /// </summary> 
        public Guid NursingDocumentId { get; set; }

    }
}
