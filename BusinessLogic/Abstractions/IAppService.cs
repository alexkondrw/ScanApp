namespace BusinessLogic.Abstractions;

public interface IAppService
{
    Task SaveFileHashesToDb(IEnumerable<string> files);
}