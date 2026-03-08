namespace CarShopFinal.Domain.Models;

public class ChatMessage:AggregateRoot
{
   public Guid senderID { get; private set; }
   public Guid receiverID { get; private set; }
   public Guid listingID { get; private set; }
   public string message { get; private set; } = string.Empty;
   public bool isRead { get; private set; } = false;
   public User sender { get; private set; }
   public User receiver { get; private set; }
   public Listing listing { get; private set; }
   
   private ChatMessage() { }

   public ChatMessage(Guid senderID, Guid receiverID, Guid listingID, string message)
   {
      Id = Guid.NewGuid();
      this.senderID = senderID;
      this.receiverID = receiverID;
      this.listingID = listingID;
      this.message = message;
      SetCreatedAt();
   }
   public void MarkAsRead()
   {
      isRead = true;
      SetUpdatedAt();
   }
   
}