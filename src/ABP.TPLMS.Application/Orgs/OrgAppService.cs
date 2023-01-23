using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using ABP.TPLMS.Entitys;
using ABP.TPLMS.Orgs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.TPLMS.Orgs
{
    public class OrgAppService : AsyncCrudAppService<Org, OrgDto, int, PagedOrgResultRequestDto,
                            CreateUpdateOrgDto, CreateUpdateOrgDto>, IOrgAppService

    {
        public OrgAppService(IRepository<Org, int> repository)
            : base(repository)
        {

        }
        [DontWrapResult]
        public  PagedOrgResultDto<OrgDto> GetAllOrgs(PagedOrgResultRequestDto input)
        {
            PagedOrgResultDto<OrgDto> orgs = new PagedOrgResultDto<OrgDto>();
            input.MaxResultCount = 1000;//这里需要进行参数传递
            var allOrgs=GetAllAsync(input);
            orgs.Rows = allOrgs.Result.Items;
            orgs.Total = allOrgs.Result.TotalCount;
            return orgs;
        }
      
        protected override IQueryable<Org> CreateFilteredQuery(PagedOrgResultRequestDto input)
        {
            var qry= base.CreateFilteredQuery(input)
                .Where(t=>t.Name.Contains(input.OrgName==null?string.Empty:input.OrgName))
                .Where(t => t.BizCode.Contains(input.OrgCode==null?string.Empty:input.OrgCode))
                .Where(t => t.CustomCode.Contains(input.CustomCode==null?string.Empty:input.CustomCode));
            List<Org> list = qry.ToList<Org>();

            var qry1 = base.CreateFilteredQuery(input);
            List<Org> listParent = new List<Org>();
            GetParentOrgs(listParent, list[0].ParentId, qry1);
            return qry.Union<Org>(listParent);
        }
        private void GetParentOrgs(List<Org> orgs, int ParentId, IQueryable<Org> listOrgs)
        {
            List<Org> drs = listOrgs.Where(x => x.Id == ParentId).ToList();
            if (drs == null || drs.Count <= 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < drs.Count; i++)
                {
                    var dr = drs[i];
                    if (!orgs.Contains(dr))
                    {
                        orgs.Add(dr);
                    }                    
                    GetParentOrgs(orgs, dr.ParentId, listOrgs);
                }
            }
        }
    }
}
