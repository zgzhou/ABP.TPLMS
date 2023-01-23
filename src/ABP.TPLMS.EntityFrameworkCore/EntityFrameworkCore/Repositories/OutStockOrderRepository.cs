using Abp.Data;
using Abp.Dependency;
using Abp.EntityFrameworkCore;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace ABP.TPLMS.EntityFrameworkCore.Repositories
{
    public class OutStockOrderRepository: TPLMSRepositoryBase<OutStockOrder, int>, IOutStockOrderRepository, ITransientDependency
        {
            private readonly IActiveTransactionProvider _transactionProvider;

            public OutStockOrderRepository(IDbContextProvider<TPLMSDbContext> dbContextProvider) : base(dbContextProvider)
            { }
            protected OutStockOrderRepository(IDbContextProvider<TPLMSDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
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
            DbCommand IOutStockOrderRepository.CreateCommand(string commandText, CommandType commandType, params object[] parameters)
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
         
            private DbTransaction GetActiveTransaction()
            {
                return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs
            {
                {"ContextType", typeof(TPLMSDbContext) },
                {"MultiTenancySide", MultiTenancySide }
            });
            }

            string IOutStockOrderRepository.GetNo(string name)
            {
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

            void IOutStockOrderRepository.ImportInStockOrder(string ids, string no)
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
         
        }
    }
