using System.Data.Entity;
using Web.Api.Infrastructure.EntityFramework.Mapping;
using Web.Api.Models;
using System.Data.Entity.Migrations;
namespace Web.Api.Infrastructure
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbConnectionString")
        {
            Database.SetInitializer<EFDbContext>(new EFDbInitializer());
            Configuration.LazyLoadingEnabled = true;
        }
        public virtual DbSet<PhotoItem> PhotoItems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PhotoItemMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new PhotoMap());
            modelBuilder.Configurations.Add(new RoleMap());            
        }
    }
    public class EFDbInitializer : CreateDatabaseIfNotExists<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            context.Roles.AddOrUpdate(role => role.Id,
                new Role() {Id = (int)Permission.Guest, RoleName = Permission.Guest.ToString() },
                new Role() {Id = (int)Permission.Contributor, RoleName = Permission.Contributor.ToString() },
                new Role() {Id = (int)Permission.Administrator, RoleName = Permission.Administrator.ToString() }
            );
        }
    }
}
