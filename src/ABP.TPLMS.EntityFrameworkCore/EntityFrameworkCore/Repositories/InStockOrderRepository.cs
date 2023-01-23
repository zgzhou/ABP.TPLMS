using Abp.Data;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ABP.TPLMS.EntityFrameworkCore.Repositories
{
    public class InStockOrderRepository : TPLMSRepositoryBase<InStockOrder, int> ,IInStockOrderRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;

        public InStockOrderRepository(IDbContextProvider<TPLMSDbContext> dbContextProvider) : base(dbContextProvider)
        { }
        protected InStockOrderRepository(IDbContextProvider<TPLMSDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
        }

        public DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            EnsureConnectionOpen();
            var dbFacade = GetContext().Database;
            var connection = Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.GetDbConnection(dbFacade);

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

 

        DbCommand IInStockOrderRepository.CreateCommand(string commandText, CommandType commandType, params object[] parameters)
        {
            EnsureConnectionOpen();
            var dbFacade = GetContext().Database;
            var connection = Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.GetDbConnection(dbFacade);

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private void EnsureConnectionOpen()
        {
            var dbFacade = GetContext().Database;
            var connection = Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.GetDbConnection(dbFacade);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        int IInStockOrderRepository.Execute(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                {"ContextType", typeof(TPLMSDbContext) },
                {"MultiTenancySide", MultiTenancySide }
            });
        }

         string IInStockOrderRepository.GetNo(string name)
        {
 //           SqlParameter paout = new System.Data.SqlClient.SqlParameter("BH", System.Data.SqlDbType.NVarChar, 30);
 //           paout.Direction = ParameterDirection.Output;


 //           int cnt=  Context.Database.ExecuteSqlCommand(
 //"EXEC p_NextBH @Name, @BH",
 // new SqlParameter("Name", name),
 //paout);
            //return paout.Value.ToString();


            SqlParameter[] parameters = {
                                         new SqlParameter("Name",System.Data.SqlDbType.NVarChar,10),
                                          new SqlParameter("BH", System.Data.SqlDbType.NVarChar,20)

                                                                    };
            parameters[0].Value = name;
            parameters[1].Direction = System.Data.ParameterDirection.Output;
            int cnt = GetContext().Database.ExecuteSqlRaw(
 "EXEC p_NextBH @Name, @BH output",
parameters);

            string no = parameters[1].Value.ToString();

            if (cnt < 0)
            {
                no = string.Empty;
            }
            return no;

        }


        void IInStockOrderRepository.ImportCargo(string ids,string no)
        {
            
            SqlParameter[] parameters = {
                                         new SqlParameter("id",System.Data.SqlDbType.VarChar,500),
                                          new SqlParameter("No", System.Data.SqlDbType.NVarChar,20)

                                                                    };
            parameters[0].Value = ids + ",";
            parameters[1].Value = no;
            int cnt = GetContext().Database.ExecuteSqlRaw(
 "EXEC SP_ImportCargo2GDE @id, @No",
parameters);

        }

        IQueryable<T> IInStockOrderRepository.SqlQuery<T>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<InStockOrderDetail> GetInodLocs(string rcv, string cargoName)
        {
            var DOs = from m in GetContext().InStockOrderDetail
                      join c in GetContext().InStockOrderDetailLoc on m.Id equals c.InStockOrderDetailId
                      join o in GetContext().InStockOrder on m.InStockNo equals o.No
                      where  c.Qty - c.OutQty > 0
                      select new InStockOrderDetail
                      {
                          Batch = m.Batch,
                          Brand = m.Brand,
                          CargoCode = m.CargoCode,
                          CargoName = m.CargoName,
                          Country = m.Country,
                          Curr = m.Curr,
                          GrossWt = m.GrossWt,
                          Height = m.Height,
                          HSCode = m.HSCode,
                          InStockNo = m.InStockNo,
                          LawfQty = m.LawfQty,
                          LawfUnit = m.LawfUnit,
                          Length = m.Length,
                          NetWt = m.NetWt,
                          Package = m.Package,
                          Price = m.Price,
                          SecdLawfQty = m.SecdLawfQty,
                          SecdLawfUnit = m.SecdLawfUnit,
                          Spcf = m.Spcf,
                          SupplierId = m.SupplierId,
                          TotalAmt = m.TotalAmt,
                          Unit = m.Unit,
                          Vol = m.Vol,
                          Width = m.Width,
                          Id = c.Id,
                          Qty = c.Qty - c.OutQty,
                          Loc = c.Loc,

                          SeqNo = c.SeqNo
                      };
            if (!String.IsNullOrEmpty(cargoName))
            {
                DOs = DOs.Where(s => s.CargoName.Contains(cargoName));
            }
            return DOs;
        }

    }
}

 

    //    public class SqlExecuter : ISqlExecuter, ITransientDependency
    //    {
    //        private readonly IDbContextProvider<TPLMSDbContext> _dbContextProvider;

    //        public SqlExecuter(IDbContextProvider<TPLMSDbContext> dbContextProvider)
    //        {
    //            _dbContextProvider = dbContextProvider;
    //        }

    //        /// <summary>
    //        /// 执行给定的命令
    //        /// </summary>
    //        /// <param name="sql">命令字符串</param>
    //        /// <param name="parameters">要应用于命令字符串的参数</param>
    //        /// <returns>执行命令后由数据库返回的结果</returns>
    //        public int Execute(string sql, params object[] parameters)
    //        {
    //            var e = _dbContextProvider.GetDbContext();
    //            e.InStockOrderDetail.Local

    //            //(sql, parameters);
    //        }

    //        /// <summary>
    //        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。
    //        /// </summary>
    //        /// <typeparam name="T">查询所返回对象的类型</typeparam>
    //        /// <param name="sql">SQL 查询字符串</param>
    //        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
    //        /// <returns></returns>
    //        public IQueryable<T> SqlQuery<T>(string sql, params object[] parameters)
    //        {
    //            return _dbContextProvider.GetDbContext().Database.SqlQuery<T>(sql, parameters).AsQueryable();
    //        }

    //        IQueryable<T> ISqlExecuter.SqlQuery<T>(string sql, params object[] parameters)
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }
