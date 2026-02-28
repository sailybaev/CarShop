using CarShopFinal.Domain.Interfaces;
using CarShopFinal.Domain.Models;

namespace CarShopFinal.Persistance.Dependency;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<string> RegisterUser(string email, string password)
    {
        var exist = await _userRepository.GetByEmailAsync(email);
        if(exist != null)
            throw new Exception("User already exist");
        
        var hash = _passwordHasher.HashPassword(password);
        var user = new User(email, hash, "Customer");

        await _userRepository.AddAsync(user);
        return _tokenService.GenerateToken(user);
    }

    public async Task<string> LoginUser(string email, string password)
    {
        var exist = await _userRepository.GetByEmailAsync(email);
        if(exist == null)
            throw new Exception("User not found");
        
        if(!_passwordHasher.VerifyHashedPassword(password,exist.hashPass))
            throw new Exception("Wrong password");
        
        return _tokenService.GenerateToken(exist);
    }
}