using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using ABP.TPLMS.Entitys;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Application.Services.Dto;

namespace ABP.TPLMS.IRepositories
{
    public interface IInStockOrderRepository : IRepository<InStockOrder,int>
    {
        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        int Execute(string sql, params object[] parameters);

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。
        /// </summary>
        /// <typeparam name="T">查询所返回对象的类型</typeparam>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        IQueryable<T> SqlQuery<T>(string sql, params object[] parameters);
        DbCommand CreateCommand(string commandText, CommandType commandType, params object[] parameters);
        /// <summary>
        /// 创建单号
        /// </summary>
        /// <param name="name">单证名称代码</param>
        /// <returns></returns>
        string GetNo(string name);
        /// <summary>
        /// 导入货物信息
        /// </summary>
        /// <param name="ids">导入货物的ID集合</param>
        /// <param name="no">单号</param>
        void ImportCargo(string ids,string no);
         
        IEnumerable<InStockOrderDetail> GetInodLocs(string rcv, string cargoName);

       

    }
}
