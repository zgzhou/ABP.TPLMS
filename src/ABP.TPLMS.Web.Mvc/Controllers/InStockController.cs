using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using Abp.Web.Models;
using ABP.TPLMS.Controllers;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Helpers;
using ABP.TPLMS.InStocks;
using ABP.TPLMS.InStocks.Dto;
using ABP.TPLMS.Models.InStock;
using Microsoft.AspNetCore.Mvc;

namespace ABP.TPLMS.Web.Controllers
{
    public class InStockController : TPLMSControllerBase
    {
        private readonly IInStockOrderAppService _inSOAppService;
        private readonly IInStockOrderDetailAppService _inSODAppService;
        private readonly IInStockOrderDetailLocAppService _inSODLAppService;
        private const int MAX_COUNT = 1000;
        public InStockController(IInStockOrderAppService InSOAppService,IInStockOrderDetailAppService InSODAppService,
            IInStockOrderDetailLocAppService InSODLAppService)
        {
            _inSOAppService = InSOAppService;
            _inSODAppService = InSODAppService;
            _inSODLAppService = InSODLAppService;
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
            paged.BeginTime = DateTime.Now.AddMonths(-1);
            paged.EndTime = DateTime.Now.AddDays(1);

            //paged.InStockNo = Request.Form["Name"].ToString();
            //paged.OrgCode = Request.Form["Code"].ToString();
            //paged.CustomCode = Request.Form["CustomCode"].ToString();


            var query = _inSOAppService.GetAllAsync(paged).GetAwaiter().GetResult();
            var isoList = query.Items;
            int total = query.TotalCount;
            var json = JsonEasyUI(isoList, total);

            return json;
        }
        [DontWrapResult]
        public string GetDetail(string no)
        {
            PagedInStockDetailResultRequestDto paged = new PagedInStockDetailResultRequestDto();
            paged.MaxResultCount = MAX_COUNT;
            paged.InStockNo = no;
            
            var podList = _inSODAppService.GetAllAsync(paged).GetAwaiter().GetResult().Items; ;
            var json = JsonEasyUI(podList);
            return json;
        }
        [HttpPost]
        [DisableValidation]
        public ActionResult Add(InStockOrderDto iso)
        {
            string result = "NO";
            try
            {
                PagedInStockResultRequestDto condition = new PagedInStockResultRequestDto();
                condition.No = iso.No;

                var isoExists = _inSOAppService.GetAllAsync(condition).GetAwaiter().GetResult();
                if (isoExists.TotalCount > 0)
                {
                    return Content(result);
                }

                CreateUpdateInStockOrderDto cuIso = ObjectMapper.Map<CreateUpdateInStockOrderDto>(iso);
                // TODO: Add logic here
              var obj=  _inSOAppService.CreateAsync(cuIso);
                result = "OK";
            }
            catch(Exception ex)
            {

                result = "NO";
            }
            return Content(result);
        }
        //[DontWrapResult]
        [HttpPost]
        [DisableValidation]

