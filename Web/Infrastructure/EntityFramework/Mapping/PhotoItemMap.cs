using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Models;

namespace Web.Infrastructure.EntityFramework.Mapping
{
    public class PhotoItemMap : EntityTypeConfiguration<PhotoItem>
    {
        public PhotoItemMap()
        {
            //Primary Key
            this.HasKey(k => k.Id);

            //Properties
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.RowId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.Info)
                .IsRequired()
                .HasMaxLength(250);

            //One-to-Many
            this.HasMany<Comment>(c => c.Comments)
                .WithRequired(c => c.PhotoItem)
                .HasForeignKey(f => f.PhotoItem_FK)
                .WillCascadeOnDelete(true);

            this.HasOptional<Photo>(c => c.Photo)
            .WithRequired(r => r.PhotoItem)
            .WillCascadeOnDelete(true);

            // Table & Column Mappings
            this.ToTable("PhotoItems");
            this.Property(t => t.Info).HasColumnName("Info");
        }
    }
}