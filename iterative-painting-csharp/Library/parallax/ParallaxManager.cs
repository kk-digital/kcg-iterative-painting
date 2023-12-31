using Enums;
using KEngine;
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


    public DefaultGeometrySpriteSheet DefaultGeometrySpriteSheet;
    public DefaultMonoSpaceCharactersSpriteSheet DefaultMonoSpaceCharactersSpriteSheet;
    public TilesetColorPaletteSystem TilesetColorPaletteSystem;
    
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

        DefaultGeometrySpriteSheet = new DefaultGeometrySpriteSheet();
        DefaultMonoSpaceCharactersSpriteSheet = new DefaultMonoSpaceCharactersSpriteSheet();
        TilesetColorPaletteSystem = new TilesetColorPaletteSystem();
        
        DefaultGeometrySpriteSheet.InitStage1();
        DefaultMonoSpaceCharactersSpriteSheet.InitStage1();
        TilesetColorPaletteSystem.InitStage1();
    }

    public void InitStage2()
    {
        DefaultGeometrySpriteSheet.InitStage2();
        DefaultMonoSpaceCharactersSpriteSheet.InitStage2();
        TilesetColorPaletteSystem.InitStage2();
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

        TilesetInformation tilesetInformation = new TilesetInformation
        {
            Uuid = uuid
        };

        TilesetData tilesetData = new TilesetData
        {
            TilesetUuid = uuid
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

        TilesetTileCenter tileCenter = new TilesetTileCenter 
        {
            Uuid = uuid
        };

        tilesetData.TilesetTiles.Add(uuid);
        TilesetTiles.Add(tileCenter);

        int tileIndex = TilesetTiles.Count - 1;
        
        TilesetTilesDictionary.Add(uuid, tileIndex);

        return tileCenter;
    }
    
    public TilesetTileCenter AddTileCenter(UInt64 tilesetId, TilesetTileCenter tileCenter)
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

        tileCenter.Uuid = uuid;

        tilesetData.TilesetTiles.Add(tileCenter.Uuid);
        TilesetTiles.Add(tileCenter);

        int tileIndex = TilesetTiles.Count - 1;
        
        TilesetTilesDictionary.Add(tileCenter.Uuid, tileIndex);

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

        TilesetCorner corner = new TilesetCorner
        {
            Uuid = uuid
        };

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

        TilesetEdgeHorizontal edge = new TilesetEdgeHorizontal
        {
            Uuid = uuid
        };

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

        TilesetEdgeVertical edge = new TilesetEdgeVertical
        {
            Uuid = uuid
        };

        tilesetData.TilesetVerticalEdges.Add(uuid);
        TilesetVerticalEdges.Add(edge);

        int index = TilesetVerticalEdges.Count - 1;
        
        TilesetVerticalEdgesDictionary.Add(uuid, index);

        return edge;
    }
    
    public TilesetSpriteSheetData AddSpriteSheet(UInt64 tilesetId)
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
        while (TilesetSpriteSheetDatasDictionary.ContainsKey(uuid))
        {
            uuid = ShortHash.GenerateUUID();
        }

        TilesetSpriteSheetData spriteSheet = new TilesetSpriteSheetData
        {
            Uuid = uuid
        };

        tilesetData.TilesetSpriteSheetDatas.Add(uuid);
        TilesetSpriteSheetDatas.Add(spriteSheet);

        int index = TilesetCorners.Count - 1;
        
        TilesetSpriteSheetDatasDictionary.Add(uuid, index);

        return spriteSheet;
    }

    public void Save(UInt64 tilesetUuid, string description)
    {
         TilesetData tilesetData = GetTilesetData(tilesetUuid);

         if (tilesetData == null)
         {
             return;
         }

         TilesetInformation tilesetInformation = GetTileset(tilesetData.TilesetUuid);

         if (tilesetInformation == null)
         {
             return;
         }
         
        TilesetSpriteSheetReader tilesetSpriteSheetReader = new TilesetSpriteSheetReader();
        tilesetSpriteSheetReader.InitStage1(this);
        tilesetSpriteSheetReader.InitStage2();
        
        WangTilesLoader wangTilesLoader = new WangTilesLoader();
        wangTilesLoader.LoadWangTiles(tilesetInformation.StringId, tilesetInformation.SequenceNumber);

        List<TilesetSpriteData> oldSprites = new List<TilesetSpriteData>();
        if (wangTilesLoader.LoaderTilesetsDictionary.ContainsKey(tilesetUuid))
        {
            ParallaxLoaderTileset oldTileset = wangTilesLoader.LoaderTilesetsDictionary[tilesetUuid];

            oldSprites = oldTileset.TilesetSpriteDatas;
            
            foreach(TilesetSpriteSheetData spriteSheet in  oldTileset.TilesetSpriteSheetDatas)
            {
                tilesetSpriteSheetReader.ReadSpriteSheet(tilesetUuid, spriteSheet.Uuid);
            }
        }

        const int spriteSheetMaxSpritesPerColumn = TilesetSpriteSheetBuilder.SpriteSheetWidthInSprites;
        const int spriteSheetMaxSpritesPerRow = TilesetSpriteSheetBuilder.SpriteSheetHeightInSprites;
        
        // Each time we inserted a sprite
        // We iterate over all the sprites 
        // And order them by type
        // Corners, horizontal edges, vertical edges, and then tiles
        List<TilesetSpriteData> sprites = new List<TilesetSpriteData>();
        foreach(UInt64 spriteUuid in tilesetData.TilesetSpriteDatas)
        {
            TilesetSpriteData sprite = GetSprite(spriteUuid);
            sprites.Add(sprite);
        }

        List<TilesetSpriteData> cornerSprites = new List<TilesetSpriteData>();
        List<TilesetSpriteData> edgeHorizontalSprites = new List<TilesetSpriteData>();
        List<TilesetSpriteData> edgeVerticalSprites = new List<TilesetSpriteData>();
        List<TilesetSpriteData> tileSprites = new List<TilesetSpriteData>();

        foreach (TilesetSpriteData spriteData in sprites)
        {
            switch (spriteData.Type)
            {
                case SpriteType.CornerSprite:
                {
                    cornerSprites.Add(spriteData);
                    break;
                }
                case SpriteType.EdgeHorizontalSprite:
                {
                    edgeHorizontalSprites.Add(spriteData);
                    break;
                }
                case SpriteType.EdgeVerticalSprite:
                {
                    edgeVerticalSprites.Add(spriteData);
                    break;
                }
                case SpriteType.TileCenterSprite:
                {
                    tileSprites.Add(spriteData);
                    break;
                }
            }
        }
        
        List<TilesetSpriteSheetData> spriteSheetDatas = new List<TilesetSpriteSheetData>();
        foreach (UInt64 spriteSheetUuid in tilesetData.TilesetSpriteSheetDatas)
        {
            TilesetSpriteSheetData spriteSheet = GetSpriteSheet(spriteSheetUuid);
            
            spriteSheetDatas.Add(spriteSheet);
        }
        
        // Foreach corner/edge/tile
        // find a spot on the sprite sheet png
        // The sprite sheet is a giant table of sprites
        // (column, row) starts from top left
        
        int spriteSheetIndex = 0;
        int spriteIndex = 0;
        int offsetX = 0;
        int offsetY = 0;
        
        foreach (TilesetSpriteData spriteData in cornerSprites)
        {
            int spriteRow = offsetY * Constants.SpritesheetTilesPerLine;
            int spriteColumn = offsetX * Constants.SpriteSheetWidthInTiles;

            TilesetSpriteSheetData spriteSheet = null;
            if (spriteSheetIndex >= 0 && spriteSheetIndex < spriteSheetDatas.Count)
            {
                spriteSheet = spriteSheetDatas[spriteSheetIndex];
            }

            if (spriteSheet == null)
            {
                spriteSheet = AddSpriteSheet(tilesetInformation.Uuid);
                spriteSheet.StringId = $"sprite_sheet_{spriteSheetIndex}";
                
                spriteSheetDatas.Add(spriteSheet);
            }
            
            spriteData.SpriteSheetRow = spriteRow;
            spriteData.SpriteSheetColumn = spriteColumn;
            spriteData.SpriteSheetUuid = spriteSheet.Uuid;

            spriteIndex++;
            offsetX++;
            
            // If we have reached maximum column size
            // Move to the next line
            if (offsetX >= spriteSheetMaxSpritesPerColumn)
            {
                offsetX = 0;
                offsetY++;
                
                // If we reached the maximum row count
                // Move on to the next sprite sheet png
                if (offsetY >= spriteSheetMaxSpritesPerRow)
                {
                    offsetY = 0;
                    spriteSheetIndex++;
                }
            }
        }

        
        foreach (TilesetSpriteData spriteData in edgeHorizontalSprites)
        {
            int spriteRow = offsetY * Constants.SpritesheetTilesPerLine;
            int spriteColumn = offsetX * Constants.SpriteSheetWidthInTiles;
            
            TilesetSpriteSheetData spriteSheet = null;
            if (spriteSheetIndex >= 0 && spriteSheetIndex < spriteSheetDatas.Count)
            {
                spriteSheet = spriteSheetDatas[spriteSheetIndex];
            }

            if (spriteSheet == null)
            {
                spriteSheet = AddSpriteSheet(tilesetInformation.Uuid);
                spriteSheet.StringId = $"sprite_sheet_{spriteSheetIndex}";
                
                spriteSheetDatas.Add(spriteSheet);
            }
            
            spriteData.SpriteSheetRow = spriteRow;
            spriteData.SpriteSheetColumn = spriteColumn;
            spriteData.SpriteSheetUuid = spriteSheet.Uuid;

            spriteIndex++;
            offsetX++;
            
            // If we have reached maximum column size
            // Move to the next line
            if (offsetX >= spriteSheetMaxSpritesPerColumn)
            {
                offsetX = 0;
                offsetY++;
                
                // If we reached the maximum row count
                // Move on to the next sprite sheet png
                if (offsetY >= spriteSheetMaxSpritesPerRow)
                {
                    offsetY = 0;
                    spriteSheetIndex++;
                }
            }
        }
        
        foreach (TilesetSpriteData spriteData in edgeVerticalSprites)
        {
            int spriteRow = offsetY * Constants.SpritesheetTilesPerLine;
            int spriteColumn = offsetX * Constants.SpriteSheetWidthInTiles;
            
            TilesetSpriteSheetData spriteSheet = null;
            if (spriteSheetIndex >= 0 && spriteSheetIndex < spriteSheetDatas.Count)
            {
                spriteSheet = spriteSheetDatas[spriteSheetIndex];
            }

            if (spriteSheet == null)
            {
                spriteSheet = AddSpriteSheet(tilesetInformation.Uuid);
                spriteSheet.StringId = $"sprite_sheet_{spriteSheetIndex}";
                
                spriteSheetDatas.Add(spriteSheet);
            }
            
            spriteData.SpriteSheetRow = spriteRow;
            spriteData.SpriteSheetColumn = spriteColumn;
            spriteData.Uuid = spriteSheet.Uuid;

            spriteIndex++;
            offsetX++;
            
            // If we have reached maximum column size
            // Move to the next line
            if (offsetX >= spriteSheetMaxSpritesPerColumn)
            {
                offsetX = 0;
                offsetY++;
                
                // If we reached the maximum row count
                // Move on to the next sprite sheet png
                if (offsetY >= spriteSheetMaxSpritesPerRow)
                {
                    offsetY = 0;
                    spriteSheetIndex++;
                }
            }
        }
        
        foreach (TilesetSpriteData spriteData in tileSprites)
        {
            int spriteRow = offsetY * Constants.SpritesheetTilesPerLine;
            int spriteColumn = offsetX * Constants.SpriteSheetWidthInTiles;
            
            TilesetSpriteSheetData spriteSheet = null;
            if (spriteSheetIndex >= 0 && spriteSheetIndex < spriteSheetDatas.Count)
            {
                spriteSheet = spriteSheetDatas[spriteSheetIndex];
            }

            if (spriteSheet == null)
            {
                spriteSheet = AddSpriteSheet(tilesetInformation.Uuid);
                spriteSheet.StringId = $"sprite_sheet_{spriteSheetIndex}";
                
                spriteSheetDatas.Add(spriteSheet);
            }
            
            spriteData.SpriteSheetRow = spriteRow;
            spriteData.SpriteSheetColumn = spriteColumn;
            spriteData.SpriteSheetUuid = spriteSheet.Uuid;

            spriteIndex++;
            offsetX++;
            
            // If we have reached maximum column size
            // Move to the next line
            if (offsetX >= spriteSheetMaxSpritesPerColumn)
            {
                offsetX = 0;
                offsetY++;
                
                // If we reached the maximum row count
                // Move on to the next sprite sheet png
                if (offsetY >= spriteSheetMaxSpritesPerRow)
                {
                    offsetY = 0;
                    spriteSheetIndex++;
                }
            }
        }

        for (spriteSheetIndex = 0; spriteSheetIndex < TilesetSpriteSheetDatas.Count; spriteSheetIndex++)
        {
            TilesetSpriteSheetData spriteSheet = TilesetSpriteSheetDatas[spriteSheetIndex];
            
            string pngfilePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber, tilesetInformation.StringId,
                DataType.SpriteSheetData, spriteSheet.StringId, KcgData.FileType.Png);

            spriteSheet.Filepath = pngfilePath;
            spriteSheet.Width = spriteSheetMaxSpritesPerColumn * Constants.SpriteSheetWidthInTiles;
            spriteSheet.Height = spriteSheetMaxSpritesPerRow * Constants.SpritesheetTilesPerLine;
        }


        
        



        TilesetManifest tilesetManifest = new TilesetManifest();
        tilesetManifest.Files = new List<TilesetManifestItem>();

        Int64 maxSequenceNumber = 0;
        
        string tilesetIdString = tilesetInformation.StringId;
        string path = $"{Constants.CacheFolderName}/{Constants.TilesetsFolderName}/{tilesetIdString}";
        if (!FileUtils.DirectoryExistsFull(path))
        {
            FileUtils.CreateDirectoryFull(path);
        }
        
        List <TilesetManifest> tilesetVersions = WangTilesUtility.ListVersions(tilesetIdString);
        foreach (var tileset in tilesetVersions)
        {
            maxSequenceNumber = Math.Max(tileset.SequenceNumber, maxSequenceNumber);
        }
        
        // Allways increment the version anytime we save
        tilesetInformation.SequenceNumber = maxSequenceNumber + 1;

        tilesetManifest.SequenceNumber = tilesetInformation.SequenceNumber;
        tilesetManifest.Description = description;
        
        for(int cornerIndex = 0; cornerIndex < tilesetData.TilesetCorners.Count; cornerIndex++)
        {
            UInt64 cornerUuid = tilesetData.TilesetCorners[cornerIndex];
            
            TilesetCorner corner = GetCorner(cornerUuid);
            string filePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber, tilesetInformation.StringId, DataType.Corner,
                corner.StringId, KcgData.FileType.Json);
            
            // Encode to json
            // Make sure file directory exists
            string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
            if (!FileUtils.DirectoryExistsFull(directory))
            {
                FileUtils.CreateDirectoryFull(directory);
            }
            TilesetDataEncoder.Encode(filePath, corner);
        
            // Check if file was created successfully
            if (!File.Exists(filePath))
            {
                // Panic, error, print some logs
                KLog.LogDebug($"creation of the file {filePath} was not successful");
                return;
            }
        
            // Compute file size
            FileInfo fileInfo = new FileInfo(filePath);
            long fileSizeInBytes = fileInfo.Length;
        
            // Compute file hash
            string hash256 = WangTilesUtility.ComputeSHA256(filePath);

            // Update the manifest
            TilesetManifestItem cornerFile = new TilesetManifestItem
            {
                Filepath = filePath,
                DataType = DataType.Corner,
                FileSize = (int)fileSizeInBytes,
                HashSha256 = hash256,
                SequenceNumber = corner.SequenceNumber
            };
            
            // Add the new corner to the tileset manifest 
            tilesetManifest.Files.Add(cornerFile);
        }
        
        foreach (var edgeVerticalUuid in tilesetData.TilesetVerticalEdges)
        {
            TilesetEdgeVertical edge = GetEdgeVertical(edgeVerticalUuid);
            
            string filePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber,
                tilesetInformation.StringId, DataType.VerticalEdge, edge.StringId, KcgData.FileType.Json);
            // Encode to json
            // Make sure file directory exists
            string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
            if (!FileUtils.DirectoryExistsFull(directory))
            {
                FileUtils.CreateDirectoryFull(directory);
            }
            TilesetDataEncoder.Encode(filePath, edge);
        
            // Check if file was created successfully
            if (!File.Exists(filePath))
            {
                // Panic, error, print some logs
                KLog.LogDebug($"creation of the file {filePath} was not successful");
                return;
            }
        
            // Compute file size
            FileInfo fileInfo = new FileInfo(filePath);
            long fileSizeInBytes = fileInfo.Length;
        
            // Compute file hash
            string hash256 = WangTilesUtility.ComputeSHA256(filePath);

            // Update the manifest
            TilesetManifestItem edgeVerticalFile = new TilesetManifestItem
            {
                Filepath = filePath,
                DataType = DataType.VerticalEdge,
                FileSize = (int)fileSizeInBytes,
                HashSha256 = hash256,
                SequenceNumber = edge.SequenceNumber
            };
            
            // Add the new vertical edge to the tileset manifest 
            tilesetManifest.Files.Add(edgeVerticalFile);
        }
        
        foreach (var edgeHorizontalUuid in tilesetData.TilesetHorizontalEdges)
        {
            TilesetEdgeHorizontal edge = GetEdgeHorizontal(edgeHorizontalUuid);
            
            string filePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber, 
                tilesetInformation.StringId, DataType.HorizontalEdge, edge.StringId, KcgData.FileType.Json);

            // Encode to json
            // Make sure file directory exists
            string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
            if (!FileUtils.DirectoryExistsFull(directory))
            {
                FileUtils.CreateDirectoryFull(directory);
            }
            TilesetDataEncoder.Encode(filePath, edge);
        
            // Check if file was created successfully
            if (!File.Exists(filePath))
            {
                // Panic, error, print some logs
                KLog.LogDebug($"creation of the file {filePath} was not successful");
                return;
            }
        
            // Compute file size
            FileInfo fileInfo = new FileInfo(filePath);
            long fileSizeInBytes = fileInfo.Length;
        
            // Compute file hash
            string hash256 = WangTilesUtility.ComputeSHA256(filePath);

            // Update the manifest
            TilesetManifestItem edgeHorizontalFile = new TilesetManifestItem
            {
                Filepath = filePath,
                DataType = DataType.HorizontalEdge,
                FileSize = (int)fileSizeInBytes,
                HashSha256 = hash256,
                SequenceNumber = edge.SequenceNumber
            };
            
            // Add the new vertical edge to the tileset manifest 
            tilesetManifest.Files.Add(edgeHorizontalFile);
        }
        
        foreach (var tileUuid in tilesetData.TilesetTiles)
        {
            TilesetTileCenter tileCenter = GetTileCenter(tileUuid);
            
            string filePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber, tilesetInformation.StringId,
                DataType.Tile, tileCenter.StringId, KcgData.FileType.Json);
            // Encode to json
            // Make sure file directory exists
            string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
            if (!FileUtils.DirectoryExistsFull(directory))
            {
                FileUtils.CreateDirectoryFull(directory);
            }
            TilesetDataEncoder.Encode(filePath, tileCenter);
        
            // Check if file was created successfully
            if (!File.Exists(filePath))
            {
                // Panic, error, print some logs
                KLog.LogDebug($"creation of the file {filePath} was not successful");
                return;
            }
        
            // Compute file size
            FileInfo fileInfo = new FileInfo(filePath);
            long fileSizeInBytes = fileInfo.Length;
        
            // Compute file hash
            string hash256 = WangTilesUtility.ComputeSHA256(filePath);

            // Update the manifest
            TilesetManifestItem tileFile = new TilesetManifestItem
            {
                Filepath = filePath,
                DataType = DataType.Tile,
                FileSize = (int)fileSizeInBytes,
                HashSha256 = hash256,
                SequenceNumber = tileCenter.SequenceNumber
            };
            
            // Add the new vertical edge to the tileset manifest 
            tilesetManifest.Files.Add(tileFile);
        }

        foreach (var spriteSheetUuid in tilesetData.TilesetSpriteSheetDatas)
        {
            TilesetSpriteSheetData spriteSheet = GetSpriteSheet(spriteSheetUuid);
            
            string pngfilePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber, tilesetInformation.StringId,
                DataType.SpriteSheetData, spriteSheet.StringId, KcgData.FileType.Png);
            string filePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber, tilesetInformation.StringId,
                DataType.SpriteSheetData, spriteSheet.StringId, KcgData.FileType.Json);
            
            // Encode to json
            // Make sure file directory exists
            string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
            if (!FileUtils.DirectoryExistsFull(directory))
            {
                FileUtils.CreateDirectoryFull(directory);
            }
            
            sprites = new List<TilesetSpriteData>();
            foreach(UInt64 spriteUuid in tilesetData.TilesetSpriteDatas)
            {
                TilesetSpriteData sprite = GetSprite(spriteUuid);
                
                sprites.Add(sprite);
            }
            
            TilesetSpriteSheetBuilder.MakePng(tilesetInformation.Uuid, pngfilePath, oldSprites, sprites,
                DefaultGeometrySpriteSheet, DefaultMonoSpaceCharactersSpriteSheet,
                tilesetSpriteSheetReader,
                this, TilesetColorPaletteSystem);
            
            spriteSheet.Filepath = pngfilePath;
            TilesetDataEncoder.Encode(filePath, spriteSheet);

            // Check if file was created successfully
            if (!File.Exists(filePath))
            {
                // Panic, error, print some logs
                KLog.LogDebug($"creation of the file {filePath} was not successful");
                return;
            }

            // Compute file size
            FileInfo fileInfo = new FileInfo(filePath);
            long fileSizeInBytes = fileInfo.Length;

            // Compute file hash
            string hash256 = WangTilesUtility.ComputeSHA256(filePath);

            // Update the manifest
            TilesetManifestItem spriteSheetfile = new TilesetManifestItem
            {
                Filepath = filePath,
                DataType = DataType.SpriteSheetData,
                FileSize = (int)fileSizeInBytes,
                HashSha256 = hash256,
                SequenceNumber = 0
            };

            // Add the new vertical edge to the tileset manifest 
            tilesetManifest.Files.Add(spriteSheetfile);
        }

        foreach (var spriteUuid in tilesetData.TilesetSpriteDatas)
        {
            TilesetSpriteData sprite = GetSprite(spriteUuid);
            
            string filePath = WangTilesUtility.GetDataPath(tilesetInformation.SequenceNumber,
                tilesetInformation.StringId, DataType.SpriteData, sprite.StringId, KcgData.FileType.Json);
            // Encode to json
            // Make sure file directory exists
            string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
            if (!FileUtils.DirectoryExistsFull(directory))
            {
                FileUtils.CreateDirectoryFull(directory);
            }
            TilesetDataEncoder.Encode(filePath, sprite);
        
            // Check if file was created successfully
            if (!File.Exists(filePath))
            {
                // Panic, error, print some logs
                KLog.LogDebug($"creation of the file {filePath} was not successful");
                return;
            }
        
            // Compute file size
            FileInfo fileInfo = new FileInfo(filePath);
            long fileSizeInBytes = fileInfo.Length;
        
            // Compute file hash
            string hash256 = WangTilesUtility.ComputeSHA256(filePath);

            // Update the manifest
            TilesetManifestItem spriteFile = new TilesetManifestItem
            {
                Filepath = filePath,
                DataType = DataType.SpriteData,
                FileSize = (int)fileSizeInBytes,
                HashSha256 = hash256,
                SequenceNumber = sprite.SequenceNumber
            };
            
            // Add the new vertical edge to the tileset manifest 
            tilesetManifest.Files.Add(spriteFile);
        }

        {
            TilesetInformation tileset = new TilesetInformation
            {
                Uuid = tilesetUuid,
                StringId = tilesetInformation.StringId,
                SequenceNumber = 0,
                Description = ""
            };

            string filePath = WangTilesUtility.GetTilesetPath(tilesetInformation.SequenceNumber, tilesetInformation.StringId);

            string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
            if (!FileUtils.DirectoryExistsFull(directory))
            {
                FileUtils.CreateDirectoryFull(directory);
            }

            TilesetDataEncoder.Encode(filePath, tileset);
        }

        // Save the tileset manifest
        SaveTilesetManifest(tilesetUuid, tilesetManifest);
    }
    
    public void SaveTilesetManifest(UInt64 tilesetUuid, TilesetManifest tilesetManifest)
    {
        TilesetInformation tilesetInformation = GetTileset(tilesetUuid);
        TilesetData tilesetData = GetTilesetData(tilesetUuid);

        if (tilesetData == null || tilesetInformation == null)
        {
            // fail
            return;
        }
        // Get today's date
        DateTime today = DateTime.Now;

        // Format the date as yyyy-MM-dd HH:mm:ss
        string todayString = today.ToString("yyyy-MM-dd HH:mm:ss");

        tilesetManifest.CreationDate = todayString;
        
        string filePath = WangTilesUtility.GetManifestPath(tilesetInformation.SequenceNumber, tilesetInformation.StringId);
        // Make sure file directory exists
        string directory = WangTilesUtility.GetDirectoryFromFilepath(filePath);
        if (!FileUtils.DirectoryExistsFull(directory))
        {
            FileUtils.CreateDirectoryFull(directory);
        }
        TilesetDataEncoder.Encode(filePath, tilesetManifest);   
    }
}