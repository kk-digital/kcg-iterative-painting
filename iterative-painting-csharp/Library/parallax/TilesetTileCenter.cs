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
    
    public int CornerNorthWest { get; set; }
    public int CornerNorthEast { get; set; }
    public int CornerSouthEast { get; set; }
    public int CornerSouthWest { get; set; }

    public int EdgeVerticalEast { get; set; }
    public int EdgeVerticalWest { get; set; }

    public int EdgeHorizontalNorth { get; set; }
    public int EdgeHorizontalSouth { get; set; }
    
    public bool IsDeprecated { get; set; }
}