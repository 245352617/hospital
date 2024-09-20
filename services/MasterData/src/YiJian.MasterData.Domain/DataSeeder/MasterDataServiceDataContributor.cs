using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using YiJian.MasterData;
using YiJian.MasterData.DictionariesMultitypes;
using YiJian.MasterData.DictionariesTypes;

namespace YiJian.Recipe;

/// <summary>
/// 种子数据
/// </summary>
public class MasterDataServiceDataContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IDictionariesRepository _repository;
    private readonly IDictionariesTypeRepository _typeRepository;
    private readonly IDictionariesMultitypeRepository _multitypeRepository;

    public MasterDataServiceDataContributor(IDictionariesRepository repository, IDictionariesTypeRepository typeRepository, IDictionariesMultitypeRepository multitypeRepository)
    {
        _repository = repository;
        _typeRepository = typeRepository;
        _multitypeRepository = multitypeRepository;
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        await CreateDataAsync();
        await CreateMultitypeAsync();
    }

    public async Task CreateDataAsync()
    {
        var list = new List<Dictionaries>();
        var dictList = await _repository
            .ToListAsync();
        //添加列表设置种子数据
        if (dictList.All(w => w.DictionariesTypeCode != "ListSettings"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries()
                {
                    DictionariesCode = "HistoryVisitArea",
                    DictionariesName = "历史就诊列表",
                    DictionariesTypeCode = "ListSettings", DictionariesTypeName = "列表设置",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "VisitArea",
                    DictionariesName = "就诊区列表",
                    DictionariesTypeCode = "ListSettings", DictionariesTypeName = "列表设置",
                    Status = true
                }
            });
        }

        //添加床位配置种子数据
        if (dictList.All(w => w.DictionariesTypeCode != "BedSettings"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries()
                {
                    DictionariesCode = "ObservationArea",
                    DictionariesName = "留观区",
                    DictionariesTypeCode = "BedSettings", DictionariesTypeName = "床位区域",
                    Status = true, Remark = "B"
                },
                new Dictionaries()
                {
                    DictionariesCode = "RescueArea",
                    DictionariesName = "抢救区",
                    DictionariesTypeCode = "BedSettings", DictionariesTypeName = "床位区域",
                    Status = true, Remark = "A"
                }
            });
        }

        //添加区域种子数据
        if (dictList.All(w => w.DictionariesTypeCode != "Area"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries()
                {
                    DictionariesCode = "OutpatientArea",
                    DictionariesName = "就诊区",
                    DictionariesTypeCode = "Area", DictionariesTypeName = "区域",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "ObservationArea",
                    DictionariesName = "留观区",
                    DictionariesTypeCode = "Area", DictionariesTypeName = "区域",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "RescueArea",
                    DictionariesName = "抢救区",
                    DictionariesTypeCode = "Area", DictionariesTypeName = "区域",
                    Status = true
                },
            });
        }

        //添加流转理由数据
        if (dictList.All(w => w.DictionariesTypeCode != "CirculationReasons"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries()
                {
                    DictionariesCode = "Remission",
                    DictionariesName = "病情缓解",
                    DictionariesTypeCode = "CirculationReasons", DictionariesTypeName = "流转理由",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "Aggravation",
                    DictionariesName = "病情加重",
                    DictionariesTypeCode = "CirculationReasons", DictionariesTypeName = "流转理由",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "FurtherProcessing",
                    DictionariesName = "进一步处理",
                    DictionariesTypeCode = "CirculationReasons", DictionariesTypeName = "流转理由",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "Other",
                    DictionariesName = "其他",
                    DictionariesTypeCode = "CirculationReasons", DictionariesTypeName = "流转理由",
                    Status = true
                }
            });
        }

        //添加科室数据
        if (dictList.All(w => w.DictionariesTypeCode != "Department"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries()
                {
                    DictionariesCode = "EmergencyTreatment",
                    DictionariesName = "急诊门诊科",
                    DictionariesTypeCode = "Department", DictionariesTypeName = "科室",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "Pediatrics",
                    DictionariesName = "儿科",
                    DictionariesTypeCode = "Department", DictionariesTypeName = "科室",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "Orthopaedics",
                    DictionariesName = "骨科",
                    DictionariesTypeCode = "Department", DictionariesTypeName = "科室",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "IntracardiacDepartment",
                    DictionariesName = "心内科",
                    DictionariesTypeCode = "Department", DictionariesTypeName = "科室",
                    Status = true
                }
            });
        }

        //添加诊断数据
        if (dictList.All(w => w.DictionariesTypeCode != "Diagnosis"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries()
                {
                    DictionariesCode = "Commonly",
                    DictionariesName = "一般诊断",
                    DictionariesTypeCode = "Diagnosis", DictionariesTypeName = "诊断",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "Suspected",
                    DictionariesName = "疑似诊断",
                    DictionariesTypeCode = "Diagnosis", DictionariesTypeName = "诊断",
                    Status = true
                },
                new Dictionaries()
                {
                    DictionariesCode = "Main",
                    DictionariesName = "主要诊断",
                    DictionariesTypeCode = "Diagnosis", DictionariesTypeName = "诊断",
                    Status = true
                }
            });
        }

        //出科原因
        if (dictList.All(w => w.DictionariesTypeCode != "OutDeptReason"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries
                {
                    DictionariesCode = "NormalGraduation",
                    DictionariesName = "正常出科",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "ToHospital",
                    DictionariesName = "转住院",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "Death",
                    DictionariesName = "死亡",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "0",
                    DictionariesName = "正常出科",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "1",
                    DictionariesName = "转住院",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "2",
                    DictionariesName = "死亡",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "3",
                    DictionariesName = "转输液区",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "4",
                    DictionariesName = "其他",
                    DictionariesTypeCode = "OutDeptReason", DictionariesTypeName = "出科原因",
                    Status = true
                }
                });
        }

        //危重登记
        if (dictList.All(w => w.DictionariesTypeCode != "EmergencyLevel"))
        {
            list.AddRange(new List<Dictionaries>()
            {
                new Dictionaries
                {
                    DictionariesCode = "Commonly",
                    DictionariesName = "一般",
                    DictionariesTypeCode = "EmergencyLevel", DictionariesTypeName = "危重登记",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "BeCriticallyIll",
                    DictionariesName = "病重",
                    DictionariesTypeCode = "EmergencyLevel", DictionariesTypeName = "危重登记",
                    Status = true
                },
                new Dictionaries
                {
                    DictionariesCode = "Endangered",
                    DictionariesName = "濒危",
                    DictionariesTypeCode = "EmergencyLevel", DictionariesTypeName = "危重登记",
                    Status = true
                }
            });
        }

        if (list.Count > 0)
        {
            await _repository.InsertManyAsync(list);
        }
    }

    private async Task CreateTypeAsync()
    {
        var typeList = await _typeRepository.GetListAsync();
        var modelList = new List<DictionariesType>();
        if (!typeList.Any(x => x.DictionariesTypeCode == "CirculationReasons"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "CirculationReasons", "流转理由", "", 0);
            modelList.Add(model);
        }
        if (!typeList.Any(x => x.DictionariesTypeCode == "ListSettings"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "ListSettings", "列表设置", "", 0);
            modelList.Add(model);

        }
        if (!typeList.Any(x => x.DictionariesTypeCode == "BedSettings"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "BedSettings", "床位区域", "", 0);
            modelList.Add(model);

        }
        if (!typeList.Any(x => x.DictionariesTypeCode == "Area"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "Area", "区域", "", 0);
            modelList.Add(model);
        }
        if (!typeList.Any(x => x.DictionariesTypeCode == "Department"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "Department", "科室", "", 0);
            modelList.Add(model);
        }
        if (!typeList.Any(x => x.DictionariesTypeCode == "Diagnosis"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "Diagnosis", "诊断", "", 0);
            modelList.Add(model);
        }
        if (!typeList.Any(x => x.DictionariesTypeCode == "OutDeptReason"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "OutDeptReason", "出科原因", "", 0);
            modelList.Add(model);

        }
        if (!typeList.Any(x => x.DictionariesTypeCode == "EmergencyLevel"))
        {
            var model = new DictionariesType(Guid.NewGuid(), "EmergencyLevel", "危重登记", "", 0);
            modelList.Add(model);
        }
        if (modelList.Any())
        {
            await _typeRepository.InsertManyAsync(modelList);
        }
    }

    private async Task CreateMultitypeAsync()
    {
        var typeList = await _multitypeRepository.GetListAsync();
        var modelList = new List<DictionariesMultitype>();
        if (!typeList.Any(x => x.GroupCode == "Recipe" && x.Code== "Print"))
        {
            var model = new DictionariesMultitype( Guid.NewGuid(), "Recipe", "医嘱设置", "Print","提交打印",0,"false",0,"",true,1);
            modelList.Add(model);
        }
        if (!typeList.Any(x => x.GroupCode == "Recipe" && x.Code == "StopOrdering"))
        {
            var model = new DictionariesMultitype(Guid.NewGuid(), "Recipe", "医嘱设置", "StopOrdering", "停嘱按钮", 0, "false", 0, "", true, 2);
            modelList.Add(model);
        }
        if (!typeList.Any(x => x.GroupCode == "Recipe" && x.Code == "Nullify"))
        {
            var model = new DictionariesMultitype(Guid.NewGuid(), "Recipe", "医嘱设置", "Nullify", "作废医嘱", 0, "false", 0, "", true, 3);
            modelList.Add(model);
        }

        if (!typeList.Any(x => x.GroupCode == "EMR" && x.Code == "PrintPreview"))
        {
            var model = new DictionariesMultitype(Guid.NewGuid(), "EMR", "病历设置", "PrintPreview", "打印预览", 0, "false", 0, "", true, 4);
            modelList.Add(model);
        }
        if (!typeList.Any(x => x.GroupCode == "EMR" && x.Code == "Mark"))
        {
            var model = new DictionariesMultitype(Guid.NewGuid(), "EMR", "病历设置", "Mark", "留痕功能", 0, "false", 0, "", true, 5);
            modelList.Add(model);
        }
        if (modelList.Any())
        {
            await _multitypeRepository.InsertManyAsync(modelList);
        }
    }
}
