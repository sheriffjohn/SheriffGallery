using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Web.Api.Models;

namespace Web.Api.Infrastructure.EntityFramework.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            //Primary Key
            this.HasKey(k => k.Id);

            //Properties
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.RowId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Ignore<PhotoItem>(p => p.PhotoItem);

            this.Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(250);
            
            // Table & Column Mappings
            this.ToTable("Comments");
            this.Property(t => t.Text).HasColumnName("Text");
        }
    }
}