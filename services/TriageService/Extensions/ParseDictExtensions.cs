using System;
using System.Collections.Generic;
using System.Linq;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 字典转换扩展类
    /// </summary>
    public static class ParseDictExtensions
    {
        /// <summary>
        /// 根据Code转换Name
        /// </summary>
        /// <param name="dicts">字典集合</param>
        /// <param name="enumType">字典类型枚举</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public static string GetNameByDictCode(this Dictionary<string, List<TriageConfigDto>> dicts, TriageDict enumType, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return "";
            }

            if (!dicts.ContainsKey(enumType.ToString()))
            {
                return  "";
            }

            var dict = dicts[enumType.ToString()].FirstOrDefault(x => x.TriageConfigCode == code);
            return dict == null ? "" : dict.TriageConfigName;
        }

        /// <summary>
        /// 根据Name转换Code(不存在的Name则直接返回空值)
        /// </summary>
        /// <param name="dicts">字典集合</param>
        /// <param name="enumType">字典类型枚举</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static string GetCodeByDictName(this Dictionary<string, List<TriageConfigDto>> dicts, TriageDict enumType, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "";
            }
            
            if (!dicts.ContainsKey(enumType.ToString()))
            {
                return  "";
            }

            var dict = dicts[enumType.ToString()].FirstOrDefault(x => x.TriageConfigName == name);
            return dict == null ? "" : dict.TriageConfigCode;
        }

        /// <summary>
        /// 根据字段设置病患信息
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static PatientInfo SetPatientInfo(this Dictionary<string, List<TriageConfigDto>> dicts, PatientInfo patient)
        {
            patient.ToHospitalWayName = dicts.GetNameByDictCode(TriageDict.ToHospitalWay, patient.ToHospitalWayCode);
            patient.SexName = dicts.GetNameByDictCode(TriageDict.Sex, patient.Sex);
            patient.IdentityName = dicts.GetNameByDictCode(TriageDict.IdentityType, patient.Identity);
            patient.ChargeTypeName = dicts.GetNameByDictCode(TriageDict.Faber, patient.ChargeType);
            patient.NationName = dicts.GetNameByDictCode(TriageDict.Nation, patient.Nation);
            patient.GreenRoadName = dicts.GetNameByDictCode(TriageDict.GreenRoad, patient.GreenRoadCode);
            patient.DiseaseName = dicts.GetNameByDictCode(TriageDict.KeyDiseases, patient.DiseaseCode);
            patient.TypeOfVisitName = dicts.GetNameByDictCode(TriageDict.TypeOfVisit, patient.TypeOfVisitCode);
            patient.ConsciousnessName = dicts.GetNameByDictCode(TriageDict.Mind, patient.Consciousness);
            patient.NarrationName = "";
            if (!string.IsNullOrWhiteSpace(patient.Narration))
            {
                foreach (var narration in patient.Narration.Split(","))
                {
                    patient.NarrationName += dicts.GetNameByDictCode(TriageDict.Narration, narration) + ",";
                }

                patient.NarrationName = patient.NarrationName.TrimEnd(',');
            }

            return patient;
        }
    }
}