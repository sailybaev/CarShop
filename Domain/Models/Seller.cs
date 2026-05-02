namespace CarShopFinal.Domain.Models;

public class Seller:AggregateRoot
{
    public string CompanyName { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public User User { get; private set; }
    public string CompanyCity { get; private set; }
    public string CompanyAddress { get; private set; }
    public string CompanyPhoneNumber { get; private set; }
    public string CompanyEmail { get; private set; }
    public bool IsVerified { get; private set; }
    
    private Seller() { }

    public Seller(string companyName, Guid userId, string companyCity, string companyAddress, string companyPhoneNumber,
        string companyEmail)
    {
        ValidateName(companyName);
        ValidateEmail(companyEmail);
        ValidateCity(companyCity);
        ValidatePhone(companyPhoneNumber);
        
        Id = Guid.NewGuid();
        UserId = userId;
        CompanyName = companyName;
        CompanyCity = companyCity;
        CompanyAddress = companyAddress;
        CompanyPhoneNumber = companyPhoneNumber;
        CompanyEmail = companyEmail;
        IsVerified = false;
        
        SetUpdatedAt();
    }

    public void UpdateContactInfo(string companyPhoneNumber, string companyEmail)
    {
        ValidatePhone(companyPhoneNumber);
        ValidateEmail(companyEmail);
        
        CompanyPhoneNumber = companyPhoneNumber;
        CompanyEmail = companyEmail;
        SetUpdatedAt();
    }
    public void UpdateCityAndAddress(string companyCity, string companyAddress)
    {
        ValidateCity(companyCity);
        CompanyCity = companyCity;
        CompanyAddress = companyAddress;
        SetUpdatedAt();
    }

    public void VerifySeller()
    {
        IsVerified = true;
        SetUpdatedAt();
    }
    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Seller name cannot be empty.");
    }

    private void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone number cannot be empty.");
    }

    private void ValidateCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty.");
    }

    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");
        
        if(!email.Contains("@"))
            throw new ArgumentException("Email must contain at least one '@'.");
    }
}