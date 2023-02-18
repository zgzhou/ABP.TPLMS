using ABP.TPLMS.Cargos.Dto;
using ABP.TPLMS.Entitys;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.InStocks.Dto
{
    internal class InStockMapProfile:Profile
    {

        public InStockMapProfile()
        {
            CreateMap<InStockOrderDto, InStockOrder>();

            CreateMap<InStockOrderDto, CreateUpdateInStockOrderDto>();

            CreateMap<CreateUpdateInStockOrderDto, InStockOrder>();

            CreateMap<InStockOrderDetailDto, InStockOrderDetail>();

            CreateMap<InStockOrderDetailDto, CreateUpdateInStockOrderDetailDto>();

            CreateMap<CreateUpdateInStockOrderDetailDto, InStockOrderDetail>();

            CreateMap<InStockOrderDetailLocDto, InStockOrderDetailLoc>();

            CreateMap<InStockOrderDetailLocDto, CreateUpdateInStockOrderDetailLocDto>();

            CreateMap<CreateUpdateInStockOrderDetailLocDto, InStockOrderDetailLoc>();
        }
    }
}
