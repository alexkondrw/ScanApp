using BusinessLogic.Abstractions;

namespace BusinessLogic.Services;

public class LoggingService : ILoggingService
{
    private const string ErrorFilePath = "error_log.csv";
    private DateTime _startTime;

    public async Task LogErrorToCsvAsync(string errorMessage, string? innerEx = null)
    {
        string? firstLine = null;
        if (!File.Exists(ErrorFilePath))
            firstLine = "TimeHappened,ErrorMessage,InnerErrorMessage";
        
        await using var writer = new StreamWriter(ErrorFilePath, true);

        if (firstLine is not null)
            await writer.WriteLineAsync(firstLine);
        
        await writer.WriteLineAsync($"{DateTime.UtcNow},{errorMessage},{innerEx??"none"}");
    }

    public void StartTimer()
    {
        _startTime = DateTime.UtcNow;
        Console.WriteLine("Timer started");
    }
    
    public void LogTimeTakenToConsole()
    {
        var endTime = DateTime.UtcNow;
        var totalTime = (endTime - _startTime).TotalMilliseconds;
        Console.WriteLine("Total time in ms is " + totalTime);
    }

    public void LogFilesScannedToConsole(int filesScanned)
    {
        Console.WriteLine("Total files scanned is " + filesScanned);
    }
}