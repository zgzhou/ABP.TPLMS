using Abp.Application.Services;
using Abp.Domain.Repositories;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.Modules
{
   public class ModuleAbpAppService :AsyncCrudAppService<Module, ModuleDto, int, PagedModuleResultRequestDto,
                            CreateUpdateModuleDto, CreateUpdateModuleDto>,IModuleAbpAppService
        
    {
        public ModuleAbpAppService(IRepository<Module, int> repository)
            : base(repository)
    {
             
    }
        public  Task<ModuleDto> Create(CreateUpdateModuleDto input)
        {
            
            return CreateAsync(input);
        }
        public override Task<ModuleDto> CreateAsync(CreateUpdateModuleDto input)
        {
            var sin = input;
            return base.CreateAsync(input);
        }
    }
}
