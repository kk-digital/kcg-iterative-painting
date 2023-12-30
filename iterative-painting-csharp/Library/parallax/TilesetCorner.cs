namespace Parallax;


public class TilesetCorner
{
    public UInt64 Uuid { get; set; }
    public UInt64 Tileset { get; set; }
    public string StringId { get; set; }
    public Int64 SequenceNumber { get; set; }
    public UInt64 SpriteUuid { get; set; }
    public string Description { get; set; }
    public int ColorId { get; set; }
    public bool IsDeprecated { get; set; }
}