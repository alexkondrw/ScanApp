using BusinessLogic.Abstractions;
using DataAccess.Models;

namespace BusinessLogic.Services;

public class AppService : IAppService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IHashService _hashService;

    public AppService(IUnitOfWork unitOfWork, IFileService fileService, IHashService hashService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _hashService = hashService;
    }

    public async Task SaveFileHashesToDb(IEnumerable<string> files)
    {
        foreach (var file in files)
            await SaveHashesForFile(file);

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task SaveHashesForFile(string filePath)
    {
        var found = await _unitOfWork.Hash.FindAsync(filePath);
        if (found is null)
        {
            await _unitOfWork.Hash.AddAsync(new Hash
            {
                FilePath = filePath,
                FileSize = _fileService.GetFileLength(filePath),
                Md5 = _hashService.CalculateMd5ForFile(filePath),
                Sha1 = _hashService.CalculateSha1ForFile(filePath),
                Sha256 = _hashService.CalculateSha256ForFile(filePath),
                LastSeen = DateTime.UtcNow,
                Scanned = 1
            });
        }
        else
        {
            var sha256 = _hashService.CalculateSha256ForFile(filePath);
            if (sha256 != found.Sha256)
            {
                found.Md5 = _hashService.CalculateMd5ForFile(filePath);
                found.Sha1 = _hashService.CalculateSha1ForFile(filePath);
                found.Sha256 = sha256;
                found.LastSeen = DateTime.UtcNow;
                found.Scanned++;
                await _unitOfWork.Hash.UpdateAsync(found);
            }
        }
    }
}