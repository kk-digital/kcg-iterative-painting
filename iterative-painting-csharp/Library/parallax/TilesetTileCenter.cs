namespace Parallax;


public class TilesetTileCenter
{
    public UInt64 Uuid { get; set; }
    public string StringId { get; set; }
    public Int64 SequenceNumber { get; set; }
    public string Description { get; set; }

    public bool IsDefaultGeometryTile { get; set; }

    public int SpriteId { get; set; }
    
    public Enums.TileGeometryAndRotation Geometry { get; set; }
    public Enums.PlanetTileMap.MapLayerType Layer { get; set; }
    
    public UInt64 CornerNorthWest { get; set; }
    public UInt64 CornerNorthEast { get; set; }
    public UInt64 CornerSouthEast { get; set; }
    public UInt64 CornerSouthWest { get; set; }

    public UInt64 EdgeVerticalEast { get; set; }
    public UInt64 EdgeVerticalWest { get; set; }

    public UInt64 EdgeHorizontalNorth { get; set; }
    public UInt64 EdgeHorizontalSouth { get; set; }
    
    public bool IsDeprecated { get; set; }
}