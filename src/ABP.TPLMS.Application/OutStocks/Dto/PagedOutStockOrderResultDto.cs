using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.OutStocks.Dto
{
    public class PagedOutStockOrderResultDto<T>
    {
        public int Total
        {
            get;
            set;
        }
        public IReadOnlyList<T> Rows
        { get; set; }
    }
}
