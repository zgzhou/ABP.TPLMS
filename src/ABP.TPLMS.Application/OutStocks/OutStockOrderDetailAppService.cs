using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.OutStocks.Dto;
using ABP.TPLMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.OutStocks
{
    public class OutStockOrderDetailAppService : AsyncCrudAppService<OutStockOrderDetail, OutStockOrderDetailDto, int
        , PagedOutStockDetailResultRequestDto,
                            CreateUpdateOutStockOrderDetailDto, CreateUpdateOutStockOrderDetailDto>, IOutStockOrderDetailAppService

    {
        public OutStockOrderDetailAppService( IRepository<OutStockOrderDetail, int> repository)
            : base(repository)
        {
        }
        protected override IQueryable<OutStockOrderDetail> CreateFilteredQuery(PagedOutStockDetailResultRequestDto input)
        {
            var qry = base.CreateFilteredQuery(input)
                .Where(t => t.OutStockNo == input.OutStockNo);
            return qry;
        }

    }
}
