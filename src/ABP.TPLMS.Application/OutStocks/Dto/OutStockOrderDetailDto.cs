using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ABP.TPLMS.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ABP.TPLMS.OutStocks.Dto
{
    [AutoMapFrom(typeof(OutStockOrderDetail))]
    public class OutStockOrderDetailDto : EntityDto<int>
    {
        public int SupplierId { get; set; }

        public string CargoCode { get; set; }

        public string HSCode { get; set; }
    
        public string CargoName { get; set; }
 
        public string Spcf { get; set; }
   
        public string Unit { get; set; }
        /// <summary>
        /// 目的国
        /// </summary>
   
        public string DestCountry { get; set; }
        /// <summary>
        /// 原产国
        /// </summary>
     
        public string Country { get; set; }

        public string Brand { get; set; }
    
        public string Curr { get; set; }

        public string Package { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Vol { get; set; }

        public decimal Price { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal GrossWt { get; set; }

        public decimal NetWt { get; set; }

        public DateTime CreationTime { get; set; }
    
        public string OutStockNo { get; set; }
        public int InStockOrderDetailLocId { get; set; }
        public decimal Qty { get; set; }
        public decimal LawfQty { get; set; }
        public decimal SecdLawfQty { get; set; }

        public string LawfUnit { get; set; }
    
        public string SecdLawfUnit { get; set; }

        public string Batch { get; set; }
        public string Loc { get; set; }

    }
}
