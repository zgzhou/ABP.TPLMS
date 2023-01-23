using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.InStocks.Dto
{
    public class PagedInStockOrderResultDto<T>
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
