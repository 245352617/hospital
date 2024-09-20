using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.Recipe;
using YiJian.Recipe.Preferences.Contracts;
using YiJian.Recipes.Preferences.Entities;
using static MasterDataService.GrpcMasterData;

namespace YiJian.DoctorsAdvices;

/// <summary>
/// 一次剂量操作运维工具
/// </summary>
[Authorize]
public class DosageOptAppService : RecipeAppService, IDosageOptAppService
{
    private readonly GrpcMasterDataClient _grpcMasterDataClient;
    private readonly IQuickStartMedicineRepository _quickStartMedicineRepository;
    private readonly ILogger<DosageOptAppService> _logger;

    /// <summary>
    /// 一次剂量操作运维工具
    /// </summary> 
    public DosageOptAppService(
        GrpcMasterDataClient grpcMasterDataClient,
        IQuickStartMedicineRepository quickStartMedicineRepository,
        ILogger<DosageOptAppService> logger)
    {
        _grpcMasterDataClient = grpcMasterDataClient;
        _quickStartMedicineRepository = quickStartMedicineRepository;
        _logger = logger;
    }

    /// <summary>
    /// 同步快速开嘱一次剂量配置的数据
    /// </summary>
    /// <returns></returns>
    public async Task<int> SyncQuickStartAdviceDosageAsync()
    {
        var data = await _grpcMasterDataClient.GetAllMedicinesAsync(new GetAllMedicinesRequest());
        var allDrugs = data.Medicines.ToList(); //视图过来的药品信息

        var drugs = await (await _quickStartMedicineRepository.GetQueryableAsync())
            .Where(w => w.HisDosageQty == 0)
            .ToListAsync();

        List<QuickStartMedicine> updateMedicines = new();

        //根据药品编码，规格，药厂，药房，获取唯一药品信息
        drugs.ForEach(x =>
        {
            var drug = allDrugs.FirstOrDefault(w => w.Code == x.MedicineCode && w.Specification == x.Specification && w.FactoryCode == x.FactoryCode && w.PharmacyCode == x.PharmacyCode);

            if (drug != null)
            {
                x.HisDosageQty = (decimal)drug.DosageQty;
                x.HisDosageUnit = drug.DosageUnit;
                x.HisUnit = drug.Unit;
                x.SetHisDosageQty();
                updateMedicines.Add(x);
            }
            else
            {
                //降为二级
                var drugLeve2 = allDrugs.FirstOrDefault(w => w.Code == x.MedicineCode && w.Specification == x.Specification);
                if (drugLeve2 != null)
                {
                    x.HisDosageQty = (decimal)drugLeve2.DosageQty;
                    x.HisDosageUnit = drugLeve2.DosageUnit;
                    x.HisUnit = drugLeve2.Unit;
                    x.Specification = drugLeve2.Specification;
                    x.SmallPackFactor = drugLeve2.SmallPackFactor;
                    x.SmallPackPrice = (decimal)drugLeve2.SmallPackPrice;
                    x.SmallPackUnit = drugLeve2.SmallPackUnit;
                    x.BigPackFactor = drugLeve2.BigPackFactor;
                    x.BigPackPrice = (decimal)drugLeve2.BigPackPrice;
                    x.BigPackUnit = drugLeve2.BigPackUnit;
                    x.Specification = drugLeve2.Specification;
                    x.FactoryCode = drugLeve2.FactoryCode;
                    x.FactoryName = drugLeve2.FactoryName;
                    x.SetHisDosageQty();
                    updateMedicines.Add(x);
                }
                else
                {
                    //降为三级
                    var drugLeve3 = allDrugs.FirstOrDefault(w => w.Code == x.MedicineCode);
                    if (drugLeve3 != null)
                    {
                        x.HisDosageQty = (decimal)drugLeve3.DosageQty;
                        x.HisDosageUnit = drugLeve3.DosageUnit;
                        x.HisUnit = drugLeve3.Unit;
                        x.Specification = drugLeve3.Specification;
                        x.SmallPackFactor = drugLeve3.SmallPackFactor;
                        x.SmallPackPrice = (decimal)drugLeve3.SmallPackPrice;
                        x.SmallPackUnit = drugLeve3.SmallPackUnit;
                        x.BigPackFactor = drugLeve3.BigPackFactor;
                        x.BigPackPrice = (decimal)drugLeve3.BigPackPrice;
                        x.BigPackUnit = drugLeve3.BigPackUnit;
                        x.Specification = drugLeve3.Specification;
                        x.FactoryCode = drugLeve3.FactoryCode;
                        x.FactoryName = drugLeve3.FactoryName;
                        x.PharmacyCode = drugLeve3.PharmacyCode;
                        x.PharmacyName = drugLeve3.PharmacyName;
                        x.SetHisDosageQty();
                        updateMedicines.Add(x);
                    }
                }
            }
        });

        if (updateMedicines.Any()) await _quickStartMedicineRepository.UpdateManyAsync(updateMedicines);

        return await Task.FromResult(updateMedicines.Count);
    }


}
