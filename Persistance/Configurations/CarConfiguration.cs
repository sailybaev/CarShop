using CarShopFinal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShopFinal.Persistance.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Brand)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(x => x.Vin, vin =>
        {
	        vin.Property(v => v.Value)
		        .HasColumnName("Vin")
		        .IsRequired()
		        .HasMaxLength(17);
        });

        builder.OwnsOne(x => x.Price, price =>
        {
	        price.Property(p => p.Amount)
		        .HasColumnName("Price")
		        .IsRequired();
            
            price.Property(p => p.Currency)
                .HasColumnName("Currency")
                .IsRequired()
                .HasMaxLength(4); 
        });




    }
}
