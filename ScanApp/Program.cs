using BusinessLogic.Abstractions;
using BusinessLogic.Services;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ScanApp;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        // Init logger and timer
        ILoggingService loggingService = new LoggingService();
        loggingService.StartTimer();
        
        // Check if arguments valid
        if (args.Length != 1)
        {
            await loggingService.LogErrorToCsvAsync(
                "Program accepts 1 argument only. If path has whitespaces, please put it inside double quotes");
            return 1;
        }
        var folderPath = args[0];
        if (!Directory.Exists(folderPath))
        {
            folderPath = args[0].TrimStart('"').TrimEnd('"');
            if (!Directory.Exists(folderPath))
            {
                await loggingService.LogErrorToCsvAsync("Argument is not existing folder path");
                return 1;
            }
        }
        
        // Init of DbContext
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=myApp.db")
            .Options;
        await using var db = new AppDbContext(dbOptions);
        
        // Init of Singleton Services
        using IUnitOfWork unitOfWork = new UnitOfWork(db);
        IFileService fileService = new FileService();
        IHashService hashService = new HashService();
        IAppService appService = new AppService(unitOfWork, fileService, hashService);
        
        // Enumerate files
        var files = fileService.FlattenAndEnumerateFiles(folderPath).ToList();
        
        // Write hashes to db
        try
        {
            await appService.SaveFileHashesToDb(files);
        }
        catch (Exception e)
        {
            await loggingService.LogErrorToCsvAsync(e.Message, e.InnerException?.Message);
        }
        
        // Write conclusion logs
        loggingService.LogFilesScannedToConsole(files.Count);
        loggingService.LogTimeTakenToConsole();
        
        return 0;
    }
}