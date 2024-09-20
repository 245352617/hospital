using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.IService;
using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.BodyParts.Domain.Shared.Enum;
using Volo.Abp.Data;
using Volo.Abp.Guids;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AutoMapper;

namespace YiJian.BodyParts.Service
{
    /// <summary>
    /// 数据库基础服务
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger _logger;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IDictSourceRepository _dictSourcesRepository;
        private readonly IIcuSysParaRepository _icuSysParaRepository;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="nursingWorkStaistics"></param>
        /// <param name="guidGenerator"></param>
        /// <param name="dictSourcesRepository"></param>
        /// <param name="icuSysParasRepository"></param>
        /// <param name="icuDeptRepository"></param>
        /// <param name="icuSysParaDictRepository"></param>
        /// <param name="icuSysParaTbColRepository"></param>
        /// <param name="icuSysParaTbResultRepository"></param>
        public DatabaseService(ILogger<DatabaseService> logger,
            IGuidGenerator guidGenerator, IDictSourceRepository dictSourcesRepository, IIcuSysParaRepository icuSysParasRepository)
        {
            _logger = logger;
            _guidGenerator = guidGenerator;
            _dictSourcesRepository = dictSourcesRepository;
            _icuSysParaRepository = icuSysParasRepository;
        }

        /// <summary>
        /// 发送种子初始化数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SeedAsync(DataSeedContext context)
        {
            try
            {
            
                #region 表:字典-基础字典设置表

                // 获取系统表数据
                // 获取标识代码，用于比较差集数据，有且只有两条数据
                List<string> allDictsourcesCode = await _dictSourcesRepository.Where(s => (s.ParaCode == "zrbz" || s.ParaCode == "zcbz" || s.ModuleCode == "APACHEII") && s.IsEnable == true && s.Pid == null && s.ParaType == "S")
                    .Select(s => s.ParaCode).ToListAsync();

                // 初始化获取种子数据
                List<DictSource> allDictSources = new List<DictSource>();
                allDictSources.AddRange(dictSources());
                // 基础code
                List<string> dictSourcesParaCode = allDictSources.Select(s => s.ParaCode).ToList();

                // 找出差集
                List<string> dictSourcesExceptCode = dictSourcesParaCode.Except(allDictsourcesCode).ToList();

                if (dictSourcesExceptCode.Count > 0)
                {
                    List<DictSource> dictSources = allDictSources.Where(s => dictSourcesExceptCode.Contains(s.ParaCode)).ToList();
                    await _dictSourcesRepository.CreateRangeAsync(dictSources);
                }
                #endregion

                #region SOFA

                List<string> allDictsourcesCodeBySofa = await _dictSourcesRepository.Where(s => (s.ModuleCode == "SOFA") && s.IsEnable == true && s.Pid == null && s.ParaType == "S")
                    .Select(s => s.ParaCode).ToListAsync();

                // 初始化获取种子数据
                List<DictSource> allDictSourcesBySofa = new List<DictSource>();
                allDictSourcesBySofa.AddRange(dictSourcesBySofa());
                // 基础code
                List<string> dictSourcesParaCodeBySofa = allDictSourcesBySofa.Select(s => s.ParaCode).ToList();

                // 找出差集
                List<string> dictSourcesExceptCodeBySofa = dictSourcesParaCodeBySofa.Except(allDictsourcesCodeBySofa).ToList();

                if (dictSourcesExceptCodeBySofa.Count > 0)
                {
                    List<DictSource> dictSourcesBySofa = allDictSourcesBySofa.Where(s => dictSourcesExceptCodeBySofa.Contains(s.ParaCode)).ToList();
                    await _dictSourcesRepository.CreateRangeAsync(dictSourcesBySofa);
                }

                #endregion

                //var modultTypes = new string[] { "DeptOverview", "NursingWorkStaistics" };
                //var allData = await _nursingWorkStaistics.Where(s => modultTypes.Contains(s.IndexName)).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"种子数据发生{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 表:字典-基础字典设置表
        /// </summary>
        /// <returns></returns>
        private List<DictSource> dictSources()
        {
            List<DictSource> dictSources = new List<DictSource>
            {
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "zrbz",
                    ModuleName = "转入标准",
                    ParaCode = "zrbz",
                    ParaName = "转入标准",
                    ParaValue = false,
                    IsEnable = true,
                    SortNum = 1,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "zcbz",
                    ModuleName = "转出标准",
                    ParaCode = "zcbz",
                    ParaName = "转出标准",
                    ParaValue = false,
                    IsEnable = true,
                    SortNum = 2,
                    Pid = null
                },
                #region APACHEII
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "TEMP",
                    ParaName = "体温",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 1,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "AP",
                    ParaName = "平均动脉压",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 2,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "HR",
                    ParaName = "心率",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 3,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "RR",
                    ParaName = "呼吸频率",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 4,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "GCS",
                    ParaName = "15-GCS评分",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 15,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "Age",
                    ParaName = "年龄",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 16,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000001",
                    ParaName = "FiO2",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 5,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000002",
                    ParaName = "PaO2",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 6,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000003",
                    ParaName = "AaDO2",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 7,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000004",
                    ParaName = "动脉血PH",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 8,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000005",
                    ParaName = "静脉血HCO3",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 9,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000006",
                    ParaName = "血清钠",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 10,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000007",
                    ParaName = "血清钾",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 11,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000008",
                    ParaName = "血肌酐",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 12,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000009",
                    ParaName = "红细胞比容",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 13,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "APACHEII",
                    ModuleName = "APACHEII评分",
                    ParaCode = "YiJian.BodyParts.000010",
                    ParaName = "WBC",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 14,
                    Pid = null
                },
                #endregion

                
            };
            return dictSources;
        }

