using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Web.Api.Models;

namespace Web.Api.Infrastructure.EntityFramework.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            //Primary Key
            this.HasKey(k => k.Id);

            //Properties
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);            
            
            // Table & Column Mappings
            this.ToTable("Roles");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
        }
    }
}