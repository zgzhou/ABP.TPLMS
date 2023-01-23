using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.Modules.Dto
{
    public class PagedModuleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
