using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.OutStocks.Dto
{
    public class PagedOutStockResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string InStockNo { get; set; }
        public DateTime BeginTime { get; set; }
        DateTime m_EndTime;
        /// <summary>
        /// 查询截止日期，如果当前时间小于100年前，就给一个默认日期（明天）
        /// </summary>
        public DateTime EndTime { get
            {
                if (m_EndTime < DateTime.Now.AddYears(-100))
                    return DateTime.Now.AddDays(1);
                else
                    return m_EndTime;
                    }
            
            set
            {
                m_EndTime = value;
            }
                }
        public string OwnerName { get; set; }
        public string No { get; set; }
    }
}
