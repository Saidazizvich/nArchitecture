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
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.ModelId).HasColumnName("ModelId").IsRequired();
            builder.Property(b => b.Kilometr).HasColumnName("Kilometer").IsRequired();
            builder.Property(b => b.CarState).HasColumnName("State");
            builder.Property(b => b.ModelYear).HasColumnName("ModelYear");

            builder.HasOne(b => b.Model);

            builder.HasQueryFilter(b => !b.DeleteDate.HasValue);
        }
    }
}
