namespace CarShopFinal.Domain.Models;

public class Customer : AggregateRoot 
{
    public Guid UserID { get; private set; }
    public User User { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    
    private Customer() { }
    
    public Customer(Guid userId, string firstName, string lastName, string email, string phoneNumber)
    {
        ValidateName(firstName, lastName);
        ValidateEmail(email);
        ValidatePhoneNumber(phoneNumber);
        
        Id = Guid.NewGuid();
        UserID = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        
        SetCreatedAt();
    }
    
    public void UpdateContactInfo(string email, string phoneNumber)
    {
        ValidateEmail(email);
        ValidatePhoneNumber(phoneNumber);
        
        Email = email;
        PhoneNumber = phoneNumber;
        SetUpdatedAt();
    }

    private void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName)|| string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("First name and last name cannot be empty");
    }

    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
        if (!email.Contains("@"))
            throw new ArgumentException("Email must contain @");
    }

    private void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be empty");
    }
}