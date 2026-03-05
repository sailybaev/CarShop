using CarShopFinal.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CarShopFinal.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly string _uploadsPath;

    public FileService(IWebHostEnvironment env)
    {
        _uploadsPath = Path.Combine(env.WebRootPath, "uploads");
        Directory.CreateDirectory(_uploadsPath);
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string originalFileName)
    {
        var ext = Path.GetExtension(originalFileName);
        var fileName = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(_uploadsPath, fileName);

        await using var output = File.Create(filePath);
        await fileStream.CopyToAsync(output);

        return fileName;
    }

    public void DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_uploadsPath, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}
