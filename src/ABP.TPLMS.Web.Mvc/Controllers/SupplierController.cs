using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Runtime.Validation;
using ABP.TPLMS.Controllers;
using ABP.TPLMS.Suppliers;
using ABP.TPLMS.Suppliers.Dto;
using ABP.TPLMS.Web.Models.Supplier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABP.TPLMS.Web.Controllers
{
    [AbpMvcAuthorize]
    [Audited]
    public class SupplierController : TPLMSControllerBase
    {
        const int MaxNum= 10;
        // GET: /<controller>/
        [DisableAuditing]
        public async Task<IActionResult> Index()
        {

            SupplierDto cuModule=null;
            var module = (await _supplierAppService.GetAllAsync(new PagedSupplierResultRequestDto { MaxResultCount = MaxNum })).Items; // Paging not implemented yet
            if (module.Count>0)
            {
                cuModule = module.First();
            }
            
            var model = new SupplierListViewModel
            {
                Supplier = cuModule,
                Suppliers=module
            };
           
            return View(model);
        }

        private readonly ISupplierAppService _supplierAppService;
        AutoMapper.Mapper m_map;
        public SupplierController(ISupplierAppService supplierAppService)
        {
            _supplierAppService = supplierAppService;
            
        }
        public async Task<ActionResult> EditSupplierModal(int supplierId)
        {
            
            var module = await _supplierAppService.GetAsync(new EntityDto<int>(supplierId));
            CreateUpdateSupplierDto cuSupplier = ObjectMapper.Map<CreateUpdateSupplierDto>(module);
            var model = new EditSupplierModalViewModel
            {
                Supplier = cuSupplier

            };
            return View("_EditSupplierModal", model);
        }
    }


}
