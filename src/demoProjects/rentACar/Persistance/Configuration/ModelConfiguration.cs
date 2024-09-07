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
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Models").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
            builder.Property(b => b.BrandId).HasColumnName("BrandId").IsRequired();
            builder.Property(b => b.FuilId).HasColumnName("FuelId").IsRequired();
            builder.Property(b => b.TranmissionId).HasColumnName("TransmissionId").IsRequired();
            builder.Property(b => b.DailyPrice).HasColumnName("DailyPrice").IsRequired();
            builder.Property(b => b.ImageUrl).HasColumnName("ImageUrl").IsRequired();


            builder.Property(b => b).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdateeDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeleteDate).HasColumnName("DeletedDate");

            //builder.HasIndex(indexExpression: b => b.Name, name: "UK_Models_Name").IsUnique();

            builder.HasOne(b => b.Brand);
            builder.HasOne(b => b.Fuel);
            builder.HasOne(b => b.Trasmission);

            builder.HasMany(b => b.Cars);

            builder.HasQueryFilter(b => !b.DeleteDate.HasValue);
        }
    }
}