        public string Update(InStockOrderDto iso)
        {
            string result = "NO";
            List<InStockOrderDetailDto> list = new List<InStockOrderDetailDto>();
            List<InStockOrderDetailLocDto> listLoc = new List<InStockOrderDetailLocDto>();
            try
            {
                string head = Request.Form["postdata"];
                if (!string.IsNullOrEmpty(head))
                {
                    //把json字符串转换成对象
                    iso = JsonHelper.Instance.Deserialize<InStockOrderDto>(head);
                }
                list = GetDetailDtos();
                listLoc = GetDetailLocDtos();
                if (iso == null)
                {
                    return "没有表头！";
                }              
               
                iso.InStockOrderDetail = list;
                iso.InStockOrderDetailLoc = listLoc;
                result = _inSOAppService.Save(iso);

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

        private List<InStockOrderDetailDto> GetDetailDtos()
        {
            List<InStockOrderDetailDto> list = new List<InStockOrderDetailDto>();
            string deleted = Request.Form["deleted"];
            string inserted = Request.Form["inserted"];
            string updated = Request.Form["updated"];
          
            // TODO: Add update logic here
            if (!string.IsNullOrEmpty(deleted))
            {
                //把json字符串转换成对象
                List<InStockOrderDetailDto> listDeleted = JsonHelper.Instance.Deserialize<List<InStockOrderDetailDto>>(deleted);
                //TODO 下面就可以根据转换后的对象进行相应的操作了
                if (listDeleted != null && listDeleted.Count > 0)
                {
                    list.AddRange(listDeleted.ToArray());
                }
            }
            if (!string.IsNullOrEmpty(inserted))
            {
                //把json字符串转换成对象
                List<InStockOrderDetailDto> listInserted = JsonHelper.Instance.Deserialize<List<InStockOrderDetailDto>>(inserted);
                if (listInserted != null && listInserted.Count > 0)
                {
                    list.AddRange(listInserted.ToArray());
                }
            }
            if (!string.IsNullOrEmpty(updated))
            {
                //把json字符串转换成对象
                List<InStockOrderDetailDto> listUpdated = JsonHelper.Instance.Deserialize<List<InStockOrderDetailDto>>(updated);
                if (listUpdated != null && listUpdated.Count > 0)
                {
                    list.AddRange(listUpdated.ToArray());
                }
            }
            return list;
        }

        private List<InStockOrderDetailLocDto> GetDetailLocDtos()
        {
            List<InStockOrderDetailLocDto> listLoc = new List<InStockOrderDetailLocDto>();

            string locDel = Request.Form["locsDeleted"];
            string locIns = Request.Form["locsInserted"];
            string locUpd = Request.Form["locsUpdated"];

            // TODO: Add update logic here
            if (!string.IsNullOrEmpty(locDel))
            {
                //把json字符串转换成对象
                List<InStockOrderDetailLocDto> listLocDeleted = JsonHelper.Instance.Deserialize<List<InStockOrderDetailLocDto>>(locDel);
                //TODO 下面就可以根据转换后的对象进行相应的操作了
                if (listLocDeleted != null && listLocDeleted.Count > 0)
                {
                    listLoc.AddRange(listLocDeleted.ToArray());
                }
            }
            if (!string.IsNullOrEmpty(locIns))
            {
                //把json字符串转换成对象
                List<InStockOrderDetailLocDto> listLocInserted = JsonHelper.Instance.Deserialize<List<InStockOrderDetailLocDto>>(locIns);
                if (listLocInserted != null && listLocInserted.Count > 0)
                {
                    listLoc.AddRange(listLocInserted.ToArray());
                }
            }

            if (!string.IsNullOrEmpty(locUpd))
            {
                //把json字符串转换成对象
                List<InStockOrderDetailLocDto> listLocUpdated = JsonHelper.Instance.Deserialize<List<InStockOrderDetailLocDto>>(locUpd);
                if (listLocUpdated != null && listLocUpdated.Count > 0)
                {
                    listLoc.AddRange(listLocUpdated.ToArray());
                }
            }
            return listLoc;
        }
        [HttpPost]
        [DisableValidation]
        public ActionResult ImportCargo(CargoModel cargos)
        {
            string result = "NO";
            try
            {                
                // TODO: 导入货物信息
                result = _inSOAppService.ImportCargo(cargos.Ids, cargos.No);
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
                bool flag = _inSOAppService.DeleteById(ids);
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
        [DontWrapResult]
        public string GetLocs(string Id)
        {
            int inodId;
            int.TryParse(Id, out inodId);

            PagedInStockDetailLocResultRequestDto paged = new PagedInStockDetailLocResultRequestDto();
            paged.MaxResultCount = MAX_COUNT;
            paged.InStockOrderDetailId = inodId;

            var iodlList = _inSODLAppService.GetAllAsync(paged).GetAwaiter().GetResult().Items; ;
            var json = JsonEasyUI(iodlList);
            return json;

        }
    }
}