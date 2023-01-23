using Abp.Dependency;
using Abp.Domain.Repositories;
using ABP.TPLMS.Entitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ABP.TPLMS.IRepositories
{
    public interface IOutStockOrderRepository: IRepository<OutStockOrder, int>, ITransientDependency
        {
          
            DbCommand CreateCommand(string commandText, CommandType commandType, params object[] parameters);
            /// <summary>
            /// 创建单号
            /// </summary>
            /// <param name="name">单证名称代码</param>
            /// <returns></returns>
            string GetNo(string name);
            /// <summary>
            /// 导入入库单信息
            /// </summary>
            /// <param name="ids">导入库单的ID集合</param>
            /// <param name="no">单号</param>
            void ImportInStockOrder(string ids, string no);
        
    }
}
