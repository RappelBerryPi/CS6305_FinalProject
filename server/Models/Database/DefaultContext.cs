using Microsoft.EntityFrameworkCore;

namespace server.Models.Database {

    public class DefaultContext : DbContext {

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

        public DbSet<Hello> Hellos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
    }

}