using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Models;

namespace Web.Infrastructure.EntityFramework.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            //Primary Key
            this.HasKey(k => k.Id);

            //Properties
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.RowId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(50);

            //Many-to-One
            this.HasRequired<Role>(p => p.Role)
                .WithMany()
                .HasForeignKey(f => f.Role_FK)
                .WillCascadeOnDelete(false);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.Name).HasColumnName("UserName");
        }
    }
}