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
    public class InStockOrderDetailLocAppService : AsyncCrudAppService<InStockOrderDetailLoc, InStockOrderDetailLocDto, int
        , PagedInStockDetailLocResultRequestDto,
                            CreateUpdateInStockOrderDetailLocDto, CreateUpdateInStockOrderDetailLocDto>, IInStockOrderDetailLocAppService

    {
        public InStockOrderDetailLocAppService( IRepository<InStockOrderDetailLoc, int> repository)
            : base(repository)
        {
        }
        protected override IQueryable<InStockOrderDetailLoc> CreateFilteredQuery(PagedInStockDetailLocResultRequestDto input)
        {
            var qry = base.CreateFilteredQuery(input)
                .Where(t => t.InStockOrderDetailId == input.InStockOrderDetailId);
            return qry;
        }

    }
}
