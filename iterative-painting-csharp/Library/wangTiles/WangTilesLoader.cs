using Enums;
using KEngine;
using Utility;

namespace WangTiles;

public class WangTilesLoader
{
    public List<WangTilesLoaderTileset> LoadedTilesets = new();
    public Dictionary<int, WangTilesLoaderTileset> LoaderTilesetsDictionary = new();

    public void LoadWangTiles(int loadedTilesetId = Constants.AllTilesets, int sequencenumber = Constants.LatestSequenceNumber)
    {
        LoadedTilesets = new List<WangTilesLoaderTileset>();
        LoaderTilesetsDictionary = new Dictionary<int, WangTilesLoaderTileset>();
        
        // The files in the folder look like this
        // asset-cache/tilesets/000/s0001/tileset.json
        // asset-cache/tilesets/000/s0002/tileset.json
        
        string tilesetFolderPath = $"{Constants.CacheFolderName}/{Constants.TilesetsFolderName}";
        
        string tilesetFileName = Constants.TilesetFileName;
        string manifestFileName = Constants.ManifestFileName;

        if (!FileUtils.DirectoryExistsFull(tilesetFolderPath))
        {
            return;
        }
        
        // Tileset folders
        // Each tileset will be in a folder
        // asset-cache/tilesets/000/
        // asset-cache/tilesets/001/
        string[] tilesetFolders = 
            FileUtils.GetDirectoriesFull(tilesetFolderPath, "*", KEngine.SearchOption.TopDirectoryOnly);
        
        // For each tileset we iterate over
        // The versions and pick the latest version
        // Then load the manifest for that version
        foreach (string tilesetFolder in tilesetFolders)
        {

            if (loadedTilesetId != Constants.AllTilesets)
            {
                string folderCorrected = tilesetFolder.Replace('\\', '/');
                string idString = folderCorrected.Split('/').LastOrDefault();
                int idInt = int.Parse(idString);

                if (idInt != loadedTilesetId)
                {
                    continue;
                }
            }
            
            if (!FileUtils.DirectoryExistsFull(tilesetFolder))
            {
                return;
            }
            
            string[] verionFolders =
                FileUtils.GetDirectoriesFull(tilesetFolder, "*", KEngine.SearchOption.TopDirectoryOnly);

            string chosenTilesetPath = "";
            if (sequencenumber == Constants.LatestSequenceNumber)
            {
                // Pick latest version
                // Extract version numbers and sort the paths based on them
                var sortedPaths = verionFolders.OrderBy(path =>
                {
                    string versionString = path.Split('/').LastOrDefault();

                    // Parse the version number (assuming it's an integer)
                    int versionNumber;
                    if (int.TryParse(versionString, out versionNumber))
                    {
                        return versionNumber;
                    }

                    // Default to 0 if parsing fails
                    return 0;
                });

                // Pick the last path
                string lastPath = sortedPaths.LastOrDefault();

                chosenTilesetPath = lastPath;
            }
            else
            {
                foreach (var folder in verionFolders)
                {
                    string sequencenumberString = folder.Replace("\\", "/");
                    sequencenumberString = sequencenumberString.Split('/').LastOrDefault();
                    int sequencenumberInt = ExtractAndConvertToInt(sequencenumberString);

                    if (sequencenumberInt == sequencenumber)
                    {
                        chosenTilesetPath = folder;
                        break;
                    }
                }
            }
            
            string vString = chosenTilesetPath.Split('/').LastOrDefault();
            int vInt = ExtractAndConvertToInt(vString);
            
            if (string.IsNullOrEmpty(vString))
            {
                KLog.LogDebug($"could not find the tileset folder folder sequence number {sequencenumber}");
                return;
            }

            // Path to the tileset & manifest
            string tilesetPath = $"{chosenTilesetPath}/{tilesetFileName}";
            string manifestPath = $"{chosenTilesetPath}/{manifestFileName}";

            WangTilesLoaderTileset tilesetLoader = new WangTilesLoaderTileset();
            
            // decode tileset json
            byte[] tilesetBytes = FileUtils.ReadAllBytesFull(tilesetPath);
            Tileset tileset = TilesetDataDecoder.Decode<Tileset>(tilesetBytes);

            // decode manifest json
            byte[] manifestBytes = FileUtils.ReadAllBytesFull(manifestPath);
            // Decode bytes to string using UTF-8 encoding
            TilesetManifest manifest = TilesetDataDecoder.Decode<TilesetManifest>(manifestBytes);

            tilesetLoader.Version = vInt;
                
            tilesetLoader.Manifest = manifest;

            tilesetLoader.Tileset = tileset;
            
            tilesetLoader.TilesetCorners = new List<TilesetCorner>();
            tilesetLoader.TilesetHorizontalEdges = new List<TilesetHorizontalEdge>();
            tilesetLoader.TilesetVerticalEdges = new List<TilesetVerticalEdge>();
            tilesetLoader.TilesetTiles = new List<TilesetTile>();
            tilesetLoader.TilesetSpriteSheetDatas = new List<TilesetSpriteSheetData>();
            tilesetLoader.TilesetSpriteDatas = new List<TilesetSpriteData>();
            
            tilesetLoader.TilesetCornersDictionary = new Dictionary<int, TilesetCorner>();
            tilesetLoader.TilesetHorizontalEdgesDictionary = new Dictionary<int, TilesetHorizontalEdge>();
            tilesetLoader.TilesetVerticalEdgesDictionary = new Dictionary<int, TilesetVerticalEdge>();
            tilesetLoader.TilesetTilesDictionary = new Dictionary<int, TilesetTile>();
            tilesetLoader.TilesetSpriteSheetDatasDictionary = new Dictionary<int, TilesetSpriteSheetData>();
            tilesetLoader.TilesetSpriteDatasDictionary = new Dictionary<int, TilesetSpriteData>();
            
            List<TilesetManifestItem> manifestItems = manifest.Files;

            foreach(TilesetManifestItem item in manifestItems)
            {
                switch (item.DataType)
                {
                    case DataType.Corner:
                    {
                        // decode json
                        byte[] bytes = FileUtils.ReadAllBytesFull(item.Filepath);
                        TilesetCorner corner = TilesetDataDecoder.Decode<TilesetCorner>(bytes);
                        
                        tilesetLoader.TilesetCorners.Add(corner);
                        tilesetLoader.TilesetCornersDictionary.Add(corner.Id, corner);
                        break;
                    }
                    case DataType.SpriteData:
                    {
                        // decode json
                        byte[] bytes = FileUtils.ReadAllBytesFull(item.Filepath);
                        TilesetSpriteData sprite = TilesetDataDecoder.Decode<TilesetSpriteData>(bytes);
                        
                        tilesetLoader.TilesetSpriteDatas.Add(sprite);
                        tilesetLoader.TilesetSpriteDatasDictionary.Add(sprite.Id, sprite);
                        break;
                    }
                    case DataType.HorizontalEdge:
                    {
                        // decode json
                        byte[] bytes = FileUtils.ReadAllBytesFull(item.Filepath);
                        TilesetHorizontalEdge edge = TilesetDataDecoder.Decode<TilesetHorizontalEdge>(bytes);
                        
                        tilesetLoader.TilesetHorizontalEdges.Add(edge);
                        tilesetLoader.TilesetHorizontalEdgesDictionary.Add(edge.Id, edge);
                        break;
                    }
                    case DataType.VerticalEdge:
                    {
                        // decode json
                        byte[] bytes = FileUtils.ReadAllBytesFull(item.Filepath);
                        TilesetVerticalEdge edge = TilesetDataDecoder.Decode<TilesetVerticalEdge>(bytes);
                        
                        tilesetLoader.TilesetVerticalEdges.Add(edge);
                        tilesetLoader.TilesetVerticalEdgesDictionary.Add(edge.Id, edge);
                        break;
                    }
                    case DataType.Tile:
                    {
                        // decode json
                        byte[] bytes = FileUtils.ReadAllBytesFull(item.Filepath);
                        TilesetTile tile = TilesetDataDecoder.Decode<TilesetTile>(bytes);
                        
                        tilesetLoader.TilesetTiles.Add(tile);
                        tilesetLoader.TilesetTilesDictionary.Add(tile.Id, tile);
                        break;
                    }
                    case DataType.SpriteSheetData:
                    {
                        // decode json
                        byte[] bytes = FileUtils.ReadAllBytesFull(item.Filepath);
                        TilesetSpriteSheetData spriteSheetData = TilesetDataDecoder.Decode<TilesetSpriteSheetData>(bytes);
                        
                        tilesetLoader.TilesetSpriteSheetDatas.Add(spriteSheetData);
                        tilesetLoader.TilesetSpriteSheetDatasDictionary.Add(spriteSheetData.Id, spriteSheetData);
                        break;
                    }
                }
            }
            
            LoadedTilesets.Add(tilesetLoader);
            LoaderTilesetsDictionary.Add(tilesetLoader.Tileset.Id, tilesetLoader);
        }
    }
    
