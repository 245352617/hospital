using HisApiMockService.Models;
using HisApiMockService.Models.Advices;
using HisApiMockService.Models.Medicals;
using HisApiMockService.Models.Stores;
using Microsoft.EntityFrameworkCore;

namespace HisApiMockService
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<PatientDto> RegisterPatient { get; set; }        
        public DbSet<CreatePatientResponseDto> BuildPatient { get; set; }
        public DbSet<DrugStockQueryResponse> DrugStock { get; set; }
        
        public DbSet<UpdateRecordStatusRequest> RecordStatusRequest { get; set; }
        
        public DbSet<SendMedicalInfoRequest> SendMedicalInfo { get; set; }
        
        public DbSet<QueryMedicalInfoResponse> MedicalInfoStatus { get; set; }

        public DbSet<HISMedicine> HISMedicines { get; set; }

        protected readonly IConfiguration _configuration;

        public SqliteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
             options.UseSqlite(_configuration.GetConnectionString("WebApiDatabase") ?? string.Empty); 
        }
    }
}