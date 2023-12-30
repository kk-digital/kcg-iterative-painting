using System.Drawing;
using BigGustave;
using Enums;
using KEngine;

namespace Parallax;

public class TilesetSpriteSheetBuilder
{
    public const int SpriteSheetWidthInSprites = 8;
    public const int SpriteSheetHeightInSprites = 8;
    
    public UInt64 TilesetUuid;
    public string Path;
    
    // Corner size in pixels
    // Edge size in pixels
    public int Size;
    
    // In pixels
    // Usually 32x32
    public int TileSize;
    
    // Manages the default geometry sprites
    public DefaultGeometrySpriteSheet DefaultGeometrySpriteSheet;
    public DefaultMonoSpaceCharactersSpriteSheet DefaultMonoSpaceCharactersSpriteSheet;
    public TilesetSpriteSheetReader TilesetSpriteSheetReader;

    public Dictionary<UInt64, TilesetSpriteData> OldSprites;
    
    // Png builder that is used to create the png file
    public PngBuilder PngBuilder;
    
    // spriteSheet  size
    public int WidthInTiles;
    public int HeightInTiles;

    public int WidthInPixels;
    public int HeightInPixels;
    
    
    // ------ Dependencies -------------
    public ParallaxManager ParallaxManager;
    public TilesetColorPaletteSystem TilesetColorPaletteSystem;
    
    public static void MakePng(UInt64 tilesetUuid, string path,
        List<TilesetSpriteData> oldSprites,
        List<TilesetSpriteData> sprites,
        DefaultGeometrySpriteSheet defaultGeometrySpriteSheet,
        DefaultMonoSpaceCharactersSpriteSheet defaultMonoSpaceCharactersSpriteSheet,
        TilesetSpriteSheetReader tilesetSpriteSheetReader,
        ParallaxManager parallaxManager,
        TilesetColorPaletteSystem tilesetColorPaletteSystem)
    {
        int spriteSheetWidthInTiles = Constants.SpriteSheetWidthInTiles * SpriteSheetWidthInSprites;
        int spriteSheetHeightInTiles = Constants.SpritesheetTilesPerLine * SpriteSheetHeightInSprites;
        
        TilesetSpriteSheetBuilder builder = new TilesetSpriteSheetBuilder(
            defaultGeometrySpriteSheet, defaultMonoSpaceCharactersSpriteSheet,
            oldSprites, parallaxManager, tilesetColorPaletteSystem,
            tilesetUuid, tilesetSpriteSheetReader, 
            path, 
            spriteSheetWidthInTiles, spriteSheetHeightInTiles);
        
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
       
        for (int spriteIndex = 0; spriteIndex < sprites.Count; spriteIndex++)
        {
            TilesetSpriteData spriteData = sprites[spriteIndex];

            switch (spriteData.Type)
            {
                case SpriteType.CornerSprite:
                {
                    builder.BlitCornerSprite(spriteData);
                    break;
                }
                case SpriteType.EdgeVerticalSprite:
                {
                    builder.BlitEdgeVerticalSprite(spriteData);
                    break;
                }
                case SpriteType.EdgeHorizontalSprite:
                {
                    builder.BlitEdgeHorizontalSprite(spriteData);
                    break;
                }
                case SpriteType.TileCenterSprite:
                {
                    builder.BlitTileSprite(spriteData);
                    break;
                }
            }
            spriteData.IsDrawnInSpriteSheet = true;
        }

        builder.SavePng();
    }
    
    
    public TilesetSpriteSheetBuilder(DefaultGeometrySpriteSheet defaultGeometrySpriteSheet,
        DefaultMonoSpaceCharactersSpriteSheet defaultMonoSpaceCharactersSpriteSheet, 
        List<TilesetSpriteData> spriteList,
        ParallaxManager parallaxManager,
        TilesetColorPaletteSystem tilesetColorPaletteSystem,
        UInt64 tilesetUuid, TilesetSpriteSheetReader spriteSheetReader,
        string path, int widthInTiles, int heightInTiles, int size = 2, int tileSize = 32)
    {
        ParallaxManager = parallaxManager;
        TilesetColorPaletteSystem = tilesetColorPaletteSystem;
        TilesetUuid = tilesetUuid;
        Path = path;
        Size = size;
        TileSize = tileSize;

        TilesetSpriteSheetReader = spriteSheetReader;
        
        DefaultGeometrySpriteSheet = defaultGeometrySpriteSheet;
        DefaultMonoSpaceCharactersSpriteSheet = defaultMonoSpaceCharactersSpriteSheet;
        
        WidthInTiles = widthInTiles;
        HeightInTiles = heightInTiles;

        WidthInPixels = WidthInTiles * tileSize;
        HeightInPixels = HeightInTiles * tileSize;
        
        

        OldSprites = new Dictionary<UInt64, TilesetSpriteData>();
        foreach (TilesetSpriteData sprite in spriteList)
        {
            OldSprites.Add(sprite.Uuid, sprite);
        }
        
        PngBuilder = PngBuilder.Create(WidthInPixels, HeightInPixels, true);
    }

    public void SavePng()
    {
        byte[] bytes = PngBuilder.Save();
        
        FileUtils.WriteAllBytesFull(Path, bytes);
    }

