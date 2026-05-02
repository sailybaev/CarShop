using CarShopFinal.Domain.Models;
using CarShopFinal.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShopFinal.Persistance.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.CustomerId)
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired();

        // Map OrderItems as an owned (owned collection) type of Order
        builder.OwnsMany(o => o.OrderItems, oi =>
        {
            oi.WithOwner().HasForeignKey("OrderId");
            oi.HasKey(i => i.Id);

            oi.Property(i => i.CarId)
                .HasColumnName("CarId")
                .IsRequired();

            // Configure Money value object inside OrderItem
            oi.OwnsOne(i => i.price, m =>
            {
                m.Property(p => p.Amount)
                    .HasColumnName("Price")
                    .IsRequired();

                m.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .IsRequired()
                    .HasMaxLength(4);
            });

            oi.ToTable("OrderItems");
        });

        // Map PaymentItems as an owned collection of Order
        builder.OwnsMany(o => o.PaymentItems, pi =>
        {
            pi.WithOwner().HasForeignKey("OrderId");
            pi.HasKey(p => p.Id);

            // Configure Money value object inside PaymentItem
            pi.OwnsOne(p => p.Price, m =>
            {
                m.Property(p => p.Amount)
                    .HasColumnName("Amount")
                    .IsRequired();

                m.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .IsRequired()
                    .HasMaxLength(4);
            });

            pi.Property(p => p.Status)
                .HasColumnName("Status")
                .IsRequired();

            pi.Property(p => p.PaidAt)
                .HasColumnName("PaidAt");

            pi.ToTable("PaymentItems");
        });
    }
}

