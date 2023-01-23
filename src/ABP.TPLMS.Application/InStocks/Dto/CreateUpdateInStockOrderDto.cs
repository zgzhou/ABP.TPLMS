using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ABP.TPLMS.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ABP.TPLMS.InStocks.Dto
{
    [AutoMapTo(typeof(InStockOrder))]
    public  class CreateUpdateInStockOrderDto : EntityDto<int>
    {
        public const int MaxLength = 255;

        [StringLength(50)]
        [Required]
        public string No { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [StringLength(MaxLength)]
        [Required]
        public string CustomerName { get; set; }

        public string WarehouseType { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
         
        [StringLength(50)]
        [Required]
        public string CustomerCode { get; set; }
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
        /// 接收时间
        /// </summary>
        [StringLength(20)]
        public string ReceiveTime { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        [StringLength(50)]
        public string Receiver { get; set; }

        [StringLength(50)]
        public string Oper { get; set; }
        public int Status { get; set; }
        [StringLength(50)]
        public string OwnerCode { get; set; }
        /// <summary>
        /// 预计送货时间
        /// </summary>
        [StringLength(20)]
        public string PreDeliveryTime { get; set; }
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

        public List<CreateUpdateInStockOrderDetailDto> InStockOrderDetail { get; set; }
        public List<CreateUpdateInStockOrderDetailLocDto> InStockOrderDetailLoc { get; set; }
    }
}
