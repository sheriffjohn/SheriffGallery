using System.Data.Entity;
using Web.Infrastructure.EntityFramework.Mapping;
using Web.Models;

namespace Web.Infrastructure
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PhotoItemMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new PhotoMap());
        }
    }
    public class EFDbInitializer : CreateDatabaseIfNotExists<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
        }
    }
}
