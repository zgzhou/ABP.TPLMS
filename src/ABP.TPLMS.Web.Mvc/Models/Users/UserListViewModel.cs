using System.Collections.Generic;
using ABP.TPLMS.Roles.Dto;

namespace ABP.TPLMS.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
