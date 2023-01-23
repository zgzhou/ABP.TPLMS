using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using ABP.TPLMS.OutStocks.Dto;


namespace ABP.TPLMS.OutStocks
{
  public  interface IOutStockOrderDetailAppService : IAsyncCrudAppService<//定义了CRUD方法
             OutStockOrderDetailDto, //用来展示出库单明细信息
             int, //出库单实体的主键
             PagedOutStockDetailResultRequestDto, //获取出库单信息的时候用于分页
             CreateUpdateOutStockOrderDetailDto, //用于创建出库单明细信息
              CreateUpdateOutStockOrderDetailDto > //用于更新出库单明细信息
    {
    }

}
