using ABP.TPLMS.Entitys;
using ABP.TPLMS.Suppliers.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.OutStocks.Dto
{
    internal class OutStockMapProfile:Profile
    {
        public OutStockMapProfile() {
            CreateMap<OutStockOrderDto, OutStockOrder>();

            CreateMap<OutStockOrderDto, CreateUpdateOutStockOrderDto>();

            CreateMap<CreateUpdateOutStockOrderDto, OutStockOrderDto>();

            CreateMap<OutStockOrderDetailDto, OutStockOrderDetail>();

            CreateMap<OutStockOrderDetailDto, CreateUpdateOutStockOrderDetailDto>();

            CreateMap<CreateUpdateOutStockOrderDetailDto, OutStockOrderDetail>();
        }
    }
}
