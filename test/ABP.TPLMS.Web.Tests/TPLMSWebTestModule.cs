using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ABP.TPLMS.EntityFrameworkCore;
using ABP.TPLMS.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ABP.TPLMS.Web.Tests
{
    [DependsOn(
        typeof(TPLMSWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TPLMSWebTestModule : AbpModule
    {
        public TPLMSWebTestModule(TPLMSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TPLMSWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TPLMSWebMvcModule).Assembly);
        }
    }
}