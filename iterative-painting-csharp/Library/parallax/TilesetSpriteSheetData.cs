
using KcgData;

namespace Parallax;

public class TilesetSpriteSheetData
{
    public UInt64 Uuid { get; set; }
    public string StringId { get; set; }
    public Int64 SequenceNumber { get; set; }
    public string Description { get; set; }
    public string Filepath { get; set; }

    public SpriteSheetOffset SpriteSheetOffset { get; set; }

    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsDeprecated { get; set; }
}