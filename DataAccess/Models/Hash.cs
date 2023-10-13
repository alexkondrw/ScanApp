using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Hash
{
    [Key] public string FilePath { get; set; } = null!;
    public string Md5 { get; set; } = null!;
    public string Sha1 { get; set; } = null!;
    public string Sha256 { get; set; } = null!;
    public long FileSize { get; set; }
    public DateTime LastSeen { get; set; }
    public int Scanned { get; set; }
}