using Abp.Application.Services;
using ABP.TPLMS.Modules.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.Modules
{
    public interface IModuleAbpAppService : IAsyncCrudAppService<//定义了CRUD方法
             ModuleDto, //用来展示模块
             int, //Module实体的主键
             PagedModuleResultRequestDto, //获取模块的时候用于分页
             CreateUpdateModuleDto, //用于创建模块
             CreateUpdateModuleDto> //用于更新模块
    {
    }
}
