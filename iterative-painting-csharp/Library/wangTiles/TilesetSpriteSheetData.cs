
using KcgData;

namespace WangTiles;

public class TilesetSpriteSheetData
{
    public int Id { get; set; }
    public string StringId { get; set; }
    public Int64 SequenceNumber { get; set; }
    public string Description { get; set; }
    public string Filepath { get; set; }

    public SpriteSheetOffset SpriteSheetOffset { get; set; }

    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsDeprecated { get; set; }
}