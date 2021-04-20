using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace server.Models.Database {

    public class DefaultContext : IdentityDbContext<UserInfo, UserRole, long> {

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

        public DbSet<Hello> Hellos { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<ContractDeployment> ContractDeployments { get; set; }
    }

}