using CarShopFinal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShopFinal.Persistance.Configurations;

public class SellerConfiguration:IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithOne().HasForeignKey<Seller>(x => x.UserId).IsRequired();
        builder.Property(x => x.CompanyEmail).IsRequired();
        builder.Property(x => x.CompanyPhoneNumber).IsRequired();
        builder.Property(x => x.CompanyCity).IsRequired();
        builder.Property(x => x.CompanyName).IsRequired();
        builder.Property(x => x.CompanyAddress).IsRequired();
    }
}