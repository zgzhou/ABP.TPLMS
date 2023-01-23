using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using Abp.Web.Models;
using ABP.TPLMS.Controllers;
using ABP.TPLMS.Helpers;
using ABP.TPLMS.Models.InStock;
using ABP.TPLMS.OutStocks;
using ABP.TPLMS.OutStocks.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ABP.TPLMS.Web.Controllers
{
    public class OutStockController :  TPLMSControllerBase
        {
            private readonly IOutStockOrderAppService _outOODAppService;
            private readonly IOutStockOrderDetailAppService _outOODAppDetService;
            
            private const int MAX_COUNT = 1000;
            public OutStockController(IOutStockOrderAppService InSOAppService, IOutStockOrderDetailAppService InSODAppService
                )
            {
                _outOODAppService = InSOAppService;
                _outOODAppDetService = InSODAppService;
                
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
                PagedOutStockResultRequestDto paged = new PagedOutStockResultRequestDto();
                paged.MaxResultCount = MAX_COUNT;
                paged.SkipCount = ((pageIndex - 1) < 0 ? 0 : pageIndex - 1) * pageSize;
                paged.BeginTime = DateTime.Now.AddMonths(-1);
                paged.EndTime = DateTime.Now.AddDays(1);

             

                var query = _outOODAppService.GetAllAsync(paged).GetAwaiter().GetResult();
                var isoList = query.Items;
                int total = query.TotalCount;
                var json = JsonEasyUI(isoList, total);

                return json;
            }
            [DontWrapResult]
            public string GetDetail(string no)
            {
                PagedOutStockDetailResultRequestDto paged = new PagedOutStockDetailResultRequestDto();
                paged.MaxResultCount = MAX_COUNT;
                paged.InStockNo = no;

                var podList = _outOODAppDetService.GetAllAsync(paged).GetAwaiter().GetResult().Items; ;
                var json = JsonEasyUI(podList);
                return json;
            }
            [HttpPost]
            [DisableValidation]
            public ActionResult Add(OutStockOrderDto iso)
            {
                string result = "NO";
                try
                {
                    PagedOutStockResultRequestDto condition = new PagedOutStockResultRequestDto();
                    condition.No = iso.No;

                    var isoExists = _outOODAppService.GetAllAsync(condition).GetAwaiter().GetResult();
                    if (isoExists.TotalCount > 0)
                    {
                        return Content(result);
                    }

                    CreateUpdateOutStockOrderDto cuIso = ObjectMapper.Map<CreateUpdateOutStockOrderDto>(iso);
                    // TODO: Add logic here
                    var obj = _outOODAppService.CreateAsync(cuIso);
                    result = "OK";
                }
                catch (Exception ex)
                {

                    result = "NO";
                }
                return Content(result);
            }
            //[DontWrapResult]
            [HttpPost]
            [DisableValidation]

            public string Update(OutStockOrderDto iso)
            {
                string result = "NO";
                List<OutStockOrderDetailDto> list = new List<OutStockOrderDetailDto>();
                
                try
                {
                    string head = Request.Form["postdata"];
                    if (!string.IsNullOrEmpty(head))
                    {
                        //把json字符串转换成对象
                        iso = JsonHelper.Instance.Deserialize<OutStockOrderDto>(head);
                    }
                    list = GetDetailDtos();
                    
                    if (iso == null)
                    {
                        return "没有表头！";
                    }

                    iso.OutStockOrderDetail = list;
                    
                    result = _outOODAppService.Save(iso);

                }
                catch
                {

                }
                if (result == "OK")
                {
                    return "更新成功！";
                }
                else
                    return "更新失败！";
            }

            private List<OutStockOrderDetailDto> GetDetailDtos()
            {
                List<OutStockOrderDetailDto> list = new List<OutStockOrderDetailDto>();
                string deleted = Request.Form["deleted"];
                string inserted = Request.Form["inserted"];
                string updated = Request.Form["updated"];

                // TODO: Add update logic here
                if (!string.IsNullOrEmpty(deleted))
                {
                    //把json字符串转换成对象
                    List<OutStockOrderDetailDto> listDeleted = JsonHelper.Instance.Deserialize<List<OutStockOrderDetailDto>>(deleted);
                    //TODO 下面就可以根据转换后的对象进行相应的操作了
                    if (listDeleted != null && listDeleted.Count > 0)
                    {
                        list.AddRange(listDeleted.ToArray());
                    }
                }
                if (!string.IsNullOrEmpty(inserted))
                {
                    //把json字符串转换成对象
                    List<OutStockOrderDetailDto> listInserted = JsonHelper.Instance.Deserialize<List<OutStockOrderDetailDto>>(inserted);
                    if (listInserted != null && listInserted.Count > 0)
                    {
                        list.AddRange(listInserted.ToArray());
                    }
                }
                if (!string.IsNullOrEmpty(updated))
                {
                    //把json字符串转换成对象
                    List<OutStockOrderDetailDto> listUpdated = JsonHelper.Instance.Deserialize<List<OutStockOrderDetailDto>>(updated);
                    if (listUpdated != null && listUpdated.Count > 0)
                    {
                        list.AddRange(listUpdated.ToArray());
                    }
                }
                return list;
            }

           
            [HttpPost]
            [DisableValidation]
            public ActionResult ImportInStockOrder(CargoModel isoder)
            {
                string result = "NO";
                try
                {
                    // TODO: 导入货物信息
                    result = _outOODAppService.ImportInStockDetail(isoder.Ids, isoder.No);
                }
                catch
                {

                }
                return Content(result);
            }
            [HttpPost]
            [DontWrapResult]
            public ActionResult Delete(string ids)
            {
                string result = "NO";
                try
                {
                    // TODO: Add Delete logic here
                    bool flag = _outOODAppService.DeleteById(ids);
                    if (flag)
                    {
                        result = "OK";
                    }
                }
                catch
                {

                }
                return Content(result);
            }
          
        }
    
}