    // Convert the version string to int
    // example 's001' or 's005' 
    public static int ExtractAndConvertToInt(string input)
    {
        // Check if the string starts with "s" and has at least one digit
        if (input.Length > 1 && input[0] == 's' && Char.IsDigit(input[1]))
        {
            // Extract numeric part and convert to int
            string numericPart = input.Substring(1); // Skip the 's'
            return int.Parse(numericPart);
        }
        else
        {
            // Handle invalid format
            return 0; // or throw an exception, return a default value, etc.
        }
    }
}

public class WangTilesLoaderTileset
{
    public int Version;
    
    public TilesetManifest Manifest;

    public Tileset Tileset;

    public List<TilesetCorner> TilesetCorners;
    public List<TilesetHorizontalEdge> TilesetHorizontalEdges;
    public List<TilesetVerticalEdge> TilesetVerticalEdges;
    public List<TilesetTile> TilesetTiles;
    public List<TilesetSpriteData> TilesetSpriteDatas;
    public List<TilesetSpriteSheetData> TilesetSpriteSheetDatas;
    
    public Dictionary<int, TilesetCorner> TilesetCornersDictionary;
    public Dictionary<int, TilesetHorizontalEdge> TilesetHorizontalEdgesDictionary;
    public Dictionary<int, TilesetVerticalEdge> TilesetVerticalEdgesDictionary;
    public Dictionary<int, TilesetTile> TilesetTilesDictionary;
    public Dictionary<int, TilesetSpriteData> TilesetSpriteDatasDictionary;
    public Dictionary<int, TilesetSpriteSheetData> TilesetSpriteSheetDatasDictionary;

}