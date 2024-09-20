using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.MasterData.External.LongGang.Medicines;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.External.LongGang;

public class MedicineEventHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<MedicinesEto>,
       ITransientDependency
{

    private readonly IDictionariesAppService _dictionariesAppService;
    private readonly IMedicineUsageRepository _medicineUsageRepository;
    private readonly IMedicineFrequencyRepository _medicineFrequencyRepository;

    public ILogger<MedicineEventHandler> _logger { get; set; }
    public IMedicineRepository MedicineRepository { get; set; }


    private MedicineFrequency dict_Frequency = null;
    private MedicineUsage dict_Usage = null;
    public MedicineEventHandler(
         IMedicineUsageRepository medicineUsageRepository,
         IMedicineFrequencyRepository medicineFrequencyRepository,
         IDictionariesAppService dictionariesAppService)
    {
        _medicineUsageRepository = medicineUsageRepository;
        _medicineFrequencyRepository = medicineFrequencyRepository;
        _dictionariesAppService = dictionariesAppService;
    }
    public async Task HandleEventAsync(MedicinesEto eventData)
    {
        var uow = UnitOfWorkManager.Begin();

        try
        {
            if (eventData == null) return;
            _logger.LogInformation("RabbitMQ药品信息内容:{0}",JsonConvert.SerializeObject(eventData));
            var selectDrug = await MedicineRepository.FindAsync(x => x.PharmacyCode == eventData.Storage && x.MedicineCode == eventData.DrugCode && x.FactoryCode == eventData.ProducerCode && x.Specification == eventData.Specs);
            if (!string.IsNullOrEmpty(eventData.FrequencyCode))
            {
                dict_Frequency = await _medicineFrequencyRepository.FindAsync(x => x.FrequencyCode == eventData.FrequencyCode);
            }
            if (!string.IsNullOrEmpty(eventData.DrugChannel))
            {
                dict_Usage = await _medicineUsageRepository.FindAsync(x => x.UsageName == eventData.DrugChannel);
            }
            var dict_Pharmacy = await _dictionariesAppService.GetDictionariesGroupAsync("LongGangPharmacy");
            
                //1.新增
                if (selectDrug==null)
                {
                    if (eventData.DrugType=="0")
                    {
                        var medicine = new Medicine();
                        medicine=   MedicineMapping(dict_Pharmacy, eventData, medicine);
                        await MedicineRepository.InsertAsync(medicine);
                        _logger.LogInformation($"{ DateTime.Now}新增药品数据成功{JsonConvert.SerializeObject(medicine)}");
                    }
                }
                //2.修改 or //3.删除
                else
                {
                    if (eventData.UseFlag != (selectDrug.IsDeleted == true ? "1" : "0"))
                    {
                    selectDrug.IsActive = eventData.UseFlag == "0" ? true : false; ;
                        await MedicineRepository.UpdateAsync(selectDrug);
                        _logger.LogInformation($"{ DateTime.Now}删除药品数据成功{JsonConvert.SerializeObject(selectDrug)}");
                    }
                    else
                    {

                    selectDrug = MedicineMapping(dict_Pharmacy, eventData, selectDrug);
                        await MedicineRepository.UpdateAsync(selectDrug);
                        _logger.LogInformation($"{ DateTime.Now}更新药品数据成功{JsonConvert.SerializeObject(selectDrug)}");
                    }

                }



            await uow.CompleteAsync();

        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            await uow.RollbackAsync();
        }
    }

    private  Medicine MedicineMapping(Dictionary<string, List<DictionariesDto>> dict_Pharmacy, MedicinesEto drug, Medicine medicine)
    {
        medicine.MedicineCode = drug.DrugCode;
        medicine.MedicineName = drug.TradeName;
        medicine.ScientificName = drug.FormalName;
        medicine.Alias = drug.OtherName;
        medicine.AliasPyCode = drug.OtherName.FirstLetterPY();
        medicine.AliasWbCode = drug.OtherName.FirstLetterWB();
        medicine.WbCode = drug.TradeName.FirstLetterWB();
        medicine.PyCode = drug.SpellCode;
        medicine.DefaultDosage = double.Parse(drug.OnceDose);
        medicine.DosageQty = decimal.Parse(drug.BaseDose);
        medicine.DosageUnit = drug.DoseUnit;
        medicine.Unit = drug.MinUnit;
        medicine.Price = drug.MinPackageIndicator == "1" ? decimal.Parse(drug.RetailPrice) / drug.MinimumPacking : decimal.Parse(drug.RetailPrice);
        medicine.BigPackPrice = decimal.Parse(drug.RetailPrice);
        medicine.BigPackUnit = drug.DrugUnit;
        medicine.SmallPackPrice = drug.MinPackageIndicator == "1" ? decimal.Parse(drug.RetailPrice) / drug.MinimumPacking : decimal.Parse(drug.RetailPrice);
        medicine.SmallPackUnit = drug.PackUnit;
        medicine.ChildrenPrice = drug.MinPackageIndicator == "1" ? decimal.Parse(drug.RetailPrice) * decimal.Parse("1.3") / drug.MinimumPacking : decimal.Parse(drug.RetailPrice) * decimal.Parse("1.3");
        medicine.FixPrice = decimal.Parse(drug.PurchasePrice);
        medicine.RetPrice = decimal.Parse(drug.RetailPrice);
        medicine.InsuranceCode = int.Parse(drug.InsuranceCode);
        medicine.FactoryCode = drug.ProducerCode;
        medicine.FactoryName = drug.ProducerName;
        medicine.IsSkinTest = drug.SkinTestSign == "0" ? false : true;
        medicine.ToxicLevel = int.Parse(drug.DrugAttributes);
        medicine.AntibioticLevel = string.IsNullOrEmpty(drug.AntibioticGrade)?0:int.Parse(drug.AntibioticGrade);
        medicine.IsLimited = string.IsNullOrEmpty(drug.LimitType) == true ? false :
                                    drug.LimitType == "0" ? false : true;
        medicine.LimitedNote = drug.LimitData;
        medicine.Specification = drug.Specs;
        medicine.DosageForm = drug.DoseModelCode;
        medicine.IsActive =drug.UseFlag=="0"?true:false;
        medicine.IsDeleted = drug.UseFlag == "0" ? false : true;
        medicine.PharmacyCode = drug.Storage;
        medicine.PharmacyName = dict_Pharmacy.Count > 0
            ? dict_Pharmacy["LongGangPharmacy"]
                .FirstOrDefault(d => d.DictionariesCode == drug.Storage)?.DictionariesName
            : "";
        medicine.ExecDeptCode = drug.Storage;
        medicine.ExecDeptName = dict_Pharmacy.Count > 0
            ? dict_Pharmacy["LongGangPharmacy"]
                .FirstOrDefault(d => d.DictionariesCode == drug.Storage)?.DictionariesName
            : "";
        medicine.AntibioticPermission = string.IsNullOrEmpty(drug.AntibioticSign) == true ? 0 :
                                   int.Parse(drug.AntibioticSign);
        medicine.PrescriptionPermission = drug.OctFlag??0;
        medicine.BaseFlag = ConvertBaseFlag( drug.Extend2);
        medicine.MedicalInsuranceCode = drug.MedicalInsuranceCode;
        medicine.FrequencyCode = drug.FrequencyCode;
        medicine.FrequencyName = dict_Frequency?.FrequencyName;
        medicine.UsageName = drug.DrugChannel;
        medicine.UsageCode = dict_Usage?.UsageCode;
        medicine.InsuranceName = drug.InsuranceType;
        medicine.MedicineProperty = ConvertMedicineProperty(drug.DrugType);
        medicine.SmallPackFactor = drug.MinimumPacking;
        medicine.PlatformType = 0;
        return medicine;
    }

    //0 西药 1 中成药 2 中草药
    private string ConvertMedicineProperty(string drugType)
    {
        switch (drugType)
        {
            case "0":
                return "西药";
            case "1":
                return "中成药";
            case "2":
                return "中草原药";
            default:
                return "其它";
        }
    }

    //0 西药 1 中成药 2 中草药
    private string ConvertBaseFlag(string baseFlag)
    {
        switch (baseFlag)
        {
            case "00":
                return "普通";
            case "01":
                return "国基";
            case "02":
                return "省基";
            case "03":
                return "市基";
            case "04":
                return "基药";
            case "05":
                return "中草药";
            case "06":
                return "非基药";
            default:
                return "其它";
        }
    }

}
