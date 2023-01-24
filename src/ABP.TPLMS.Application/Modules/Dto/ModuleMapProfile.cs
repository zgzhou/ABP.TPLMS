using ABP.TPLMS.Authorization.Users;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Users.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.Modules.Dto
{
    public class ModuleMapProfile:Profile
    {

        public ModuleMapProfile()
        {
            CreateMap<ModuleDto, Module>();
            
            CreateMap<ModuleDto, CreateUpdateModuleDto>();

           CreateMap<CreateUpdateModuleDto, Module>();

        }
    }
}
