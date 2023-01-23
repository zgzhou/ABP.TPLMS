using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using ABP.TPLMS.OutStocks.Dto;


namespace ABP.TPLMS.OutStocks
{
  public  interface IOutStockOrderAppService : IAsyncCrudAppService<//定义了CRUD方法
             OutStockOrderDto, //用来展示出库单信息
             int, //出库单实体的主键
             PagedOutStockResultRequestDto, //获取出库单信息的时候用于分页
             CreateUpdateOutStockOrderDto, //用于创建出库单信息
             CreateUpdateOutStockOrderDto> //用于更新出库单信息
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
        string Save(OutStockOrderDto iso);
        /// <summary>
        /// 导入货物信息
        /// </summary>
        /// <param name="ids">导出库货物信息的ID</param>
        /// <param name="No">出库单单号</param>
        /// <returns></returns>
        string ImportInStockDetail(string ids,string No);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">出库单ID集合</param>
        /// <returns></returns>
        bool DeleteById(string ids);


    }

}
