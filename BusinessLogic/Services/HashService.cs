using System.Security.Cryptography;
using BusinessLogic.Abstractions;

namespace BusinessLogic.Services;

public class HashService : IHashService
{
    public string CalculateMd5ForFile(string filePath)
    {
        using var algorithm = MD5.Create();
        return CalculateHashForFile(filePath, algorithm);
    }

    public string CalculateSha1ForFile(string filePath)
    {
        using var algorithm = SHA1.Create();
        return CalculateHashForFile(filePath, algorithm);
    }

    public string CalculateSha256ForFile(string filePath)
    {
        using var algorithm = SHA256.Create();
        return CalculateHashForFile(filePath, algorithm);
    }

    private string CalculateHashForFile(string filePath, HashAlgorithm algorithm)
    {
        using var stream = File.OpenRead(filePath);
        var hashBytes = algorithm.ComputeHash(stream);
        stream.Close();
        return BitConverter.ToString(hashBytes).Replace("-", "");
    }
}