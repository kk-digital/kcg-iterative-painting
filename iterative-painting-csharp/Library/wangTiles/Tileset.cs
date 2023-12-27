namespace WangTiles;

public class Tileset
{
    public int Id { get; set; }
    public string StringId { get; set; }
    public string Description { get; set; }
    public Int64 SequenceNumber { get; set; }
    public string SequenceNumberString { get; set; }
    
    public bool IsDeprecated { get; set; }

}