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
    [AutoMapTo(typeof(OutStockOrderDetail))]
    public class CreateUpdateOutStockOrderDetailDto : EntityDto<int>
    {
        public const int MaxLength = 255;
        public int SupplierId { get; set; }
        [MaxLength(50)]
        public string CargoCode { get; set; }
        [MaxLength(10)]
        public string HSCode { get; set; }
        [MaxLength(MaxLength)]
        public string CargoName { get; set; }
        [MaxLength(MaxLength)]
        public string Spcf { get; set; }
        [MaxLength(20)]
        public string Unit { get; set; }
        /// <summary>
        /// 目的国
        /// </summary>
        [MaxLength(20)]
        public string DestCountry { get; set; }
        /// <summary>
        /// 原产国
        /// </summary>
        [MaxLength(20)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string Brand { get; set; }
        [MaxLength(20)]
        public string Curr { get; set; }
        [MaxLength(20)]
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
        [MaxLength(20)]
        public string OutStockNo { get; set; }
        public int InStockOrderDetailLocId { get; set; }
        public decimal Qty { get; set; }
        public decimal LawfQty { get; set; }
        public decimal SecdLawfQty { get; set; }

        [MaxLength(20)]
        public string LawfUnit { get; set; }
        [MaxLength(20)]
        public string SecdLawfUnit { get; set; }
        [MaxLength(20)]
        public string Batch { get; set; }
        public string Loc { get; set; }


    }
}
