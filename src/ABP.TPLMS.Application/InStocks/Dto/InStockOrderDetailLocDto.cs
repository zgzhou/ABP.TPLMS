using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ABP.TPLMS.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ABP.TPLMS.InStocks.Dto
{
   public class InStockOrderDetailLocDto :  EntityDto<int>
    {
         [AutoMapFrom(typeof(InStockOrderDetailLoc))]
        public InStockOrderDetailLocDto()
        {
           
            this.Qty = 0;
            this.SeqNo = 0;
            this.Loc = string.Empty;
            this.CreationTime = DateTime.Now;
            this.InStockOrderDetailId = 0;
        }

      

        public int InStockOrderDetailId { get; set; }
        public int SeqNo { get; set; }
        public string Loc { get; set; }    

        public decimal Qty { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
