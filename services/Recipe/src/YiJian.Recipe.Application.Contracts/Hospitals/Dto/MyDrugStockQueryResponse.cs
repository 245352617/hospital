using System;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.Hospitals.Dto
{





    /// <summary>
    /// 库存信息
    /// </summary>
    public class MyDrugStockQueryResponse
    {
        /// <summary>
        /// 药房编码
        /// <![CDATA[
        /// 2.1药房编码码字典（字典、写死）
        /// ]]>
        /// </summary>
        public EDrugStoreCode PharmacyCode { get; set; }

        /// <summary>
        /// 药房编码名称
        /// </summary>
        public string PharmacyName
        {
            get
            {
                return PharmacyCode.GetDescription();
            }
        }

        /// <summary>
        /// 药品编号, 医院药品唯一编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 药品规格 
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 厂家
        /// </summary> 
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary> 
        public string FactoryCode { get; set; }

        /// <summary>
        /// 批发价格（进货价格 元）
        /// </summary> 
        public decimal? FixPrice { get; set; }

        /// <summary>
        /// 零售价格（ 零售价格 元）
        /// </summary> 
        public decimal? RetPrice { get; set; }

        /// <summary>
        /// 药房规格, 门急诊药房规格
        /// </summary>
        public string PharSpec { get; set; }

        /// <summary>
        /// 药品单位, 门急诊药房单位
        /// </summary>
        public string PharmUnit { get; set; }

        /// <summary>
        /// 包装数量, 大/小包装数量 小包装数量 =（例如一盒有36片）=36
        /// </summary>
        public decimal PackageAmount { get; set; }

        /// <summary>
        /// 包装单位,大/小包装单位 小包装单位=片
        /// </summary>
        public string MinPackageUnit { get; set; }


        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary> 
        public EMedicineUnPack Unpack
        {
            get
            {
                return (EMedicineUnPack)MinPackageIndicator;
            }
        }

        /// <summary>
        /// 包装类型, 1 表示小包装，0表示大包装
        /// </summary>
        public int MinPackageIndicator { get; set; }

        /// <summary>
        /// 最小单位数量 
        /// </summary>
        public decimal Dosage { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string DosageUnit { get; set; }

        /// <summary>
        /// 药品剂量
        /// </summary>
        public decimal DrugDose { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string DrugUnit { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string ReturnDesc { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 批次号
        /// </summary> 
        public string BatchNo { get; set; }

        /// <summary>
        /// 失效期
        /// </summary> 
        public DateTime? ExpirDate { get; set; }

    }


}