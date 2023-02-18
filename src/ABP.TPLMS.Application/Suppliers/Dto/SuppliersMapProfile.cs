using ABP.TPLMS.Entitys;
using ABP.TPLMS.Orgs.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.Suppliers.Dto
{
    internal class SuppliersMapProfile:Profile
    {
        public SuppliersMapProfile()
        {
            CreateMap<SupplierDto, Supplier>();

            CreateMap<SupplierDto, CreateUpdateSupplierDto>();

            CreateMap<CreateUpdateSupplierDto, Supplier>();

        }
    }
}
