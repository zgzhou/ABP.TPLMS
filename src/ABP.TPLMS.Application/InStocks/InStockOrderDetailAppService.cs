using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.InStocks.Dto;
using ABP.TPLMS.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.InStocks
{
    public class InStockOrderDetailAppService : AsyncCrudAppService<InStockOrderDetail, InStockOrderDetailDto, int
        , PagedInStockDetailResultRequestDto,
                            CreateUpdateInStockOrderDetailDto, CreateUpdateInStockOrderDetailDto>, IInStockOrderDetailAppService

    {
        public InStockOrderDetailAppService( IRepository<InStockOrderDetail, int> repository)
            : base(repository)
        {
        }
        protected override IQueryable<InStockOrderDetail> CreateFilteredQuery(PagedInStockDetailResultRequestDto input)
        {
            var qry = base.CreateFilteredQuery(input)
                .Where(t => t.InStockNo == input.InStockNo);
            return qry;
        }

    }
}
