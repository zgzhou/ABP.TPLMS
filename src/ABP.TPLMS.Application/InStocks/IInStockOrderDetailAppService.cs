using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using ABP.TPLMS.InStocks.Dto;


namespace ABP.TPLMS.InStocks
{
  public  interface IInStockOrderDetailAppService : IAsyncCrudAppService<//定义了CRUD方法
             InStockOrderDetailDto, //用来展示入库单明细信息
             int, //Org实体的主键
             PagedInStockDetailResultRequestDto, //获取入库单信息的时候用于分页
             CreateUpdateInStockOrderDetailDto, //用于创建入库单明细信息
              CreateUpdateInStockOrderDetailDto > //用于更新入库单明细信息
    {
    }

}
