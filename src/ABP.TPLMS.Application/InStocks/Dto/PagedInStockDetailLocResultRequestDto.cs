using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.InStocks.Dto
{
    public class PagedInStockDetailLocResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int InStockOrderDetailId { get; set; }
  
    }
}
