using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using ABP.TPLMS.InStocks.Dto;


namespace ABP.TPLMS.InStocks
{
  public  interface IInStockOrderAppService : IAsyncCrudAppService<//定义了CRUD方法
             InStockOrderDto, //用来展示入库单信息
             int, //入库单实体的主键
             PagedInStockResultRequestDto, //获取入库单信息的时候用于分页
             CreateUpdateInStockOrderDto, //用于创建入库单信息
             CreateUpdateInStockOrderDto> //用于更新入库单信息
    {
        /// <summary>
        /// 创建单号
        /// </summary>
        /// <returns></returns>
        string GetNo();
        /// <summary>
        /// 保存入库单数据
        /// </summary>
        /// <param name="iso"></param>
        /// <returns></returns>
        string Save(InStockOrderDto iso);
        /// <summary>
        /// 导入货物信息
        /// </summary>
        /// <param name="ids">导入货物信息的ID</param>
        /// <param name="No">入库单单号</param>
        /// <returns></returns>
        string ImportCargo(string ids,string No);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">出库单ID集合</param>
        /// <returns></returns>
        bool DeleteById(string ids);


    }

}
