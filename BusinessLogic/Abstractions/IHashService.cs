namespace BusinessLogic.Abstractions;

public interface IHashService
{
    public string CalculateMd5ForFile(string filePath);
    public string CalculateSha1ForFile(string filePath);
    public string CalculateSha256ForFile(string filePath);
}