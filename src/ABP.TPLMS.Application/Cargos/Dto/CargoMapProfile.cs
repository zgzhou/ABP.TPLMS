using ABP.TPLMS.Entitys;
using ABP.TPLMS.Orgs.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.Cargos.Dto
{
    internal class CargoMapProfile:Profile
    {
        public CargoMapProfile()
        {
            CreateMap<CargoDto, Cargo>();

            CreateMap<CargoDto, CreateUpdateCargoDto>();

            CreateMap<CreateUpdateCargoDto, Cargo>();

        }
    }
}
