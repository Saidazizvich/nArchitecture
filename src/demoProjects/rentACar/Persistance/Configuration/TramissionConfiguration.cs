using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configuration
{
    public class TramissionConfiguration : IEntityTypeConfiguration<Trasmission>
    {
        public void Configure(EntityTypeBuilder<Trasmission> builder)
        {
            builder.ToTable("Transmissions").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.DeleteDate).HasColumnName("DeleteDate");
            builder.Property(b => b.UpdateDate).HasColumnName("UpdateDate");

            //builder.HasIndex(indexExpression: b => b.Name, name: "UK_Tranmissions_Name").IsUnique();

            builder.HasMany(b => b.Models);

            builder.HasQueryFilter(b => !b.DeleteDate.HasValue);
        }
    }
}
