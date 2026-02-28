using CarShopFinal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShopFinal.Persistance.Configurations;

public class ListingConfiguration:IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(x => x.Id);
        //builder.HasOne<Car>().WithMany().HasForeignKey(x => x.Car.Id);
        builder.HasOne(c => c.Car).WithOne(t=>t.Listing).HasForeignKey<Listing>(c => c.CarId);
    }
}