        /// <summary>
        /// SOFA默认数据
        /// </summary>
        /// <returns></returns>
        private List<DictSource> dictSourcesBySofa()
        {
            return new List<DictSource>
            {
                #region SOFA
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "FiO2",
                    ParaName = "FiO2",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 1,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "YiJian.BodyParts.000002",
                    ParaName = "PaO2/FiO2",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 1,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "BST",
                    ParaName = "呼吸支持",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 2,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "YiJian.BodyParts.000013",
                    ParaName = "血小板",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 3,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "YiJian.BodyParts.000014",
                    ParaName = "胆红素",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 4,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "AP",
                    ParaName = "平均动脉压",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 5,
                    Pid = null
                },
                //new DictSource(_guidGenerator.Create())
                //{
                //    ParaType = "S",
                //    DeptCode = "0",
                //    ModuleCode = "SOFA",
                //    ModuleName = "SOFA评分",
                //    ParaCode = "",
                //    ParaName = "多巴胺剂量",
                //    ParaValue = true,
                //    IsEnable = true,
                //    SortNum = 6,
                //    Pid = null
                //},
                //new DictSource(_guidGenerator.Create())
                //{
                //    ParaType = "S",
                //    DeptCode = "0",
                //    ModuleCode = "SOFA",
                //    ModuleName = "SOFA评分",
                //    ParaCode = "",
                //    ParaName = "去甲肾上腺素剂量",
                //    ParaValue = true,
                //    IsEnable = true,
                //    SortNum = 7,
                //    Pid = null
                //},
                //new DictSource(_guidGenerator.Create())
                //{
                //    ParaType = "S",
                //    DeptCode = "0",
                //    ModuleCode = "SOFA",
                //    ModuleName = "SOFA评分",
                //    ParaCode = "DOB",
                //    ParaName = "肾上腺素剂量",
                //    ParaValue = true,
                //    IsEnable = true,
                //    SortNum = 8,
                //    Pid = null
                //},
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "DOB",
                    ParaName = "多巴酚丁胺",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 9,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "GCS",
                    ParaName = "GCS",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 10,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "YiJian.BodyParts.000012",
                    ParaName = "肌酐",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 11,
                    Pid = null
                },
                new DictSource(_guidGenerator.Create())
                {
                    ParaType = "S",
                    DeptCode = "0",
                    ModuleCode = "SOFA",
                    ModuleName = "SOFA评分",
                    ParaCode = "UV",
                    ParaName = "24小时尿量",
                    ParaValue = true,
                    IsEnable = true,
                    SortNum = 12,
                    Pid = null
                }

                #endregion SOFA
            };
        }

    }
}