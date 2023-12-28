using Utility;

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
    
    public Dictionary<UInt64, TilesetData> TilesetDatas;
    
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

        TilesetDatas = new Dictionary<ulong, TilesetData>();
    }

    public void InitStage2()
    {
        
    }
    
    #region Getters
    
    public TilesetData GetTilesetData(UInt64 uuid)
    {
        // If does not have the uuid
        // will return null ptr
        if (!TilesetDatas.ContainsKey(uuid))
        {
            return null;
        }
        
        return TilesetDatas[uuid];
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
    
    #endregion

    public TilesetInformation CreateTileset()
    {
        UInt64 uuid = ShortHash.GenerateUUID();
        
        // Keep trying until we get a unique uuid
        while (TilesetsDictionary.ContainsKey(uuid))
        {
            uuid = ShortHash.GenerateUUID();
        }

        TilesetInformation tilesetInformation = new TilesetInformation();

        TilesetData tilesetData = new TilesetData
        {
            Tileset = uuid
        };
        
        // Fatal error checks
        Utils.Assert(!TilesetDatas.ContainsKey(uuid));
        Utils.Assert(!TilesetsDictionary.ContainsKey(uuid));
        
        TilesetDatas.Add(uuid, tilesetData);
        Tilesets.Add(tilesetInformation);

        int tilesetIndex = Tilesets.Count - 1;
        
        TilesetsDictionary.Add(uuid, tilesetIndex);

        return tilesetInformation;
    }
    
    public TilesetTileCenter AddTileCenter(UInt64 tilesetId)
    {
        // Tileset existence
        TilesetInformation tilesetInformation = GetTileset(tilesetId);
        TilesetData tilesetData = GetTilesetData(tilesetId);
            
        if (tilesetInformation == null || tilesetData == null)
        {
            return null;
        }
        
        UInt64 uuid = ShortHash.GenerateUUID();
        
        // Keep trying until we get a unique uuid
        while (TilesetTilesDictionary.ContainsKey(uuid))
        {
            uuid = ShortHash.GenerateUUID();
        }

        TilesetTileCenter tileCenter = new TilesetTileCenter();

        tilesetData.TilesetTiles.Add(uuid);
        TilesetTiles.Add(tileCenter);

        int tileIndex = TilesetTiles.Count - 1;
        
        TilesetTilesDictionary.Add(uuid, tileIndex);

        return tileCenter;
    }
    
    public TilesetCorner AddCorner(UInt64 tilesetId)
    {
        // Tileset existence
        TilesetInformation tilesetInformation = GetTileset(tilesetId);
        TilesetData tilesetData = GetTilesetData(tilesetId);
            
        if (tilesetInformation == null || tilesetData == null)
        {
            return null;
        }
        
        UInt64 uuid = ShortHash.GenerateUUID();
        
        // Keep trying until we get a unique uuid
        while (TilesetCornersDictionary.ContainsKey(uuid))
        {
            uuid = ShortHash.GenerateUUID();
        }

        TilesetCorner corner = new TilesetCorner();

        tilesetData.TilesetCorners.Add(uuid);
        TilesetCorners.Add(corner);

        int index = TilesetCorners.Count - 1;
        
        TilesetCornersDictionary.Add(uuid, index);

        return corner;
    }
    
    public TilesetEdgeHorizontal AddEdgeHorizontal(UInt64 tilesetId)
    {
        // Tileset existence
        TilesetInformation tilesetInformation = GetTileset(tilesetId);
        TilesetData tilesetData = GetTilesetData(tilesetId);
            
        if (tilesetInformation == null || tilesetData == null)
        {
            return null;
        }
        
        UInt64 uuid = ShortHash.GenerateUUID();
        
        // Keep trying until we get a unique uuid
        while (TilesetHorizontalEdgesDictionary.ContainsKey(uuid))
        {
            uuid = ShortHash.GenerateUUID();
        }

        TilesetEdgeHorizontal edge = new TilesetEdgeHorizontal();

        tilesetData.TilesetHorizontalEdges.Add(uuid);
        TilesetHorizontalEdges.Add(edge);

        int index = TilesetHorizontalEdges.Count - 1;
        
        TilesetHorizontalEdgesDictionary.Add(uuid, index);

        return edge;
    }
    
    public TilesetEdgeVertical AddEdgeVertical(UInt64 tilesetId)
    {
        // Tileset existence
        TilesetInformation tilesetInformation = GetTileset(tilesetId);
        TilesetData tilesetData = GetTilesetData(tilesetId);
            
        if (tilesetInformation == null || tilesetData == null)
        {
            return null;
        }
        
        UInt64 uuid = ShortHash.GenerateUUID();
        
        // Keep trying until we get a unique uuid
        while (TilesetVerticalEdgesDictionary.ContainsKey(uuid))
        {
            uuid = ShortHash.GenerateUUID();
        }

        TilesetEdgeVertical edge = new TilesetEdgeVertical();

        tilesetData.TilesetVerticalEdges.Add(uuid);
        TilesetVerticalEdges.Add(edge);

        int index = TilesetVerticalEdges.Count - 1;
        
        TilesetVerticalEdgesDictionary.Add(uuid, index);

        return edge;
    }
}