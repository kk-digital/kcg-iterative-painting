namespace Parallax;

public class ParallaxManager
{
    // Object list
    public List<TilesetInformation> Tilesets;
    public List<TilesetCorner> TilesetCorners;
    public List<TilesetEdgeHorizontal> TilesetHorizontalEdges;
    public List<TilesetEdgeVertical> TilesetVerticalEdges;
    public List<TilesetTileCenter> TilesetTiles;
    public List<TilesetSpriteData> TilesetSpriteDatas;
    public List<TilesetSpriteSheetData> TilesetSpriteSheetDatas;
    
    // Maps Object UUID to object list
    // Index (UUID => Index)
    public Dictionary<UInt64, int> TilesetsDictionary;
    public Dictionary<UInt64, int> TilesetCornersDictionary;
    public Dictionary<UInt64, int> TilesetHorizontalEdgesDictionary;
    public Dictionary<UInt64, int> TilesetVerticalEdgesDictionary;
    public Dictionary<UInt64, int> TilesetTilesDictionary;
    public Dictionary<UInt64, int> TilesetSpriteDatasDictionary;
    public Dictionary<UInt64, int> TilesetSpriteSheetDatasDictionary;


    public void InitStage1()
    {
        Tilesets = new List<TilesetInformation>();
        TilesetCorners = new List<TilesetCorner>();
        TilesetHorizontalEdges = new List<TilesetEdgeHorizontal>();
        TilesetVerticalEdges = new List<TilesetEdgeVertical>();
        TilesetTiles = new List<TilesetTileCenter>();
        TilesetSpriteDatas = new List<TilesetSpriteData>();
        TilesetSpriteSheetDatas = new List<TilesetSpriteSheetData>();

        TilesetsDictionary = new Dictionary<UInt64, int>();
        TilesetCornersDictionary = new Dictionary<UInt64, int>();
        TilesetHorizontalEdgesDictionary = new Dictionary<UInt64, int>();
        TilesetVerticalEdgesDictionary = new Dictionary<UInt64, int>();
        TilesetTilesDictionary = new Dictionary<UInt64, int>();
        TilesetSpriteDatasDictionary = new Dictionary<UInt64, int>();
        TilesetSpriteSheetDatasDictionary = new Dictionary<UInt64, int>();
    }

    public void InitStage2()
    {
        
    }

    public TilesetInformation GetTileset(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetsDictionary.ContainsKey(uuid))
        {
            return null;
        }

        int index = TilesetsDictionary[uuid];
        
        // Make sure index is withing
        // Object list bounds
        if (index < 0 && index >= Tilesets.Count)
        {
            return null;
        }
        
        return Tilesets[index];
    }
    
    public TilesetTileCenter GetTileCenter(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetTilesDictionary.ContainsKey(uuid))
        {
            return null;
        }

        int index = TilesetTilesDictionary[uuid];
        
        // Make sure index is withing
        // Object list bounds
        if (index < 0 && index >= TilesetTiles.Count)
        {
            return null;
        }
        
        return TilesetTiles[index];
    }
    
    public TilesetCorner GetCorner(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetCornersDictionary.ContainsKey(uuid))
        {
            return null;
        }

        int index = TilesetCornersDictionary[uuid];
        
        // Make sure index is withing
        // Object list bounds
        if (index < 0 && index >= TilesetCorners.Count)
        {
            return null;
        }
        
        return TilesetCorners[index];
    }
    
    public TilesetEdgeHorizontal GetEdgeHorizontal(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetHorizontalEdgesDictionary.ContainsKey(uuid))
        {
            return null;
        }

        int index = TilesetHorizontalEdgesDictionary[uuid];
        
        // Make sure index is withing
        // Object list bounds
        if (index < 0 && index >= TilesetHorizontalEdges.Count)
        {
            return null;
        }
        
        return TilesetHorizontalEdges[index];
    }
    
    public TilesetEdgeVertical GetEdgeVertical(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetVerticalEdgesDictionary.ContainsKey(uuid))
        {
            return null;
        }

        int index = TilesetVerticalEdgesDictionary[uuid];
        
        // Make sure index is withing
        // Object list bounds
        if (index < 0 && index >= TilesetVerticalEdges.Count)
        {
            return null;
        }
        
        return TilesetVerticalEdges[index];
    }
    
    public TilesetSpriteData GetSprite(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetSpriteDatasDictionary.ContainsKey(uuid))
        {
            return null;
        }

        int index = TilesetSpriteDatasDictionary[uuid];
        
        // Make sure index is withing
        // Object list bounds
        if (index < 0 && index >= TilesetSpriteDatas.Count)
        {
            return null;
        }
        
        return TilesetSpriteDatas[index];
    }
    
    public TilesetSpriteSheetData GetSpriteSheet(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetSpriteSheetDatasDictionary.ContainsKey(uuid))
        {
            return null;
        }

        int index = TilesetSpriteSheetDatasDictionary[uuid];
        
        // Make sure index is withing
        // Object list bounds
        if (index < 0 && index >= TilesetSpriteSheetDatas.Count)
        {
            return null;
        }
        
        return TilesetSpriteSheetDatas[index];
    }
}