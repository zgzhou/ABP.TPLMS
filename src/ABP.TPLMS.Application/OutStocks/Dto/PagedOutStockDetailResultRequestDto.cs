using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.OutStocks.Dto
{
    public class PagedOutStockDetailResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string InStockNo { get; set; }
        public string OutStockNo { get; set; }

    }
}
