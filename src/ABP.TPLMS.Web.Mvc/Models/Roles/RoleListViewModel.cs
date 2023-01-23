using System.Collections.Generic;
using ABP.TPLMS.Roles.Dto;

namespace ABP.TPLMS.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
