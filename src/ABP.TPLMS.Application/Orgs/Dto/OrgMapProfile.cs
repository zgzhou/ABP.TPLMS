using ABP.TPLMS.Authorization.Users;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Users.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.Orgs.Dto
{
    public class OrgMapProfile:Profile
    {

        public OrgMapProfile()
        {
            CreateMap<OrgDto, Org>();
            
            CreateMap<OrgDto, CreateUpdateOrgDto>();

           CreateMap<CreateUpdateOrgDto, Org>();

        }
    }
}
