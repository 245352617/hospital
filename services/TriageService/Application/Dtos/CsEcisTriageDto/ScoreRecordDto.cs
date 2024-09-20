using System;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊分诊评分记录Dto
    /// </summary>
    public class ScoreRecordDto
    {
        public Guid Id { get; set; }
        
        public Guid PVID { get; set; }

        
        public Guid TID { get; set; }

        
        public DateTime? RecordDT { get; set; }

        
        public string ScoreType { get; set; }

        
        public string ScoreValue { get; set; }

        
        public string ScoreDescription { get; set; }

        
        public string ScoreContent { get; set; }

        
        public string Operator { get; set; }

        
        public int RecordType { get; set; }
    }
}