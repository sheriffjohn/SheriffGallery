using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Infrastructure.EntityFramework.Mapping
{
    public class PhotoMap : EntityTypeConfiguration<Photo>
    {
        public PhotoMap()
        {
            //Primary Key
            this.HasKey(k => k.Id);

            //Properties
            //this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.RowId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(p => p.Binary)
               .IsRequired()
               .HasMaxLength(5000);

            // Table & Column Mappings
            this.ToTable("Photos");
        }
    }
}