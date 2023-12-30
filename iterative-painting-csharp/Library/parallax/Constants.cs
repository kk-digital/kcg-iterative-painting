namespace Parallax;

public class Constants
{
    public const int CurrentVersion = 0;
    
    public static readonly int TileSize = 32;
    public static readonly int SpriteSheetWidthInTiles = 7; // TODO(): Confusing names refactor
    public static readonly int SpritesheetTilesPerLine = 4; // TODO(): Confusing names refactor
    public static readonly int SpriteSheetSprites = 8;
    public static readonly int SpriteSheetHeightInTiles = SpritesheetTilesPerLine * SpriteSheetSprites;

    public static readonly string CacheFolderName = "asset-cache";
    public static readonly string TilesetsFolderName = "tilesets";
    
    public static readonly string TilesetFileName = "tileset.json";
    public static readonly string ManifestFileName = "tileset-manifest.json";
    
    public const int LatestSequenceNumber = -1;
    public const string AllTilesets = "AllTilesets";

    public const int TilesetIdRuntimeOffset = 1000;
    
    public static string GeometrySpriteSheetPath = "assets/textures/tiles/geometryMetal/metal_tiles_geometry.png";
}