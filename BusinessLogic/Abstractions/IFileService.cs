namespace BusinessLogic.Abstractions;

public interface IFileService
{
    public IEnumerable<string> FlattenAndEnumerateFiles(string folderPath);
    public long GetFileLength(string filePath);
}