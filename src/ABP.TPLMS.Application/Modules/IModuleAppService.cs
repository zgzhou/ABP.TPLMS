using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.Modules
{
    //public interface IModuleAppService :
    //    IAsyncCrudAppService< //定义了CRUD方法
    //        ModuleDto, //用来展示模块
    //        long, //Book实体的主键
    //        PagedAndSortedResultRequestDto, //获取模块的时候用于分页和排序
    //        CreateUpdateModuleDto, //用于创建模块
    //        CreateUpdateModuleDto> //用户更新模块
    //{
    //}
    public interface IModuleAppService : IApplicationService
    {
        Task CreateAsync(CreateUpdateModuleDto input);
        Task UpdateAsync(CreateUpdateModuleDto input);
        Task<ListResultDto<ModuleDto>> GetAllAsync();
        Task  DeleteAsync(int Id);
        void Delete(int Id);
        List<Module> GetAll();
    }
}
