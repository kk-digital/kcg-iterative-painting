namespace Parallax;

public class TilesetInformation
{
    public UInt64 Uuid { get; set; }
    public UInt64 TilesetId { get; set; }
    public string StringId { get; set; }
    public string Description { get; set; }
    public Int64 SequenceNumber { get; set; }
    public string SequenceNumberString { get; set; }
    
    public bool IsDeprecated { get; set; }

}