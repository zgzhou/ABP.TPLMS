using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ABP.TPLMS.Controllers;
using ABP.TPLMS.InStocks;
using ABP.TPLMS.InStocks.Dto;
using Abp.Web.Models;

namespace ABP.TPLMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class InStockController1 : TPLMSControllerBase
    {
        private readonly IInStockOrderAppService _inSOAppService;
        private const int MAX_COUNT = 1000;
        public InStockController1(IInStockOrderAppService InSOAppService)
        {
            _inSOAppService = InSOAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [DontWrapResult]
        [HttpPost]
        public string List()
        {

            var page = Request.Form["page"].ToString();
            var size = Request.Form["rows"].ToString();
            int pageIndex = page == null ? 1 : int.Parse(page);
            int pageSize = size == null ? 20 : int.Parse(size);
            PagedInStockResultRequestDto paged = new PagedInStockResultRequestDto();
            paged.MaxResultCount = MAX_COUNT;
            paged.SkipCount = ((pageIndex - 1) < 0 ? 0 : pageIndex - 1) * pageSize;

            //paged.InStockNo = Request.Form["Name"].ToString();
            //paged.OrgCode = Request.Form["Code"].ToString();
            //paged.CustomCode = Request.Form["CustomCode"].ToString();


            var userList = _inSOAppService.GetAllAsync(paged).GetAwaiter().GetResult().Items;
            int total = userList.Count;
            var json = JsonEasyUI(userList, total);
            return json;
        }
    }
}
