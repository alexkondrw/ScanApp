using BusinessLogic.Abstractions;

namespace BusinessLogic.Services;

public class FileService : IFileService
{
    public IEnumerable<string> FlattenAndEnumerateFiles(string folderPath)
    {
        IEnumerable<string> fileList = new List<string>();

        var files = Directory.EnumerateFiles(folderPath);
        fileList = fileList.Concat(files);

        var subDirectories = Directory.EnumerateDirectories(folderPath);

        return subDirectories.Aggregate(fileList, 
            (current, subDirectory) => current.Concat(FlattenAndEnumerateFiles(subDirectory)));
    }

    public long GetFileLength(string filePath)
    {
        var info = new FileInfo(filePath);
        return info.Length;
    }
}