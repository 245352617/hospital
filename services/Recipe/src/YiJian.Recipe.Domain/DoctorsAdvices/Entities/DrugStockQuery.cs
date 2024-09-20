using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.DoctorsAdvices.Entities
{
    /// <summary>
    /// 库存相关的信息
    /// </summary>
    [Comment("库存相关的信息")]
    public class DrugStockQuery : Entity<int>
    {
        /// <summary>
        /// 药房编码
        /// <![CDATA[
        /// 2.1药房编码码字典（字典、写死）
        /// ]]>
        /// </summary>
        [Comment("药房编码")]
        public int Storage { get; set; }

        /// <summary>
        /// 药品编号, 医院药品唯一编码
        /// </summary>
        [Comment("药品编号, 医院药品唯一编码")]
        [StringLength(50)]
        public string DrugCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        [Comment("药品名称")]
        [StringLength(50)]
        public string DrugName { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        [Comment("药品规格")]
        [StringLength(50)]
        public string DrugSpec { get; set; }

        /// <summary>
        /// 包装单位,大/小包装单位 小包装单位=片
        /// </summary>
        [Comment("包装单位,大/小包装单位 小包装单位=片")]
        [StringLength(50)]
        public string MinPackageUnit { get; set; }

        /// <summary>
        /// 厂商id
        /// </summary>
        [Comment("厂商id")]
        [StringLength(50)]
        public string FirmID { get; set; }

        /// <summary>
        /// 进货价格 元
        /// </summary>
        [Comment("进货价格 元")]
        [StringLength(50)]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 零售价格 元
        /// </summary>
        [Comment("零售价格 元")]
        public decimal RetailPrice { get; set; }

        /// <summary>
        /// 药房规格, 门急诊药房规格
        /// </summary>
        [Comment("药房规格, 门急诊药房规格")]
        [StringLength(50)]
        public string PharSpec { get; set; }

        /// <summary>
        /// 药品单位, 门急诊药房单位
        /// </summary>
        [Comment("药品单位, 门急诊药房单位")]
        [StringLength(50)]
        public string PharmUnit { get; set; }

        /// <summary>
        /// 包装数量, 大/小包装数量 小包装数量 =（例如一盒有36片）=36
        /// </summary>
        [Comment("包装数量, 大/小包装数量 小包装数量 =（例如一盒有36片）=36")]
        public decimal PackageAmount { get; set; }

        /// <summary>
        /// 包装类型, 1 表示小包装，0表示大包装
        /// </summary>
        [Comment("药 包装类型, 1 表示小包装，0表示大包装")]
        public int MinPackageIndicator { get; set; }

        /// <summary>
        /// 最小单位数量 
        /// </summary>
        [Comment("最小单位数量 ")]
        public int Dosage { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        [Comment("最小单位")]
        [StringLength(50)]
        public string DosageUnit { get; set; }

        /// <summary>
        /// 药品剂量
        /// </summary>
        [Comment("药品剂量")]
        [StringLength(50)]
        public decimal DrugDose { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        [Comment("剂量单位")]
        [StringLength(50)]
        public string DrugUnit { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [Comment("备注信息")]
        [StringLength(50)]
        public string ReturnDesc { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [Comment("库存数量")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 药品效期
        /// </summary>
        [Comment("药品效期")]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 药品批号
        /// </summary>
        [Comment("药品批号")]
        [StringLength(50)]
        public string DrugBatchNumber { get; set; }


        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        public void SetDoctorsAdviceId(Guid doctorsAdviceId)
        {
            DoctorsAdviceId = doctorsAdviceId;
        }


        public void Update(DrugStockQuery model)
        {
            Storage = model.Storage;
            DrugCode = model.DrugCode;
            DrugName = model.DrugName;
            DrugSpec = model.DrugSpec;
            MinPackageUnit = model.MinPackageUnit;
            FirmID = model.FirmID;
            PurchasePrice = model.PurchasePrice;
            RetailPrice = model.RetailPrice;
            PharSpec = model.PharSpec;
            PharmUnit = model.PharmUnit;
            PackageAmount = model.PackageAmount;
            MinPackageIndicator = model.MinPackageIndicator;
            Dosage = model.Dosage;
            DosageUnit = model.DosageUnit;
            DrugDose = model.DrugDose;
            DrugUnit = model.DrugUnit;
            ReturnDesc = model.ReturnDesc;
            Quantity = model.Quantity;
            ExpiryDate = model.ExpiryDate;
            DrugBatchNumber = model.DrugBatchNumber;

        }

    }
}
