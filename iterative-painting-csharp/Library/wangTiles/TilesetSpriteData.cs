using Enums;

namespace WangTiles;

public class TilesetSpriteData
{
    public int Id { get; set; }
    public string StringId { get; set; }
    public int DataId { get; set; }
    public Int64 SequenceNumber { get; set; }
    public SpriteType Type { get; set; }
    public SpriteSize Size { get; set; }

    public int SpriteSheetId { get; set; }
    public int SpriteSheetRow { get; set; } // Starting from Upper Left corner
    public int SpriteSheetColumn { get; set; } // Starting from Upper left corner
    public bool IsDeprecated { get; set; }
    
    // Whether the element is
    // present in the sprite sheet png
    public bool IsDrawnInSpriteSheet { get; set; }
}