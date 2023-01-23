using Abp.Application.Services;
using ABP.TPLMS.InStocks.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABP.TPLMS.InStocks
{
    public interface IInStockOrderDetailLocAppService : IAsyncCrudAppService<//定义了CRUD方法
             InStockOrderDetailLocDto, //用来展示入库单中的库位信息
             int, //实体的主键
             PagedInStockDetailLocResultRequestDto, //获取入库单信息的时候用于分页
             CreateUpdateInStockOrderDetailLocDto, //用于创建入库单库位信息
             CreateUpdateInStockOrderDetailLocDto> //用于更新入库单库位信息
    {
    }
}