namespace CarShopFinal.Domain.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(Stream fileStream, string originalFileName);
    void DeleteFile(string fileName);
}
