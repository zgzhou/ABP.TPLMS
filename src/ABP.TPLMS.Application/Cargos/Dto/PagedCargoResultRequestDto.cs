using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.Cargos.Dto
{
//    /// <summary>
// 5         /// 每页显示的行数
// 6         /// </summary>
// 7         [Range(1, AppConsts.MaxPageSize)]
// 8         public int MaxResultCount { get; set; }
// 9         /// <summary>
//10         /// 跳过数量=MaxResultCount*页数
//11         /// </summary>
//12         [Range(0, int.MaxValue)]
//13         public int SkipCount { get; set; }
//14 
//15         public PagedInputDto()
//16         {
//17             MaxResultCount = AppConsts.DefaultPageSize;
//18         }
public class PagedCargoResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string CargoName { get; set; }
        public string CargoCode { get; set; }
        public string HsCode { get; set; }
    }
}
