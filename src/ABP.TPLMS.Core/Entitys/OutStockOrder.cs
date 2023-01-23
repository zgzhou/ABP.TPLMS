using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ABP.TPLMS.Entitys
{
    public class OutStockOrder : Entity<int>, IHasCreationTime
    {

        public const int MaxLength = 255;
        public OutStockOrder()
        {
            No = string.Empty;
            CustomerCode = string.Empty;
            CustomerName = string.Empty;
            WarehouseNo = string.Empty;
            
            DeliveryNo = string.Empty;
            TallyClerk = string.Empty;
            TallyTime = string.Empty;
            CreationTime = DateTime.Now;
            Oper = string.Empty;
            Checker = string.Empty;
            CheckTime = string.Empty;
            Gwt = 0;
            Nwt = 0;
            PackageQty = 0;
            OwnerCode = string.Empty;
            OwnerName = string.Empty;
            Remark = string.Empty;
            Status = 0;
            PreOutStockTime = string.Empty;
        }

        [StringLength(50)]
        [Required]
        public string No { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [StringLength(MaxLength)]
        [Required]
        public string CustomerName { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string VehicleNo { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>


        [StringLength(50)]
        [Required]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 收货人代码
        /// </summary>
        public string ConsigneeCode { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee{get ;set;}
        /// <summary>
        /// 收货人社会信用代码
        /// </summary>
        public string ConsigneeSCCD { get; set; }
        /// <summary>
        /// 托运人，发货人
        /// </summary>
        public string Shipper { get; set; }

        /// <summary>
        /// 托运人，发货人代码
        /// </summary>
        public string ShipperCode { get; set; }

     
        /// <summary>
        /// 托运人，发货人社会信用代码
        /// </summary>
        public string ShipperSCCD { get; set; }

        /// <summary>
        /// 通知人
        /// </summary>
        public string Notify { get; set; }

        /// <summary>
        /// 通知人代码
        /// </summary>
        public string NotifyCode { get; set; }


        /// <summary>
        /// 通知人社会信用代码
        /// </summary>
        public string NotifySCCD { get; set; }


        /// <summary>
        /// 送货单号
        /// </summary>
        public string DeliveryNo { get; set; }
        /// <summary>
        /// 仓库号
        /// </summary>
        public string WarehouseNo { get; set; }
        /// <summary>
        /// 货主
        /// </summary>
        [StringLength(MaxLength)]
        [Required]
        public string OwnerName { get; set; }


        public decimal Gwt { get; set; }
        public decimal Nwt { get; set; }
        public int PackageQty { get; set; }
        /// <summary>
        /// 理货时间
        /// </summary>
        [StringLength(20)]
        public string TallyTime { get; set; }
        /// <summary>
        /// 理货员
        /// </summary>
        [StringLength(50)]
        public string TallyClerk { get; set; }

        [StringLength(50)]
        public string Oper { get; set; }
        public int Status { get; set; }
        [StringLength(50)]
        public string OwnerCode { get; set; }
        /// <summary>
        /// 预计出库时间
        /// </summary>
        [StringLength(20)]
        public string PreOutStockTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [StringLength(50)]
        public string Checker { get; set; }
        [StringLength(20)]
        public string CheckTime { get; set; }
        [StringLength(1000)]
        public string Remark { get; set; }
        public DateTime CreationTime { get; set; }
        [StringLength(20)]
        public string LastUpdateTime { get; set; }
        [StringLength(50)]
        public string LastOper { get; set; }

    }
}