    public void BlitLine(int positionX, int positionY, Color color)
    {
        int lineHeight = 2;
        int lineOffsetX = positionX + 0;
        int lineOffsetY = positionY + 16 - lineHeight / 2;

        int lineWidth = Constants.TileSize * 7;
        
        // blit white pixels
        for (int y = lineOffsetY; y < lineOffsetY + lineHeight; y++)
        {
            for (int x = lineOffsetX; x < lineOffsetX + lineWidth; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
    }

    public void BlitCornerSprite(TilesetSpriteData spriteData)
    {
        TilesetCorner corner = ParallaxManager.GetCorner(spriteData.DataUuid);

        byte[] texturePixels = null;
        if (OldSprites.ContainsKey(spriteData.Uuid))
        {
            TilesetSpriteData oldSprite = OldSprites[spriteData.Uuid];
            
            texturePixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
        }
        
        int row = spriteData.SpriteSheetRow;
        int column = spriteData.SpriteSheetColumn;
        
        // blit the corner stringId (name)
        int cornerNameOffsetX = column * Constants.TileSize + 4;
        int cornerNameOffsetY = (row + 1) * Constants.TileSize + Constants.TileSize / 2;
        BlitString(cornerNameOffsetX, cornerNameOffsetY, corner.StringId);
        
        // The “view” in geometry view (without edge coloring)
        int geometryViewRow = row + 0;
        int geometryViewColumn = column + 0;
        // The “view” in geometry view,(with edge coloring)
        int geometryViewColorRow = row + 0;
        int geometryViewColorColumn = column + 1;
        // The full texture of tile, with edge coloring overlaid
        int fullTextureViewColorRow = row + 0;
        int fullTextureViewColorColumn = column + 2;
        // The full “texture” of tile/corner/edge
        int fullTextureViewRow = row + 0;
        int fullTextureViewColumn = column + 3;

        BlitTileOutline(geometryViewRow, geometryViewColumn, 1);
        BlitCornerColor(geometryViewRow, geometryViewColumn, corner.ColorId);
        BlitTileOutline(geometryViewColorRow, geometryViewColorColumn, 1);
        BlitCornerColor(geometryViewColorRow, geometryViewColorColumn, corner.ColorId);
        BlitTileOutline(fullTextureViewColorRow, fullTextureViewColorColumn, 1);
        BlitCorner(fullTextureViewColorRow, fullTextureViewColorColumn, Color.Pink);
        BlitTileOutline64(fullTextureViewRow, fullTextureViewColumn, 1);
        BlitCornerTexture(fullTextureViewRow, fullTextureViewColumn, texturePixels, Color.Pink);
    }
    
      public void BlitEdgeVerticalSprite(TilesetSpriteData spriteData)
    {
        TilesetEdgeVertical verticalEdge = 
            ParallaxManager.GetEdgeVertical(spriteData.DataUuid);
        
        byte[] texturePixels = null;
        if (OldSprites.ContainsKey(spriteData.Uuid))
        {
            TilesetSpriteData oldSprite = OldSprites[spriteData.Uuid];
            
            texturePixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
        }
        
        int row = spriteData.SpriteSheetRow;
        int column = spriteData.SpriteSheetColumn;
        
        // blit the edge stringId (name)
        int edgeNameOffsetX = column * Constants.TileSize + 4;
        int edgeNameOffsetY = (row + 1) * Constants.TileSize + Constants.TileSize / 2;
        BlitString(edgeNameOffsetX, edgeNameOffsetY, verticalEdge.StringId);
        
        // The “view” in geometry view (without edge coloring)
        int geometryViewRow = row + 0;
        int geometryViewColumn = column + 0;
        // The “view” in geometry view,(with edge coloring)
        int geometryViewColorRow = row + 0;
        int geometryViewColorColumn = column + 1;
        // The full texture of tile, with edge coloring overlaid
        int fullTextureViewColorRow = row + 0;
        int fullTextureViewColorColumn = column + 2;
        // The full “texture” of tile/corner/edge
        int fullTextureViewRow = row + 0;
        int fullTextureViewColumn = column + 3;
        
        BlitTileOutline(geometryViewColorRow, geometryViewColorColumn, 1);
        BlitVerticalEdgeColor(geometryViewColorRow, geometryViewColorColumn, verticalEdge.ColorId);
        BlitTileOutline(geometryViewRow, geometryViewColumn, 1);
        BlitVerticalEdgeColor(geometryViewRow, geometryViewColumn, verticalEdge.ColorId);
        BlitTileOutline(fullTextureViewColorRow, fullTextureViewColorColumn, 1);
        BlitVerticalEdge(fullTextureViewColorRow, fullTextureViewColorColumn, Color.Pink);
        BlitTileOutline64(fullTextureViewRow, fullTextureViewColumn, 1);
        BlitVerticalEdgeTexture(fullTextureViewRow, fullTextureViewColumn, texturePixels, Color.Pink);
    }
    
    public void BlitEdgeHorizontalSprite(TilesetSpriteData spriteData)
    {
        TilesetEdgeHorizontal horizontalEdge = 
            ParallaxManager.GetEdgeHorizontal(spriteData.DataUuid);
        
        byte[] texturePixels = null;
        if (OldSprites.ContainsKey(spriteData.Uuid))
        {
            TilesetSpriteData oldSprite = OldSprites[spriteData.Uuid];
            
            texturePixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
        }
        
        int row = spriteData.SpriteSheetRow;
        int column = spriteData.SpriteSheetColumn;
        
        // blit the edge stringId (name)
        int edgeNameOffsetX = column * Constants.TileSize + 4;
        int edgeNameOffsetY = (row + 1) * Constants.TileSize + Constants.TileSize / 2;
        BlitString(edgeNameOffsetX, edgeNameOffsetY, horizontalEdge.StringId);
        
        // The “view” in geometry view (without edge coloring)
        int geometryViewRow = row + 0;
        int geometryViewColumn = column + 0;
        // The “view” in geometry view,(with edge coloring)
        int geometryViewColorRow = row + 0;
        int geometryViewColorColumn = column + 1;
        // The full texture of tile, with edge coloring overlaid
        int fullTextureViewColorRow = row + 0;
        int fullTextureViewColorColumn = column + 2;
        // The full “texture” of tile/corner/edge
        int fullTextureViewRow = row + 0;
        int fullTextureViewColumn = column + 3;

        BlitTileOutline(geometryViewRow, geometryViewColumn, 1);
        BlitHorizontalEdgeColor(geometryViewRow, geometryViewColumn, horizontalEdge.ColorId);
        BlitTileOutline(geometryViewColorRow, geometryViewColorColumn, 1);
        BlitHorizontalEdgeColor(geometryViewColorRow, geometryViewColorColumn, horizontalEdge.ColorId);
        BlitTileOutline(fullTextureViewColorRow, fullTextureViewColorColumn, 1);
        BlitHorizontalEdge(fullTextureViewColorRow, fullTextureViewColorColumn, Color.Pink);
        BlitTileOutline64(fullTextureViewRow, fullTextureViewColumn, 1);
        BlitHorizontalEdgeTexture(fullTextureViewRow, fullTextureViewColumn, texturePixels, Color.Pink);
    }

    public void BlitTileOutline(int row, int column, int size)
    {
        Color color = Color.Gray;
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // blit white pixel outline
        for (int y = tileOffsetY; y < tileOffsetY + size; y++)
        {
            for (int x = tileOffsetX; x <= tileOffsetX + TileSize - 1; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
        for (int y = tileOffsetY + TileSize - size; y <= tileOffsetY + TileSize - 1; y++)
        {
            for (int x = tileOffsetX; x <= tileOffsetX + TileSize - 1; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
        for (int y = tileOffsetY; y <= tileOffsetY + TileSize - 1; y++)
        {
            for (int x = tileOffsetX; x < tileOffsetX + size; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
        for (int y = tileOffsetY; y <= tileOffsetY + TileSize - 1; y++)
        {
            for (int x = tileOffsetX + TileSize - size; x < tileOffsetX + TileSize; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
    }
    
    public void BlitTileOutline64(int row, int column, int size)
    {
        Color color = Color.Gray;
        
        int outlineSize = TileSize * 2;
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // blit white pixel outline
        for (int y = tileOffsetY; y < tileOffsetY + size; y++)
        {
            for (int x = tileOffsetX; x <= tileOffsetX + outlineSize - 1; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
        for (int y = tileOffsetY + outlineSize - size; y <= tileOffsetY + outlineSize - 1; y++)
        {
            for (int x = tileOffsetX; x <= tileOffsetX + outlineSize - 1; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
        for (int y = tileOffsetY; y <= tileOffsetY + outlineSize - 1; y++)
        {
            for (int x = tileOffsetX; x < tileOffsetX + size; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
        for (int y = tileOffsetY; y <= tileOffsetY + outlineSize - 1; y++)
        {
            for (int x = tileOffsetX + outlineSize - size; x < tileOffsetX + outlineSize; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
    }
    
    public void BlitTileSprite(TilesetSpriteData spriteData)
    {
        TilesetTileCenter tileCenter = ParallaxManager.GetTileCenter(spriteData.DataUuid);
        
        TilesetCorner cornerNorthWest = ParallaxManager.GetCorner(tileCenter.CornerNorthWest);
        TilesetSpriteData cornerNorthWestSprite = null;
        if (cornerNorthWest != null)
        {
            cornerNorthWestSprite =
                ParallaxManager.GetSprite(cornerNorthWest.SpriteUuid);
        }

        TilesetCorner cornerNorthEast = ParallaxManager.GetCorner(tileCenter.CornerNorthEast);
        TilesetSpriteData cornerNorthEastSprite = null;
        if (cornerNorthEast != null)
        {
            cornerNorthEastSprite = ParallaxManager.GetSprite(cornerNorthEast.SpriteUuid);
        }

        TilesetCorner cornerSouthWest = ParallaxManager.GetCorner(tileCenter.CornerSouthWest);
        TilesetSpriteData cornerSouthWestSprite = null;
        if (cornerSouthWest != null)
        {
            cornerSouthWestSprite = ParallaxManager.GetSprite(cornerSouthWest.SpriteUuid);
        }

        TilesetCorner cornerSouthEast = ParallaxManager.GetCorner(tileCenter.CornerSouthEast);
        TilesetSpriteData cornerSouthEastSprite = null;
        if (cornerSouthEast != null)
        {
            cornerSouthEastSprite = ParallaxManager.GetSprite(cornerSouthEast.SpriteUuid);
        }

        TilesetEdgeHorizontal edgeHorizontalNorth = 
            ParallaxManager.GetEdgeHorizontal(tileCenter.EdgeHorizontalNorth);
        TilesetSpriteData edgeHorizontalNorthSprite = null;
        if (edgeHorizontalNorth != null)
        {
            edgeHorizontalNorthSprite = ParallaxManager.GetSprite(edgeHorizontalNorth.SpriteUuid);
        }

        TilesetEdgeHorizontal edgeHorizontalSouth =
            ParallaxManager.GetEdgeHorizontal(tileCenter.EdgeHorizontalSouth);
        TilesetSpriteData edgeHorizontalSouthSprite = null;
        if (edgeHorizontalSouth != null)
        {
            edgeHorizontalSouthSprite = ParallaxManager.GetSprite(edgeHorizontalSouth.SpriteUuid);
        }



        TilesetEdgeVertical edgeVerticalWest =
            ParallaxManager.GetEdgeVertical(tileCenter.EdgeVerticalWest);
        TilesetSpriteData edgeVerticalWestSprite = null;
        if (edgeVerticalWest != null)
        {
            edgeVerticalWestSprite = ParallaxManager.GetSprite(edgeVerticalWest.SpriteUuid);
        }

        TilesetEdgeVertical edgeVerticalEast =
            ParallaxManager.GetEdgeVertical(tileCenter.EdgeVerticalEast);
        TilesetSpriteData edgeVerticalEastSprite = null;
        if (edgeVerticalEast != null)
        {
            edgeVerticalEastSprite =
                ParallaxManager.GetSprite(edgeVerticalEast.SpriteUuid);
        }
        
        Color cornerNorthWestColor = Color.White;
        if (cornerNorthWest != null)
        {
            cornerNorthWestColor = TilesetColorPaletteSystem.GetCornerColor(cornerNorthWest.ColorId);
        }

        Color cornerNorthEastColor = Color.White;
        if (cornerNorthEast != null)
        {
            cornerNorthEastColor = TilesetColorPaletteSystem.GetCornerColor(cornerNorthEast.ColorId);
        }

        Color cornerSouthWestColor = Color.White;
        if (cornerSouthWest != null)
        {
            cornerSouthWestColor = TilesetColorPaletteSystem.GetCornerColor(cornerSouthWest.ColorId);
        }

        Color cornerSouthEastColor = Color.White;
        if (cornerSouthEast != null)
        {
            cornerSouthEastColor = TilesetColorPaletteSystem.GetCornerColor(cornerSouthEast.ColorId);
        }
        
        Color edgeVerticalEastColor = Color.White;
        if (edgeVerticalEast != null)
        {
            edgeVerticalEastColor =
                TilesetColorPaletteSystem.GetEdgeVerticalColor(edgeVerticalEast.ColorId);
        }

        Color edgeVerticalWestColor = Color.White;
        if (edgeVerticalWest != null)
        {
            edgeVerticalWestColor =
                TilesetColorPaletteSystem.GetEdgeVerticalColor(edgeVerticalWest.ColorId);
        }

        Color edgeHorizontalNorthColor = Color.White;
        if (edgeHorizontalNorth != null)
        {
            edgeHorizontalNorthColor =
                TilesetColorPaletteSystem.GetEdgeHorizontalColor(edgeHorizontalNorth.ColorId);
        }

        Color edgeHorizontalSouthColor = Color.White;
        if (edgeHorizontalSouth != null)
        {
            edgeHorizontalSouthColor =
                TilesetColorPaletteSystem.GetEdgeHorizontalColor(edgeHorizontalSouth.ColorId);
        }

        byte[] cornerNorthWestPixels = null;
        if (cornerNorthWestSprite != null)
        {
            if (OldSprites.ContainsKey(cornerNorthWestSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[cornerNorthWestSprite.Uuid];
            
                cornerNorthWestPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }
        
        

        byte[] cornerNorthEastPixels = null;
        if (cornerNorthEastSprite != null)
        {
            if (OldSprites.ContainsKey(cornerNorthEastSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[cornerNorthEastSprite.Uuid];
            
                cornerNorthEastPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }

        byte[] cornerSouthEastPixels = null;
        if (cornerSouthEastSprite != null)
        {
            if (OldSprites.ContainsKey(cornerSouthEastSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[cornerSouthEastSprite.Uuid];
            
                cornerSouthEastPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }

        byte[] cornerSouthWestPixels = null;
        if (cornerSouthWestSprite != null)
        {
            if (OldSprites.ContainsKey(cornerSouthWestSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[cornerSouthWestSprite.Uuid];
            
                cornerSouthWestPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }

        byte[] edgeVerticalEastPixels = null;
        if (edgeVerticalEastSprite != null)
        {
            if (OldSprites.ContainsKey(edgeVerticalEastSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[edgeVerticalEastSprite.Uuid];
            
                edgeVerticalEastPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }

        byte[] edgeVerticalWestPixels = null;
        if (edgeVerticalWestSprite != null)
        {
            if (OldSprites.ContainsKey(edgeVerticalWestSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[edgeVerticalWestSprite.Uuid];
            
                edgeVerticalWestPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }

        byte[] edgeHorizontalNorthPixels = null;
        if (edgeHorizontalNorthSprite != null)
        {
            if (OldSprites.ContainsKey(edgeHorizontalNorthSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[edgeHorizontalNorthSprite.Uuid];
            
                edgeHorizontalNorthPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }

        byte[] edgeHorizontalSouthPixels = null;
        if (edgeHorizontalSouthSprite != null)
        {
            if (OldSprites.ContainsKey(edgeHorizontalSouthSprite.Uuid))
            {
                TilesetSpriteData oldSprite = OldSprites[edgeHorizontalSouthSprite.Uuid];
            
                edgeHorizontalSouthPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
            }
        }

        byte[] tileCenterPixels = null;
        
        if (OldSprites.ContainsKey(spriteData.Uuid))
        {
            TilesetSpriteData oldSprite = OldSprites[spriteData.Uuid];
            
            tileCenterPixels = TilesetSpriteSheetReader.ReadSpritePixels(oldSprite);
        }
            
        int row = spriteData.SpriteSheetRow;
        int column = spriteData.SpriteSheetColumn;
        
        // blit the edge stringId (name)
        int tileNameOffsetX = column * Constants.TileSize + 4;
        int tileNameOffsetY = (row + 1) * Constants.TileSize + Constants.TileSize / 2;
        BlitString(tileNameOffsetX, tileNameOffsetY, tileCenter.StringId);
        
        // The “view” in geometry view (without edge coloring)
        int geometryViewRow = row + 0;
        int geometryViewColumn = column + 0;
        // The “view” in geometry view,(with edge coloring)
        int geometryViewColorRow = row + 0;
        int geometryViewColorColumn = column + 1;
        // The full texture of tile, with edge coloring overlaid
        int fullTextureViewColorRow = row + 0;
        int fullTextureViewColorColumn = column + 2;
        // The full “texture” of tile/corner/edge
        int fullTextureViewRow = row + 0;
        int fullTextureViewColumn = column + 3;

        BlitTileOutline(geometryViewRow, geometryViewColumn, 1);
        BlitTileCenterGeometry(geometryViewRow, geometryViewColumn, tileCenter.Geometry);
        BlitTileOutline(geometryViewColorRow, geometryViewColorColumn, 1);
        BlitTileCenterGeometryColor(geometryViewColorRow, geometryViewColorColumn, tileCenter.Geometry,
            cornerNorthWestColor, cornerNorthEastColor,
            cornerSouthEastColor, cornerSouthWestColor,
            edgeVerticalEastColor, edgeVerticalWestColor, 
            edgeHorizontalNorthColor, edgeHorizontalSouthColor);
        BlitTileOutline(fullTextureViewColorRow, fullTextureViewColorColumn, 1);
        BlitTileBorderPreview(fullTextureViewColorRow, fullTextureViewColorColumn, 
            tileCenterPixels,
            cornerNorthWestPixels, cornerNorthEastPixels, cornerSouthEastPixels, cornerSouthWestPixels,
            edgeVerticalEastPixels, edgeVerticalWestPixels,
            edgeHorizontalNorthPixels, edgeHorizontalSouthPixels,
            Color.Pink);
        BlitTileOutline64(fullTextureViewRow, fullTextureViewColumn, 1);
        BlitTilePreview(fullTextureViewRow, fullTextureViewColumn, 
            tileCenterPixels,
            cornerNorthWestPixels, cornerNorthEastPixels, cornerSouthEastPixels, cornerSouthWestPixels,
            edgeVerticalEastPixels, edgeVerticalWestPixels,
            edgeHorizontalNorthPixels, edgeHorizontalSouthPixels,
            tileCenter.Geometry,
            Color.Pink);
    }
    
    public void BlitCornerTexture(int row, int column, byte[] pixels, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize;
        int tileCenterY = tileOffsetY + TileSize;
        
        // Corner texture
        int cornerSize = Size * 2;
        int cornerOffsetX = tileCenterX - Size;
        int cornerOffsetY = tileCenterY - Size;
        
        // blit white pixels
        for (int y = 0; y < cornerSize; y++)
        {
            for (int x = 0; x < cornerSize; x++)
            {
                int positionInPngX = x + cornerOffsetX;
                int positionInPngY = y + cornerOffsetY;

                int positionInByteArrayX = x + TileSize / 2 - Size;
                int positionInByteArrayY = y + TileSize / 2 - Size;

                int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                // If we have a texture ready in the sprite sheet
                // We just copy that texture over to the next sprite sheet
                if (pixels != null)
                {
                    byte r = pixels[indexInArray + 0];
                    byte g = pixels[indexInArray + 1];
                    byte b = pixels[indexInArray + 2];
                    byte a = pixels[indexInArray + 3];

                    Pixel pixel = new Pixel(r, g, b, a, false);
                    
                    PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                }
                else
                {
                    PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                }
            }
        }
    }
    
    public void BlitCorner(int row, int column, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int cornerSize = Size * 2;
        int cornerOffsetX = tileCenterX - Size;
        int cornerOffsetY = tileCenterY - Size;
        
        // blit white pixels
        for (int y = cornerOffsetY; y < cornerOffsetY + cornerSize; y++)
        {
            for (int x = cornerOffsetX; x < cornerOffsetX + cornerSize; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
    }
    
    public void BlitCornerColor(int row, int column, int colorId)
    {
        Color color = TilesetColorPaletteSystem.GetCornerColor(colorId);
        
       BlitCorner(row, column, color);
    }
    
    public void BlitHorizontalEdgeTexture(int row, int column, byte[] pixels, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize;
        int tileCenterY = tileOffsetY + TileSize;
        
        // Corner texture
        int horizontalEdgeWidth = TileSize - Size * 2 ;
        int horizontalEdgeHeight = Size * 2;
        int edgeOffsetX = tileCenterX - horizontalEdgeWidth / 2;
        int edgeOffsetY = tileCenterY - Size;
        
        // blit white pixels
        for (int y = 0; y < horizontalEdgeHeight; y++)
        {
            for (int x = 0; x < horizontalEdgeWidth; x++)
            {
                int positionInPngX = x + edgeOffsetX;
                int positionInPngY = y + edgeOffsetY;

                int positionInByteArrayX = x + Size;
                int positionInByteArrayY = y + TileSize / 2 - Size;

                int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                // If we have a texture ready in the sprite sheet
                // We just copy that texture over to the next sprite sheet
                if (pixels != null)
                {
                    byte r = pixels[indexInArray + 0];
                    byte g = pixels[indexInArray + 1];
                    byte b = pixels[indexInArray + 2];
                    byte a = pixels[indexInArray + 3];

                    Pixel pixel = new Pixel(r, g, b, a, false);
                    
                    PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                }
                else
                {
                    PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                }
            }
        }
    }
    
    public void BlitHorizontalEdge(int row, int column, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int horizontalEdgeWidth = TileSize - Size * 2 ;
        int horizontalEdgeHeight = Size * 2;
        int edgeOffsetX = tileOffsetX + Size;
        int edgeOffsetY = tileCenterY - Size / 2;
        
        // blit white pixels
        for (int y = edgeOffsetY; y < edgeOffsetY + horizontalEdgeHeight; y++)
        {
            for (int x = edgeOffsetX; x < edgeOffsetX + horizontalEdgeWidth; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
    }
    
    public void BlitHorizontalEdgeColor(int row, int column, int colorId)
    {
        Color color = TilesetColorPaletteSystem.GetEdgeHorizontalColor(colorId);
        
        BlitHorizontalEdge(row, column, color);
    }
    
    public void BlitVerticalEdgeTexture(int row, int column, byte[] pixels, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize;
        int tileCenterY = tileOffsetY + TileSize;
        
        // Corner texture
        int horizontalEdgeWidth = Size * 2;
        int horizontalEdgeHeight = TileSize - Size * 2;
        int edgeOffsetX = tileCenterX - Size;
        int edgeOffsetY = tileCenterY - horizontalEdgeHeight / 2;
        
        // blit white pixels
        for (int y = 0; y < horizontalEdgeHeight; y++)
        {
            for (int x = 0; x < horizontalEdgeWidth; x++)
            {
                int positionInPngX = x + edgeOffsetX;
                int positionInPngY = y + edgeOffsetY;

                int positionInByteArrayX = x + TileSize / 2 - Size;
                int positionInByteArrayY = y + Size;

                int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                // If we have a texture ready in the sprite sheet
                // We just copy that texture over to the next sprite sheet
                if (pixels != null)
                {
                    byte r = pixels[indexInArray + 0];
                    byte g = pixels[indexInArray + 1];
                    byte b = pixels[indexInArray + 2];
                    byte a = pixels[indexInArray + 3];

                    Pixel pixel = new Pixel(r, g, b, a, false);
                    
                    PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                }
                else
                {
                    PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                }
            }
        }
        
    }
    
    public void BlitVerticalEdge(int row, int column, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int horizontalEdgeWidth = Size * 2;
        int horizontalEdgeHeight = TileSize - Size * 2;
        int edgeOffsetX = tileCenterX - Size / 2;
        int edgeOffsetY = tileOffsetY + Size;
        
        // blit white pixels
        for (int y = edgeOffsetY; y < edgeOffsetY + horizontalEdgeHeight; y++)
        {
            for (int x = edgeOffsetX; x < edgeOffsetX + horizontalEdgeWidth; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
    }
    
    public void BlitVerticalEdgeColor(int row, int column, int colorId)
    {
        Color color = TilesetColorPaletteSystem.GetEdgeVerticalColor(colorId);
        
        BlitVerticalEdge(row, column, color);
    }
    
    public void BlitVerticalEdgeGeometry(int row, int column, Enums.TileGeometryAndRotation geometry)
    {
        byte[] tileGeometryBytes = DefaultGeometrySpriteSheet.GetBytesFromShape(geometry);
        
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int horizontalEdgeWidth = Size * 2;
        int horizontalEdgeHeight = TileSize - Size * 2;
        int edgeOffsetX = tileCenterX - Size / 2;
        int edgeOffsetY = tileOffsetY + Size;
        
        // blit white pixels
        for (int y = 0; y < horizontalEdgeHeight; y++)
        {
            for (int x = 0; x < horizontalEdgeWidth; x++)
            {
                int pngOffsetY = edgeOffsetY + y;
                int pngOffsetX = edgeOffsetX + x;

                int byteArrayIndex = (x + y * Constants.TileSize) * 4;

                byte r = tileGeometryBytes[byteArrayIndex + 0];
                byte g = tileGeometryBytes[byteArrayIndex + 1];
                byte b = tileGeometryBytes[byteArrayIndex + 2];
                byte a = tileGeometryBytes[byteArrayIndex + 3];

                Pixel pixel = new Pixel(r, g, b, a, false);
                
                PngBuilder.SetPixel(pixel, pngOffsetX, pngOffsetY);
            }
        }
        
    }
    
    public void BlitTileBorderPreview(int row, int column,
        byte[] centerPixels,
        byte[] cornerNorthWestPixels, 
        byte[] cornerNorthEastPixels,
        byte[] cornerSouthEastPixels,
        byte[] cornerSouthWestPixels,
        byte[] edgeVerticalEastPixels,
        byte[] edgeVerticalWestPixels,
        byte[] edgeHorizontalNorthPixels,
        byte[] edgeHorizontalSouthPixels,
        Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // center texture
        int width = TileSize - Size * 2;
        int height = TileSize - Size * 2;

        int cornerWidth = Size;
        int cornerHeight = Size;

        int edgeVerticalWidth = Size;
        int edgeVerticalHeight = TileSize - Size * 2;

        int edgeHorizontalWidth = TileSize - Size * 2;
        int edgeHorizontalHeight = Size;
        
                
        int tileCenterOffsetX = tileCenterX - width / 2;
        int tileCenterOffsetY = tileCenterY - height / 2;

        int cornerNorthWestOffsetX = tileOffsetX + 0;
        int cornerNorthWestOffsetY = tileOffsetY + 0;

        int cornerNorthEastOffsetX = tileOffsetX + TileSize - Size;
        int cornerNorthEastOffsetY = tileOffsetY + 0;

        int cornerSouthEastOffsetX = tileOffsetX + TileSize - Size;
        int cornerSouthEastOffsetY = tileOffsetY + TileSize - Size;

        int cornerSouthWestOffsetX = tileOffsetX + 0;
        int cornerSouthWestOffsetY = tileOffsetY + TileSize - Size;

        int edgeHorizontalNorthOffsetX = tileOffsetX + Size;
        int edgeHorizontalNorthOffsetY = tileOffsetY + 0;
        
        int edgeHorizontalSouthOffsetX = tileOffsetX + Size;
        int edgeHorizontalSouthOffsetY = tileOffsetY + TileSize - Size;

        int edgeVerticalEastOffsetX = tileOffsetX + TileSize - Size;
        int edgeVerticalEastOffsetY = tileOffsetY + Size;

        int edgeVerticalWestOffsetX = tileOffsetX + 0;
        int edgeVerticalWestOffsetY = tileOffsetY + Size;
        

        // Blit corner textures
        {
            // North west corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerNorthWestOffsetX;
                        int positionInPngY = y + cornerNorthWestOffsetY;

                        int positionInByteArrayX = x + TileSize / 2;
                        int positionInByteArrayY = y + TileSize / 2;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerNorthWestPixels != null)
                        {
                            byte r = cornerNorthWestPixels[indexInArray + 0];
                            byte g = cornerNorthWestPixels[indexInArray + 1];
                            byte b = cornerNorthWestPixels[indexInArray + 2];
                            byte a = cornerNorthWestPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // North east corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerNorthEastOffsetX;
                        int positionInPngY = y + cornerNorthEastOffsetY;

                        int positionInByteArrayX = x + TileSize / 2 - Size;
                        int positionInByteArrayY = y + TileSize / 2;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerNorthEastPixels != null)
                        {
                            byte r = cornerNorthEastPixels[indexInArray + 0];
                            byte g = cornerNorthEastPixels[indexInArray + 1];
                            byte b = cornerNorthEastPixels[indexInArray + 2];
                            byte a = cornerNorthEastPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // South east corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerSouthEastOffsetX;
                        int positionInPngY = y + cornerSouthEastOffsetY;

                        int positionInByteArrayX = x + TileSize / 2 - Size;
                        int positionInByteArrayY = y + TileSize / 2 - Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerSouthEastPixels != null)
                        {
                            byte r = cornerSouthEastPixels[indexInArray + 0];
                            byte g = cornerSouthEastPixels[indexInArray + 1];
                            byte b = cornerSouthEastPixels[indexInArray + 2];
                            byte a = cornerSouthEastPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // South west corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerSouthWestOffsetX;
                        int positionInPngY = y + cornerSouthWestOffsetY;

                        int positionInByteArrayX = x + TileSize / 2;
                        int positionInByteArrayY = y + TileSize / 2 - Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerSouthWestPixels != null)
                        {
                            byte r = cornerSouthWestPixels[indexInArray + 0];
                            byte g = cornerSouthWestPixels[indexInArray + 1];
                            byte b = cornerSouthWestPixels[indexInArray + 2];
                            byte a = cornerSouthWestPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
        }
        
        // Horizontal edge
        {
            // Edge Horizontal North
            {
                for (int y = 0; y < edgeHorizontalHeight; y++)
                {
                    for (int x = 0; x < edgeHorizontalWidth; x++)
                    {
                        int positionInPngX = x + edgeHorizontalNorthOffsetX;
                        int positionInPngY = y + edgeHorizontalNorthOffsetY;

                        int positionInByteArrayX = x + Size;
                        int positionInByteArrayY = y + TileSize / 2;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
            
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeHorizontalNorthPixels != null)
                        {
                            byte r = edgeHorizontalNorthPixels[indexInArray + 0];
                            byte g = edgeHorizontalNorthPixels[indexInArray + 1];
                            byte b = edgeHorizontalNorthPixels[indexInArray + 2];
                            byte a = edgeHorizontalNorthPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // Edge Horizontal South
            {
                for (int y = 0; y < edgeHorizontalHeight; y++)
                {
                    for (int x = 0; x < edgeHorizontalWidth; x++)
                    {
                        int positionInPngX = x + edgeHorizontalSouthOffsetX;
                        int positionInPngY = y + edgeHorizontalSouthOffsetY;

                        int positionInByteArrayX = x + Size;
                        int positionInByteArrayY = y + TileSize / 2 - Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
            
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeHorizontalSouthPixels != null)
                        {
                            byte r = edgeHorizontalSouthPixels[indexInArray + 0];
                            byte g = edgeHorizontalSouthPixels[indexInArray + 1];
                            byte b = edgeHorizontalSouthPixels[indexInArray + 2];
                            byte a = edgeHorizontalSouthPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
        }
        
        // Vertical edge
        {
            // Edge Vertical east
            {
                for (int y = 0; y < edgeVerticalHeight; y++)
                {
                    for (int x = 0; x < edgeVerticalWidth; x++)
                    {
                        int positionInPngX = x + edgeVerticalEastOffsetX;
                        int positionInPngY = y + edgeVerticalEastOffsetY;

                        int positionInByteArrayX = x + TileSize / 2 - Size;
                        int positionInByteArrayY = y + Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;

                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeVerticalEastPixels != null)
                        {
                            byte r = edgeVerticalEastPixels[indexInArray + 0];
                            byte g = edgeVerticalEastPixels[indexInArray + 1];
                            byte b = edgeVerticalEastPixels[indexInArray + 2];
                            byte a = edgeVerticalEastPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);

                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);

                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }

            // Edge Vertical West
            {
                for (int y = 0; y < edgeVerticalHeight; y++)
                {
                    for (int x = 0; x < edgeVerticalWidth; x++)
                    {
                        int positionInPngX = x + edgeVerticalWestOffsetX;
                        int positionInPngY = y + edgeVerticalWestOffsetY;

                        int positionInByteArrayX = x + TileSize / 2;
                        int positionInByteArrayY = y + Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;

                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeVerticalWestPixels != null)
                        {
                            byte r = edgeVerticalWestPixels[indexInArray + 0];
                            byte g = edgeVerticalWestPixels[indexInArray + 1];
                            byte b = edgeVerticalWestPixels[indexInArray + 2];
                            byte a = edgeVerticalWestPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);

                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);

                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
        }
    }
    
    public void BlitTilePreview(int row, int column,
        byte[] centerPixels,
        byte[] cornerNorthWestPixels, 
        byte[] cornerNorthEastPixels,
        byte[] cornerSouthEastPixels,
        byte[] cornerSouthWestPixels,
        byte[] edgeVerticalEastPixels,
        byte[] edgeVerticalWestPixels,
        byte[] edgeHorizontalNorthPixels,
        byte[] edgeHorizontalSouthPixels,
        TileGeometryAndRotation geometry,
        Color color)
    {
        byte[] tileGeometryBytes = DefaultGeometrySpriteSheet.GetBytesFromShape(geometry);  
        
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        int tileCenterX = tileOffsetX + TileSize;
        int tileCenterY = tileOffsetY + TileSize;
        
        // center texture
        int width = TileSize - Size * 2;
        int height = TileSize - Size * 2;

        int cornerWidth = Size;
        int cornerHeight = Size;

        int edgeVerticalWidth = Size;
        int edgeVerticalHeight = TileSize - Size * 2;

        int edgeHorizontalWidth = TileSize - Size * 2;
        int edgeHorizontalHeight = Size;
        
                
        int tileCenterOffsetX = tileCenterX - width / 2;
        int tileCenterOffsetY = tileCenterY - height / 2;

        int cornerNorthWestOffsetX = tileCenterX - TileSize / 2 + 0;
        int cornerNorthWestOffsetY = tileCenterY - TileSize / 2 + 0;

        int cornerNorthEastOffsetX = tileCenterX - TileSize / 2 + TileSize - Size;
        int cornerNorthEastOffsetY = tileCenterY - TileSize / 2 + 0;

        int cornerSouthEastOffsetX = tileCenterX - TileSize / 2 + TileSize - Size;
        int cornerSouthEastOffsetY = tileCenterY - TileSize / 2 + TileSize - Size;

        int cornerSouthWestOffsetX = tileCenterX - TileSize / 2 + 0;
        int cornerSouthWestOffsetY = tileCenterY - TileSize / 2 + TileSize - Size;

        int edgeHorizontalNorthOffsetX = tileCenterX - TileSize / 2 + Size;
        int edgeHorizontalNorthOffsetY = tileCenterY - TileSize / 2 + 0;
        
        int edgeHorizontalSouthOffsetX = tileCenterX - TileSize / 2 + Size;
        int edgeHorizontalSouthOffsetY = tileCenterY - TileSize / 2 + TileSize - Size;

        int edgeVerticalEastOffsetX = tileCenterX - TileSize / 2 + TileSize - Size;
        int edgeVerticalEastOffsetY = tileCenterY - TileSize / 2 + Size;

        int edgeVerticalWestOffsetX = tileCenterX - TileSize / 2 + 0;
        int edgeVerticalWestOffsetY = tileCenterY - TileSize / 2 + Size;
        
        // blit white pixels
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int positionInPngX = x + tileCenterOffsetX;
                int positionInPngY = y + tileCenterOffsetY;

                int positionInByteArrayX = x + Size;
                int positionInByteArrayY = y + Size;

                int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                // If we have a texture ready in the sprite sheet
                // We just copy that texture over to the next sprite sheet
                if (centerPixels != null)
                {
                    byte r = centerPixels[indexInArray + 0];
                    byte g = centerPixels[indexInArray + 1];
                    byte b = centerPixels[indexInArray + 2];
                    byte a = centerPixels[indexInArray + 3];

                    Pixel pixel = new Pixel(r, g, b, a, false);
                    
                    PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                }
                else
                {
                    if (tileGeometryBytes != null)
                    {
                        byte r = tileGeometryBytes[indexInArray + 0];
                        byte g = tileGeometryBytes[indexInArray + 1];
                        byte b = tileGeometryBytes[indexInArray + 2];
                        byte a = tileGeometryBytes[indexInArray + 3];

                        Pixel pixel = new Pixel(r, g, b, a, false);
                    
                        PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    }
                    else
                    {
                        PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                    }
                }
            }
        }


        // Blit corner textures
        {
            // North west corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerNorthWestOffsetX;
                        int positionInPngY = y + cornerNorthWestOffsetY;

                        int positionInByteArrayX = x + TileSize / 2;
                        int positionInByteArrayY = y + TileSize / 2;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerNorthWestPixels != null)
                        {
                            byte r = cornerNorthWestPixels[indexInArray + 0];
                            byte g = cornerNorthWestPixels[indexInArray + 1];
                            byte b = cornerNorthWestPixels[indexInArray + 2];
                            byte a = cornerNorthWestPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // North east corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerNorthEastOffsetX;
                        int positionInPngY = y + cornerNorthEastOffsetY;

                        int positionInByteArrayX = x + TileSize / 2 - Size;
                        int positionInByteArrayY = y + TileSize / 2;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerNorthEastPixels != null)
                        {
                            byte r = cornerNorthEastPixels[indexInArray + 0];
                            byte g = cornerNorthEastPixels[indexInArray + 1];
                            byte b = cornerNorthEastPixels[indexInArray + 2];
                            byte a = cornerNorthEastPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // South east corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerSouthEastOffsetX;
                        int positionInPngY = y + cornerSouthEastOffsetY;

                        int positionInByteArrayX = x + TileSize / 2 - Size;
                        int positionInByteArrayY = y + TileSize / 2 - Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerSouthEastPixels != null)
                        {
                            byte r = cornerSouthEastPixels[indexInArray + 0];
                            byte g = cornerSouthEastPixels[indexInArray + 1];
                            byte b = cornerSouthEastPixels[indexInArray + 2];
                            byte a = cornerSouthEastPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // South west corner
            {
                for (int y = 0; y < cornerHeight; y++)
                {
                    for (int x = 0; x < cornerWidth; x++)
                    {
                        int positionInPngX = x + cornerSouthWestOffsetX;
                        int positionInPngY = y + cornerSouthWestOffsetY;

                        int positionInByteArrayX = x + TileSize / 2;
                        int positionInByteArrayY = y + TileSize / 2 - Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (cornerSouthWestPixels != null)
                        {
                            byte r = cornerSouthWestPixels[indexInArray + 0];
                            byte g = cornerSouthWestPixels[indexInArray + 1];
                            byte b = cornerSouthWestPixels[indexInArray + 2];
                            byte a = cornerSouthWestPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                    
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
        }
        
        // Horizontal edge
        {
            // Edge Horizontal North
            {
                for (int y = 0; y < edgeHorizontalHeight; y++)
                {
                    for (int x = 0; x < edgeHorizontalWidth; x++)
                    {
                        int positionInPngX = x + edgeHorizontalNorthOffsetX;
                        int positionInPngY = y + edgeHorizontalNorthOffsetY;

                        int positionInByteArrayX = x + Size;
                        int positionInByteArrayY = y + TileSize / 2;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
            
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeHorizontalNorthPixels != null)
                        {
                            byte r = edgeHorizontalNorthPixels[indexInArray + 0];
                            byte g = edgeHorizontalNorthPixels[indexInArray + 1];
                            byte b = edgeHorizontalNorthPixels[indexInArray + 2];
                            byte a = edgeHorizontalNorthPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
            
            // Edge Horizontal South
            {
                for (int y = 0; y < edgeHorizontalHeight; y++)
                {
                    for (int x = 0; x < edgeHorizontalWidth; x++)
                    {
                        int positionInPngX = x + edgeHorizontalSouthOffsetX;
                        int positionInPngY = y + edgeHorizontalSouthOffsetY;

                        int positionInByteArrayX = x + Size;
                        int positionInByteArrayY = y + TileSize / 2 - Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
            
                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeHorizontalSouthPixels != null)
                        {
                            byte r = edgeHorizontalSouthPixels[indexInArray + 0];
                            byte g = edgeHorizontalSouthPixels[indexInArray + 1];
                            byte b = edgeHorizontalSouthPixels[indexInArray + 2];
                            byte a = edgeHorizontalSouthPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);
                
                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                
                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
        }
        
        // Vertical edge
        {
            // Edge Vertical east
            {
                for (int y = 0; y < edgeVerticalHeight; y++)
                {
                    for (int x = 0; x < edgeVerticalWidth; x++)
                    {
                        int positionInPngX = x + edgeVerticalEastOffsetX;
                        int positionInPngY = y + edgeVerticalEastOffsetY;

                        int positionInByteArrayX = x + TileSize / 2 - Size;
                        int positionInByteArrayY = y + Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;

                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeVerticalEastPixels != null)
                        {
                            byte r = edgeVerticalEastPixels[indexInArray + 0];
                            byte g = edgeVerticalEastPixels[indexInArray + 1];
                            byte b = edgeVerticalEastPixels[indexInArray + 2];
                            byte a = edgeVerticalEastPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);

                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);

                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }

            // Edge Vertical West
            {
                for (int y = 0; y < edgeVerticalHeight; y++)
                {
                    for (int x = 0; x < edgeVerticalWidth; x++)
                    {
                        int positionInPngX = x + edgeVerticalWestOffsetX;
                        int positionInPngY = y + edgeVerticalWestOffsetY;

                        int positionInByteArrayX = x + TileSize / 2;
                        int positionInByteArrayY = y + Size;

                        int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;

                        // If we have a texture ready in the sprite sheet
                        // We just copy that texture over to the next sprite sheet
                        if (edgeVerticalWestPixels != null)
                        {
                            byte r = edgeVerticalWestPixels[indexInArray + 0];
                            byte g = edgeVerticalWestPixels[indexInArray + 1];
                            byte b = edgeVerticalWestPixels[indexInArray + 2];
                            byte a = edgeVerticalWestPixels[indexInArray + 3];

                            Pixel pixel = new Pixel(r, g, b, a, false);

                            PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);

                        }
                        else
                        {
                            PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                        }
                    }
                }
            }
        }
    }
    
    public void BlitTileCenterTexture(int row, int column, byte[] pixels, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize;
        int tileCenterY = tileOffsetY + TileSize;
        
        // Corner texture
        int width = TileSize - Size * 2;
        int height = TileSize - Size * 2;
        
        int tileCenterOffsetX = tileCenterX - width / 2;
        int tileCenterOffsetY = tileCenterY - height / 2;
        
        // blit white pixels
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int positionInPngX = x + tileCenterOffsetX;
                int positionInPngY = y + tileCenterOffsetY;

                int positionInByteArrayX = x + Size;
                int positionInByteArrayY = y + Size;

                int indexInArray = (positionInByteArrayX + positionInByteArrayY * Constants.TileSize) * 4;
                
                // If we have a texture ready in the sprite sheet
                // We just copy that texture over to the next sprite sheet
                if (pixels != null)
                {
                    byte r = pixels[indexInArray + 0];
                    byte g = pixels[indexInArray + 1];
                    byte b = pixels[indexInArray + 2];
                    byte a = pixels[indexInArray + 3];

                    Pixel pixel = new Pixel(r, g, b, a, false);
                    
                    PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                    
                }
                else
                {
                    PngBuilder.SetPixel(color.R, color.G, color.B, positionInPngX, positionInPngY);
                }
            }
        }
    }
    
    public void BlitTileCenterColor(int row, int column, Color color, 
        Color cornerNorthWest, Color cornerNorthEast, Color cornerSouthEast, Color cornerSouthWest,
        Color edgeVerticalEast, Color edgeVerticalWest, Color edgeHorizontalNorth, Color edgeHorizontalSouth)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int width = TileSize - Size * 2;
        int height = TileSize - Size * 2;
        
        int tileCenterOffsetX = tileCenterX - width / 2;
        int tileCenterOffsetY = tileCenterY - height / 2;
        
        // blit colored pixels
        // representing the center of the tile
        for (int y = tileCenterOffsetY; y < tileCenterOffsetY + height; y++)
        {
            for (int x = tileCenterOffsetX; x < tileCenterOffsetX + width; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
        
        // blit corner colors
        {
            // blit north west corner
            int northWestCornerOffsetX = tileOffsetX + 0;
            int northWestCornerOffsetY = tileOffsetY + 0;
            for (int y = northWestCornerOffsetY; y < northWestCornerOffsetY + Size; y++)
            {
                for (int x = northWestCornerOffsetX; x < northWestCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerNorthWest.R, cornerNorthWest.G, cornerNorthWest.B, x, y);
                }
            }
            
            // blit north east corner
            int northEastCornerOffsetX = tileOffsetX + TileSize - Size;
            int northEastCornerOffsetY = tileOffsetY + 0;
            for (int y = northEastCornerOffsetY; y < northEastCornerOffsetY + Size; y++)
            {
                for (int x = northEastCornerOffsetX; x < northEastCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerNorthEast.R, cornerNorthEast.G, cornerNorthEast.B, x, y);
                }
            }
            
            // blit south east corner
            int southEastCornerOffsetX = tileOffsetX + TileSize - Size;
            int southEastCornerOffsetY = tileOffsetY + TileSize - Size;
            for (int y = southEastCornerOffsetY; y < southEastCornerOffsetY + Size; y++)
            {
                for (int x = southEastCornerOffsetX; x < southEastCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerSouthEast.R, cornerSouthEast.G, cornerSouthEast.B, x, y);
                }
            }
            
            // blit south west corner
            int southWestCornerOffsetX = tileOffsetX + 0;
            int southWestCornerOffsetY = tileOffsetY + TileSize - Size;
            for (int y = southWestCornerOffsetY; y < southWestCornerOffsetY + Size; y++)
            {
                for (int x = southWestCornerOffsetX; x < southWestCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerSouthWest.R, cornerSouthWest.G, cornerSouthWest.B, x, y);
                }
            }
        }
        
        // blit Edge colors
        {
            
            // Vertical edge colors
            {
                // Vertical East
                {
                    int eastEdgeOffsetX = tileOffsetX + TileSize - Size;
                    int eastEdgeOffsetY = tileOffsetY + Size;
                    int edgeWidth = Size;
                    int edgeHeight = TileSize - Size * 2;
                    for (int y = eastEdgeOffsetY; y < eastEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = eastEdgeOffsetX; x < eastEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeVerticalEast.R, edgeVerticalEast.G, edgeVerticalEast.B, x, y);
                        }
                    }
                }
                
                // Vertical West
                {
                    int westEdgeOffsetX = tileOffsetX + 0;
                    int westEdgeOffsetY = tileOffsetY + Size;
                    int edgeWidth = Size;
                    int edgeHeight = TileSize - Size * 2;
                    for (int y = westEdgeOffsetY; y < westEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = westEdgeOffsetX; x < westEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeVerticalWest.R, edgeVerticalWest.G, edgeVerticalWest.B, x, y);
                        }
                    }
                }
            }
            
            // Horizontal edge colors
            {
                // Horizontal North
                {
                    int northEdgeOffsetX = tileOffsetX + Size;
                    int northEdgeOffsetY = tileOffsetY + 0;
                    int edgeWidth = TileSize - Size * 2;
                    int edgeHeight = Size;
                    for (int y = northEdgeOffsetY; y < northEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = northEdgeOffsetX; x < northEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeHorizontalNorth.R, edgeHorizontalNorth.G, edgeHorizontalNorth.B, x, y);
                        }
                    }
                }
                
                // Horizontal South
                {
                    int southEdgeOffsetX = tileOffsetX + Size;
                    int southEdgeOffsetY = tileOffsetY + TileSize - Size;
                    int edgeWidth = TileSize - Size * 2;
                    int edgeHeight = Size;
                    for (int y = southEdgeOffsetY; y < southEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = southEdgeOffsetX; x < southEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeHorizontalSouth.R, edgeHorizontalSouth.G, edgeHorizontalSouth.B, x, y);
                        }
                    }
                }
            }
        }
    }
    
    public void BlitTileCenter(int row, int column, Color color)
    {
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int width = TileSize - Size * 2;
        int height = TileSize - Size * 2;
        
        int tileCenterOffsetX = tileOffsetX + Size;
        int tileCenterOffsetY = tileOffsetY + Size;
        
        // blit white pixels
        for (int y = tileCenterOffsetY; y < tileCenterOffsetY + height; y++)
        {
            for (int x = tileCenterOffsetX; x < tileCenterOffsetX + width; x++)
            {
                PngBuilder.SetPixel(color.R, color.G, color.B, x, y);
            }
        }
    }
    
    public void BlitTileCenterGeometry(int row, int column, Enums.TileGeometryAndRotation geometry)
    {
        byte[] tileGeometryBytes = DefaultGeometrySpriteSheet.GetBytesFromShape(geometry);  
        if(tileGeometryBytes == null)
            return;

        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int width = TileSize - Size * 2;
        int height = TileSize - Size * 2;
        
        // blit white pixels
        for (int y = Size; y < Size + height; y++)
        {
            for (int x = Size; x < Size + width; x++)
            {
                int positionInPngX = tileOffsetX + x;
                int positionInPngY = tileOffsetY + y;
                
                int indexInByteArray = (x + y * Constants.TileSize) * 4;

                byte r = tileGeometryBytes[indexInByteArray + 0];
                byte g = tileGeometryBytes[indexInByteArray + 1];
                byte b = tileGeometryBytes[indexInByteArray + 2];
                byte a = tileGeometryBytes[indexInByteArray + 3];

                Pixel pixel = new Pixel(r, g, b, a, false);
                
                PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
            }
        }
    }
    
    
    public void BlitTileCenterGeometryColor(int row, int column, Enums.TileGeometryAndRotation geometry,
        Color cornerNorthWest, Color cornerNorthEast, Color cornerSouthEast, Color cornerSouthWest,
        Color edgeVerticalEast, Color edgeVerticalWest, Color edgeHorizontalNorth, Color edgeHorizontalSouth)
    {
        byte[] tileGeometryBytes = DefaultGeometrySpriteSheet.GetBytesFromShape(geometry);
        if(tileGeometryBytes == null)
            return;
        
        // Top left coordinates 
        int tileOffsetX = column * TileSize;
        int tileOffsetY = row * TileSize;
        
        // Corner is going to be centered
        int tileCenterX = tileOffsetX + TileSize / 2;
        int tileCenterY = tileOffsetY + TileSize / 2;
        
        // Corner texture
        int width = TileSize - Size * 2;
        int height = TileSize - Size * 2;
        
        // blit white pixels
        for (int y = Size; y < Size + height; y++)
        {
            for (int x = Size; x < Size + width; x++)
            {
                int positionInPngX = tileOffsetX + x;
                int positionInPngY = tileOffsetY + y;
                
                int indexInByteArray = (x + y * Constants.TileSize) * 4;

                byte r = tileGeometryBytes[indexInByteArray + 0];
                byte g = tileGeometryBytes[indexInByteArray + 1];
                byte b = tileGeometryBytes[indexInByteArray + 2];
                byte a = tileGeometryBytes[indexInByteArray + 3];

                Pixel pixel = new Pixel(r, g, b, a, false);
                
                PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
            }
        }
        
        // blit corner colors
        {
            // blit north west corner
            int northWestCornerOffsetX = tileOffsetX + 0;
            int northWestCornerOffsetY = tileOffsetY + 0;
            for (int y = northWestCornerOffsetY; y < northWestCornerOffsetY + Size; y++)
            {
                for (int x = northWestCornerOffsetX; x < northWestCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerNorthWest.R, cornerNorthWest.G, cornerNorthWest.B, x, y);
                }
            }
            
            // blit north east corner
            int northEastCornerOffsetX = tileOffsetX + TileSize - Size;
            int northEastCornerOffsetY = tileOffsetY + 0;
            for (int y = northEastCornerOffsetY; y < northEastCornerOffsetY + Size; y++)
            {
                for (int x = northEastCornerOffsetX; x < northEastCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerNorthEast.R, cornerNorthEast.G, cornerNorthEast.B, x, y);
                }
            }
            
            // blit south east corner
            int southEastCornerOffsetX = tileOffsetX + TileSize - Size;
            int southEastCornerOffsetY = tileOffsetY + TileSize - Size;
            for (int y = southEastCornerOffsetY; y < southEastCornerOffsetY + Size; y++)
            {
                for (int x = southEastCornerOffsetX; x < southEastCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerSouthEast.R, cornerSouthEast.G, cornerSouthEast.B, x, y);
                }
            }
            
            // blit south west corner
            int southWestCornerOffsetX = tileOffsetX + 0;
            int southWestCornerOffsetY = tileOffsetY + TileSize - Size;
            for (int y = southWestCornerOffsetY; y < southWestCornerOffsetY + Size; y++)
            {
                for (int x = southWestCornerOffsetX; x < southWestCornerOffsetX + Size; x++)
                {
                    PngBuilder.SetPixel(cornerSouthWest.R, cornerSouthWest.G, cornerSouthWest.B, x, y);
                }
            }
        }
        
        // blit Edge colors
        {
            
            // Vertical edge colors
            {
                // Vertical East
                {
                    int eastEdgeOffsetX = tileOffsetX + TileSize - Size;
                    int eastEdgeOffsetY = tileOffsetY + Size;
                    int edgeWidth = Size;
                    int edgeHeight = TileSize - Size * 2;
                    for (int y = eastEdgeOffsetY; y < eastEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = eastEdgeOffsetX; x < eastEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeVerticalEast.R, edgeVerticalEast.G, edgeVerticalEast.B, x, y);
                        }
                    }
                }
                
                // Vertical West
                {
                    int westEdgeOffsetX = tileOffsetX + 0;
                    int westEdgeOffsetY = tileOffsetY + Size;
                    int edgeWidth = Size;
                    int edgeHeight = TileSize - Size * 2;
                    for (int y = westEdgeOffsetY; y < westEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = westEdgeOffsetX; x < westEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeVerticalWest.R, edgeVerticalWest.G, edgeVerticalWest.B, x, y);
                        }
                    }
                }
            }
            
            // Horizontal edge colors
            {
                // Horizontal North
                {
                    int northEdgeOffsetX = tileOffsetX + Size;
                    int northEdgeOffsetY = tileOffsetY + 0;
                    int edgeWidth = TileSize - Size * 2;
                    int edgeHeight = Size;
                    for (int y = northEdgeOffsetY; y < northEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = northEdgeOffsetX; x < northEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeHorizontalNorth.R, edgeHorizontalNorth.G, edgeHorizontalNorth.B, x, y);
                        }
                    }
                }
                
                // Horizontal South
                {
                    int southEdgeOffsetX = tileOffsetX + Size;
                    int southEdgeOffsetY = tileOffsetY + TileSize - Size;
                    int edgeWidth = TileSize - Size * 2;
                    int edgeHeight = Size;
                    for (int y = southEdgeOffsetY; y < southEdgeOffsetY + edgeHeight; y++)
                    {
                        for (int x = southEdgeOffsetX; x < southEdgeOffsetX + edgeWidth; x++)
                        {
                            PngBuilder.SetPixel(edgeHorizontalSouth.R, edgeHorizontalSouth.G, edgeHorizontalSouth.B, x, y);
                        }
                    }
                }
            }
        }
    }

    
    public void BlitString(int offsetX, int offsetY, string text)
    {
        int characterWidth = DefaultMonoSpaceCharactersSpriteSheet.CharacterWidth;
        int characterHeight = DefaultMonoSpaceCharactersSpriteSheet.CharacterHeight;

        int textLimit = (Constants.TileSize * 2 + Constants.TileSize / 2) / characterWidth;
        textLimit = Math.Min(textLimit, text.Length);
        for (int textIndex = 0; textIndex < textLimit; textIndex++)
        {
            char thisCharacter = text[textIndex];

            byte[] characterBytes = DefaultMonoSpaceCharactersSpriteSheet.GetBytesFromCharacter(thisCharacter);
            
            if (characterBytes == null)
                continue;

            int characterPositionX = offsetX + textIndex * characterWidth;
            int characterPositionY = offsetY;
            
            // blit white pixels
            for (int y = 0; y < characterHeight; y++)
            {
                for (int x = 0; x < characterWidth; x++)
                {
                    int positionInPngX = characterPositionX + x;
                    int positionInPngY = characterPositionY + y;

                    int indexInByteArray = (x + y * characterWidth) * 4;

                    byte r = characterBytes[indexInByteArray + 0];
                    byte g = characterBytes[indexInByteArray + 1];
                    byte b = characterBytes[indexInByteArray + 2];
                    byte a = characterBytes[indexInByteArray + 3];

                    Pixel pixel = new Pixel(r, g, b, a, false);

                    PngBuilder.SetPixel(pixel, positionInPngX, positionInPngY);
                }
            }
        }
    }
}