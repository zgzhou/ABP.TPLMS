using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Web.Models;
using ABP.TPLMS.Controllers;
using ABP.TPLMS.Orgs;
using ABP.TPLMS.Orgs.Dto;
using ABP.TPLMS.Web.Models.Orgs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABP.TPLMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class OrgsController : TPLMSControllerBase
    {
        private readonly IOrgAppService _orgAppService;
        private const int MAX_COUNT= 1000;
        
        public OrgsController(IOrgAppService orgAppService)
        {
            _orgAppService = orgAppService;
        }
        [HttpGet]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [DontWrapResult]
        [HttpPost]
        public string List()
        {

            //var page = Request.Form["page"].ToString();
            //var size = Request.Form["rows"].ToString();
            //int pageIndex = page == null ? 1 : int.Parse(page);
            //int pageSize = size == null ? 20 : int.Parse(size);
            PagedOrgResultRequestDto paged = new PagedOrgResultRequestDto();
            paged.MaxResultCount = MAX_COUNT;
            //paged.SkipCount = ((pageIndex - 1) < 0 ? 0 : pageIndex - 1) * pageSize;
          
            paged.OrgName = Request.Form["Name"].ToString();
            paged.OrgCode = Request.Form["Code"].ToString();
            paged.CustomCode = Request.Form["CustomCode"].ToString();


            var userList = _orgAppService.GetAllAsync(paged).GetAwaiter().GetResult().Items;
            int total = userList.Count;
            var json = JsonEasyUI(userList, total);
            return json;
        }
        [DontWrapResult]
        [HttpGet]
        public JsonResult GetJsonTree()
        {
            PagedOrgResultRequestDto paged = new PagedOrgResultRequestDto();
            paged.MaxResultCount = MAX_COUNT;
            var classlist = _orgAppService.GetAllAsync(paged).GetAwaiter().GetResult().Items;
            List<TreeJsonViewModel> list = LinqJsonTree(classlist,0);
           // list.Insert(0, new TreeJsonViewModel() { id = 0, children = null, parentId = 0, text = "根节点" });
            return Json(list);

        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //
        private List<TreeJsonViewModel> LinqJsonTree(IReadOnlyList<OrgDto> orgs,int parentId)
        {
            List<TreeJsonViewModel> jsonData = new List<TreeJsonViewModel>();
            List<OrgDto> classlist = orgs.Where(m => m.ParentId == parentId).ToList();
            classlist.ToList().ForEach(item =>
            {
                jsonData.Add(new TreeJsonViewModel
                {
                    id = item.Id,
                    children = LinqJsonTree(orgs, item.Id),
                    parentId = item.ParentId,
                    text = item.Name,
                    url = string.Empty,
                    state = parentId == 0 ? "open" : ""
                });
            });
            return jsonData;
        }
    }
}
