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
    public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> builder)
        {
            builder.ToTable("Fuels").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.InsertDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdateeDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeleteDate).HasColumnName("DeletedDate");

            //builder.HasIndex(indexExpression: b => b.Name, name: "UK_Fuels_Name").IsUnique();

            builder.HasMany(b => b.Models);

            builder.HasQueryFilter(b => !b.DeleteDate.HasValue);
        }
    }
}
