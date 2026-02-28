namespace CarShopFinal.Domain.Models;

public class User:AggregateRoot
{
    public string email { get; private set; }
    public string hashPass { get; private set; }
    public string role { get; private set; }
    
    private User(){}

    public User(string email, string hashPass, string role)
    {
        this.email = email;
        this.hashPass = hashPass;
        this.role = role;
        SetCreatedAt();
    }
}