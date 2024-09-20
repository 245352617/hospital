using HisApiMockService.Models;
using CsvHelper;
using System.Globalization;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HisApiMockService.Controllers;

/// <summary>
/// 模拟药品视图内容（北大）
/// </summary>
[Route("api/ecis")]
public class DrugViewController : Controller
{
    private readonly SqliteDbContext _sqliteDbContext;

    public DrugViewController(SqliteDbContext sqliteDbContext)
    {
        _sqliteDbContext = sqliteDbContext;
    }

    /// <summary>
    /// 获取药品信息
    /// </summary>
    /// <returns></returns>
    [HttpPost("DrugsView")]
    public List<HISMedicine> GetDrugsView([FromBody]PKUHisMedicineRequest request)
    {
        if (request.Param1 != null)
        {
            var param = request.Param1;
            var query = _sqliteDbContext.HISMedicines.Where(w =>
            param.MedicineCode.Trim() == w.MedicineCode.ToString()
            && param.Specification.Trim() == w.Specification.Trim()
            && param.FactoryCode.Trim() == w.FactoryCode.ToString()).ToList();
        }

        if (request.Param2 != null)
        {
            var param = request.Param2;
            return _sqliteDbContext.HISMedicines.Where(w => param.InvId.Contains(w.InvId.ToString())).ToList();
        }

        if (request.Param3 != null)
        {
            var param = request.Param3;
            var query = _sqliteDbContext.HISMedicines.AsQueryable();
            if (param.EmergencySign == 1)
            {
                query = query.Where(w => param.EmergencySign == w.EmergencySign).AsQueryable();
            }

            query = query.Where(w => param.NameOrPyCode.Contains(w.PyCode) || param.NameOrPyCode.Contains(w.MedicineName));

            if (!string.IsNullOrEmpty(param.PharmacyCode))
            {
                query = query.Where(w => param.PharmacyCode.Trim() == w.PharmacyCode);
            }

            return query.ToList();
        }

        return new List<HISMedicine>();

    }

}
