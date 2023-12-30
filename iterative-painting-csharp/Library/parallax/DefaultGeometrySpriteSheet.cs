using BigGustave;
using Enums;
using Utility;

namespace Parallax;

// Utility class that helps us use the geometry sprite sheet
// The sprite pixels are stored in memory
// We can access the sprite pixels using this class 

public class DefaultGeometrySpriteSheet
{   
    // Stores the geometry sprite data
    // Maps The tile shape to a byte[]
    // (Tile Geometry) => (Sprite)
    public Dictionary<Enums.TileGeometryAndRotation, byte[]> GeometrySpriteMap;

    public void InitStage1()
    {
        GeometrySpriteMap = new Dictionary<TileGeometryAndRotation, byte[]>();
    }

    public void InitStage2()
    {
        LoadDefaultSpriteSheet();
    }

    public byte[] GetBytesFromShape(TileGeometryAndRotation shape)
    {
        if (!GeometrySpriteMap.ContainsKey(shape))
        {
            return null;
        }

        return GeometrySpriteMap[shape];
    }

    public void LoadSprite(Png png, Enums.TileGeometryAndRotation shape, int row, int column)
    {
        // Get sprite pixels from sprite sheet
        byte[] spritePixels = GetTileSpritePixelsFromPng(png, row, column);
        
        // Assign the shape to the sprite pixels
        GeometrySpriteMap.Add(shape, spritePixels);
    }
    
    public byte[] GetTileSpritePixelsFromPng(Png png, int row, int column)
    {
        // Check if png exists
        Utils.Assert(png != null);

        const int spriteSheetTileSize = 32;
        
        // RGBA Data
        const int channels = 4;
        byte[] tilePixels = new byte[Constants.TileSize * Constants.TileSize * channels];

        for (int y = 0; y < Constants.TileSize; y++)
        {
            for (int x = 0; x < Constants.TileSize; x++)
            {
                int sheetX = (column * spriteSheetTileSize) + x;
                int sheetY = (row * spriteSheetTileSize) + y;

                int arrayIndex = (x + y * Constants.TileSize) * channels;

                Pixel pixel = png.GetPixel(sheetX, sheetY);

                tilePixels[arrayIndex + 0] = pixel.R;
                tilePixels[arrayIndex + 1] = pixel.G;
                tilePixels[arrayIndex + 2] = pixel.B;
                tilePixels[arrayIndex + 3] = pixel.A;
            }
        }

        return tilePixels;
    }

    public void LoadDefaultSpriteSheet()
    {
        Png defaultGeometrySpriteSheetPng = Png.Open(Constants.GeometrySpriteSheetPath);

        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.SB_R0, 1, 1);
        
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HB_R0, 3, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HB_R1, 3, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HB_R2, 3, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HB_R3, 3, 7);
        
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.TB_R0, 5, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.TB_R1, 5, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.TB_R2, 7, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.TB_R3, 7, 3);
        
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R0, 9, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R1, 9, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R2, 9, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R3, 9, 7);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R4, 11, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R5, 11, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R6, 11, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L1_R7, 11, 7);
        
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R0, 13, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R1, 13, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R2, 13, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R3, 13, 7);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R4, 15, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R5, 15, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R6, 15, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.L2_R7, 15, 7);
        
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.QP_R0, 17, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.QP_R1, 17, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.QP_R2, 17, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.QP_R3, 17, 7);
        
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HP_R0, 19, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HP_R1, 19, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HP_R2, 19, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.HP_R3, 19, 7);
        
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.FP_R0, 21, 1);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.FP_R1, 21, 3);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.FP_R2, 21, 5);
        LoadSprite(defaultGeometrySpriteSheetPng, TileGeometryAndRotation.FP_R3, 21, 7);
        
    }
}