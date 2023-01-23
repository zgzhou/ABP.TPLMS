using Abp.Dependency;
using Abp.EntityFrameworkCore;
using ABP.TPLMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABP.TPLMS.EntityFrameworkCore.Repositories
{
    public class SqlExecuter : ISqlExecuter, ITransientDependency
    {
        private readonly IDbContextProvider<TPLMSDbContext> _dbContextProvider;

        public SqlExecuter(IDbContextProvider<TPLMSDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        public int Execute(string sql, params object[] parameters)
        {
            return 0;
            //return _dbContextProvider.DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。
        /// </summary>
        /// <typeparam name="T">查询所返回对象的类型</typeparam>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数</param>
        /// <returns></returns>
        public IQueryable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return null;
            //return _dbContextProvider.DbContext.Database.SqlQuery<T>(sql, parameters).AsQueryable<T>();
        }
    
    }
}
