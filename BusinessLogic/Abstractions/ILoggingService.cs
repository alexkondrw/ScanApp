namespace BusinessLogic.Abstractions;

public interface ILoggingService
{
    Task LogErrorToCsvAsync(string errorMessage, string? innerEx = null);
    void StartTimer();
    void LogTimeTakenToConsole();
    void LogFilesScannedToConsole(int filesScanned);
}