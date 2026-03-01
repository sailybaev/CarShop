using CarShopFinal.Domain.Enums;

namespace CarShopFinal.Domain.Models;

public class Listing:AggregateRoot
{

    public Guid SellerId { get; private set; }
    public ModerationStatus? Status { get; private set; }
    
    public Car Car { get; private set; } //TODO: вырезать везде Car
    
    public Guid CarId { get; private set; }
    public int View { get; private set; }
    public string City { get; private set; }
    public string Description { get; private set; }
    public List<Guid> CarImage {get; private set;}
    public ListingStatus ListingStatus { get; private set; }

    public Listing(){}

    public Listing(Guid sellerId, ModerationStatus? status, Car car, int view, string city, string description,
        List<Guid> carImage)
    {
        Id = Guid.NewGuid();
        SellerId = sellerId;
        Status = status;
        Car = car;
        CarId = car.Id;
        View = view;
        City = city ?? throw new ArgumentNullException(nameof(city));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        CarImage = carImage ?? throw new ArgumentNullException(nameof(carImage));
        ListingStatus = ListingStatus.Draft;
        SetCreatedAt();
    }

    public void Update(string city, string description)
    {
        City = city ?? throw new ArgumentNullException(nameof(city));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        SetUpdatedAt();
    }

    public void Approve()
    {
        Status = ModerationStatus.Approved;
        ListingStatus = ListingStatus.Published;

    }

    public void Reject()
    {
        Status = ModerationStatus.Rejected;
        ListingStatus = ListingStatus.Draft;
    }
}