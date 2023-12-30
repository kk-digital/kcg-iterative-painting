using BigGustave;
using KEngine;
using Utility;

namespace Parallax;

// Stores a sprite sheet png in memory
// Reads the corner/edge/tile pixels
public class TilesetSpriteSheetReader
{
    public UInt64 TilesetUuid;
    public UInt64 SpriteSheetUuid;
    public TilesetSpriteSheetData SpriteSheetData;
    public Dictionary<UInt64, Png> SpriteSheetPng;
    
    // --------- Dependencies --------------
    public ParallaxManager ParallaxManager;
    // -------------------------------------


    public void InitStage1(ParallaxManager parallaxManager)
    {
        ParallaxManager = parallaxManager;
        SpriteSheetPng = new Dictionary<UInt64, Png>();
    }

    public void InitStage2()
    {
        
    }
    
    // Opens a png file for reading
    public void ReadSpriteSheet(UInt64 tilesetUuid, UInt64 spriteSheetUuid)
    {
        TilesetUuid = tilesetUuid;
        SpriteSheetUuid = spriteSheetUuid;

        SpriteSheetData = ParallaxManager.GetSpriteSheet(SpriteSheetUuid);

        if (SpriteSheetData == null)
        {
            // Sprite sheet not found
            KLog.LogDebug($"Sprite sheet tilesetUuid : {tilesetUuid}, spriteSheetUuid:{spriteSheetUuid} was not found.");
            
            // Quick exit
            return;
        }

        if (!FileUtils.FileExistsFull(SpriteSheetData.Filepath))
        {
            KLog.LogDebug($"Sprite sheet file {SpriteSheetData.Filepath} does not exist");
            // Quick exit
            return;
        }
        
        if (SpriteSheetPng.ContainsKey(spriteSheetUuid))
        {
            KLog.LogDebug($"sprite sheet already exists in sprite sheet reader dictionary");
            return;
        }
        
        Png png = Png.Open(SpriteSheetData.Filepath);
        
        SpriteSheetPng.Add(spriteSheetUuid, png);
    }
    
    
    public byte[] ReadSpritePixels(TilesetSpriteData sprite)
    {
        // Make sure a sprite sheet is already in memory
        if (!SpriteSheetPng.ContainsKey(sprite.SpriteSheetUuid))
        {
            KLog.LogDebug($"Error while trying to read sprite ['{sprite.StringId}', '{sprite.Uuid}'] from sprite sheet");
            return null;
        }

        Png png = SpriteSheetPng[sprite.SpriteSheetUuid];
        
        // If the sprite is not
        // present in the sprite sheet
        // return null
        if (!sprite.IsDrawnInSpriteSheet)
        {
            return null;
        }
        
        // The full “texture” coordinates in pixels
        int fullTextureViewY = (sprite.SpriteSheetRow + 0) * Constants.TileSize + Constants.TileSize / 2;
        int fullTextureViewX = (sprite.SpriteSheetColumn + 3) * Constants.TileSize + Constants.TileSize / 2;

        byte[] spritePixels = new byte[Constants.TileSize * Constants.TileSize * 4];

        for (int y = 0; y < Constants.TileSize; y++)
        {
            for (int x = 0; x < Constants.TileSize; x++)
            {
                int positionInSheetX = x + fullTextureViewX;
                int positionInSheetY = y + fullTextureViewY;
                int indexInArray = (x + y * Constants.TileSize) * 4;

                Pixel pixel = png.GetPixel(positionInSheetX, positionInSheetY);

                spritePixels[indexInArray + 0] = pixel.R;
                spritePixels[indexInArray + 1] = pixel.G;
                spritePixels[indexInArray + 2] = pixel.B;
                spritePixels[indexInArray + 3] = pixel.A;
            }
        }

        return spritePixels;
    }
}