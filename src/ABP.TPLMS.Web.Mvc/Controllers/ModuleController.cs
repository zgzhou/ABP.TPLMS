using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Runtime.Validation;
using ABP.TPLMS.Controllers;
using ABP.TPLMS.Modules;
using ABP.TPLMS.Modules.Dto;
using ABP.TPLMS.Web.Models.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABP.TPLMS.Web.Controllers
{
    [AbpMvcAuthorize] /*
    public class ModuleController : TPLMSControllerBase
    {
        const int MaxNum= 1000;
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
      
            var module = (await _moduleAppService.GetAll(new PagedModuleResultRequestDto { MaxResultCount = MaxNum })).Items; // Paging not implemented yet
            CreateUpdateModuleDto cuModule = AutoMapper.Mapper.Map<CreateUpdateModuleDto>(module.First());
            var model = new EditModuleModalViewModel
            {
                Module = cuModule,
                Modules = module
            };
           
            return View(model);
        }

        private readonly IModuleAbpAppService _moduleAppService;

        public ModuleController(IModuleAbpAppService moduleAppService)
        {
            _moduleAppService = moduleAppService;
        }
        public async Task<ActionResult> EditModuleModal(int moduleId)
        {
            var module = await _moduleAppService.Get(new EntityDto<int>(moduleId));
            CreateUpdateModuleDto cuModule = AutoMapper.Mapper.Map<CreateUpdateModuleDto>(module);
            var model = new EditModuleModalViewModel
            {
                Module = cuModule,
                Modules = null
            };
            return View("_EditModuleModal", model);
        }
    }*/

    public class ModuleController : TPLMSControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            Logger.Info("列表操作-日记记录 - 显示模块列表");
            var output = _moduleAppService.GetAllAsync();
            var model = new EditModuleModalViewModel
            {
                Module = m_map.Map<CreateUpdateModuleDto>(output.Result.Items.First()),
                Modules = output.Result.Items
            };
            return View(model);
        }
      
            private readonly IModuleAppService _moduleAppService;
        AutoMapper.Mapper m_map;
            public ModuleController(IModuleAppService moduleAppService,AutoMapper.Mapper map)
            {
            _moduleAppService = moduleAppService;  
            m_map = map;
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditModuleModalViewModel updateDto)
        {
            if (updateDto == null)
            {
                return NotFound();
            }
            if (updateDto.Module == null)
            {
                return NotFound();
            }
            _moduleAppService.CreateAsync(updateDto.Module);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [DisableValidation]
        public ActionResult Edit(int id,EditModuleModalViewModel updateDto)
        {
            if (id != updateDto.Module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var module= updateDto.Module;
                    _moduleAppService.UpdateAsync(module);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DtoExists(updateDto.Module.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updateDto);
           

            //var output = _moduleAppService.GetAllAsync();

            //return PartialView("_List", output.Result);
        }
        private bool DtoExists(long id)
        {
            return _moduleAppService.GetAllAsync().Result.Items.Any(e => e.Id == id);
        }
        // GET: Cargoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module =  _moduleAppService.GetAllAsync().Result.Items.SingleOrDefault(m => m.Id == id);
            if (module == null)
            {
                return NotFound();
            }
            var model = new EditModuleModalViewModel
            {
                Module = m_map.Map<CreateUpdateModuleDto>(module)                
            };
            return View(model);
            //return Ok(cargo.Result);
        }

        // GET: Cargoes/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                     
                var module = _moduleAppService.GetAllAsync().Result.Items.SingleOrDefault(m => m.Id == id);

            if (module == null)
            {
                return NotFound();
            }
            var model = new EditModuleModalViewModel
            {
                Module = m_map.Map<CreateUpdateModuleDto>(module)
            };
            return View(model);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _moduleAppService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
                //throw;
            }
         
            return RedirectToAction(nameof(Index));
        }

    }
}
