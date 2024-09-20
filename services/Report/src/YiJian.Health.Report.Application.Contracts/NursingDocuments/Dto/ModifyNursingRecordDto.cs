using System;
using System.Collections.Generic;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理记录
    /// </summary>
    public class ModifyNursingRecordDto : NursingRecordChangeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ModifyNursingRecordDto()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nursingDocumentId"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="signature"></param>
        public ModifyNursingRecordDto(Guid nursingDocumentId, int sheetIndex, string signature = "")
        {
            NursingDocumentId = nursingDocumentId;
            SheetIndex = sheetIndex;
            Consciousness = "";
            Field1 = "";
            Field2 = "";
            Field3 = "";
            Field4 = "";
            Field5 = "";
            Field6 = "";
            Field7 = "";
            Field8 = "";
            Field9 = "";
            BP = null;
            BP2 = null;
            HR = null;
            Mmol = null;
            P = null;
            R = null;
            SPO2 = null;
            T = null;
            IntakeDtos = new List<IntakeDto>();
            Pupil = new List<PupilDto>();
            RecordTime = DateTime.Now;
            Remark = "";
            Nurse = "";
            Signature = signature;
        }

        private string sheetName;
        /// <summary>
        /// 新建页索引名称
        /// </summary>
        public string SheetName
        {
            get { return sheetName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    sheetName = $"护理记录单{SheetIndex}";
                }
                else
                {
                    sheetName = value;
                }
            }
        }
    }

}
