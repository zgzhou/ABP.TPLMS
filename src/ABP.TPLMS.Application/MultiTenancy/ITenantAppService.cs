using Abp.Application.Services;
using ABP.TPLMS.MultiTenancy.Dto;

namespace ABP.TPLMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

