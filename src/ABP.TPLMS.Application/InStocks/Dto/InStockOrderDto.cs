using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ABP.TPLMS.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.InStocks.Dto
{
    [AutoMapFrom(typeof(InStockOrder))]
    public class InStockOrderDto : EntityDto<int>
    {
        public string No { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        public string WarehouseType { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
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
        public string OwnerName { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public decimal Gwt { get; set; }
        public decimal Nwt { get; set; }
        public int PackageQty { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public string ReceiveTime { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string Receiver { get; set; }

        public string Oper { get; set; }
        public int Status { get; set; }
        public string OwnerCode { get; set; }
        /// <summary>
        /// 预计送货时间
        /// </summary>
        public string PreDeliveryTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Checker { get; set; }
        public string CheckTime { get; set; }
        public string Remark { get; set; }
        public DateTime CreationTime { get; set; }
        public string LastUpdateTime { get; set; }
        public string LastOper { get; set; }

        public List<InStockOrderDetailDto> InStockOrderDetail { get; set; }
        public List<InStockOrderDetailLocDto> InStockOrderDetailLoc { get; set; }
    }

    //public class OutStockDto : EntityDto<int>
    //{
    //    public string No { get; set; }
    //    /// <summary>
    //    /// 客户名称
    //    /// </summary>
    //    public string CustomerName { get; set; }

    //    public string WarehouseType { get; set; }
    //    /// <summary>
    //    /// 客户代码
    //    /// </summary>
    //    public string CustomerCode { get; set; }
    //    /// <summary>
    //    /// 船名
    //    /// </summary>
    //    public string VesselName { get; set; }
    //    /// <summary>
    //    /// 航次
    //    /// </summary>
    //    public string Voyage { get; set; }
    //    /// <summary>
    //    /// 舱单号
    //    /// </summary>
    //    public string BookingNo { get; set; }
    //    /// <summary>
    //    /// 集装箱号
    //    /// </summary>

    //    public string ContainerNo { get; set; }
    //    public string ContainerSize { get; set; }
    //    public string ContainerType { get; set; }
    //    /// <summary>
    //    /// 截关时间
    //    /// </summary>
    //    public string  ClosingTime { get; set; }


    //    public string Oper { get; set; }
    //    public int Status { get; set; }
    //    public string OTD { get; set; }

    //    public string OTA { get; set; }
    //    public string POL { get; set; }

    //    public string POD { get; set; }
    //    public DateTime CreationTime { get; set; }

    //}
}
