using CarShopFinal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShopFinal.Persistance.Configurations;

public class ChatMessageConfiguration:IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.message).IsRequired().HasMaxLength(2000);
        builder.HasOne(x => x.sender).WithMany().HasForeignKey(c => c.senderID);
        builder.HasOne(x => x.receiver).WithMany().HasForeignKey(c => c.receiverID);
        builder.HasOne(x => x.listing).WithMany().HasForeignKey(c => c.listingID).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => x.receiverID);
        builder.HasIndex(x => x.senderID);
        builder.HasIndex(x => x.listingID);
        builder.HasIndex(x => x.CreatedAt);
    }
}