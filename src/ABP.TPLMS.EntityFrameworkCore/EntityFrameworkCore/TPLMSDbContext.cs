using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ABP.TPLMS.Authorization.Roles;
using ABP.TPLMS.Authorization.Users;
using ABP.TPLMS.MultiTenancy;
using ABP.TPLMS.Entitys;

namespace ABP.TPLMS.EntityFrameworkCore
{
    public class TPLMSDbContext : AbpZeroDbContext<Tenant, Role, User, TPLMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public TPLMSDbContext(DbContextOptions<TPLMSDbContext> options)
            : base(options)
        {
        }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Org> Orgs { get; set; }

        public virtual DbSet<InStockOrder> InStockOrder { get; set; }
        public virtual DbSet<InStockOrderDetail> InStockOrderDetail { get; set; }
        public virtual DbSet<InStockOrderDetailLoc> InStockOrderDetailLoc { get; set; }

        public virtual DbSet<OutStockOrder> OutStockOrder { get; set; }
        public virtual DbSet<OutStockOrderDetail> OutStockOrderDetail { get; set; }
    }
}
