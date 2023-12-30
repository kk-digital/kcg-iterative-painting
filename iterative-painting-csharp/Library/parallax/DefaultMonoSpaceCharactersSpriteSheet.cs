using System.Drawing;
using BigGustave;
using Utility;

namespace Parallax;

// Utility class that helps us use the geometry sprite sheet
// The sprite pixels are stored in memory
// We can access the sprite pixels using this class 

public class DefaultMonoSpaceCharactersSpriteSheet
{
    public const string MonoSpaceFontSpriteSheetPath = "assets/font/mono_space_sprite_sheet.png";
    // Stores the character sprite data
    // Maps The character to a byte[]
    // (character) => (Sprite)
    public Dictionary<char, byte[]> SpriteMap;

    public int CharacterWidth = 5;
    public int CharacterHeight = 5;

    public void InitStage1()
    {
        SpriteMap = new Dictionary<char, byte[]>();
    }

    public void InitStage2()
    {
        LoadDefaultSpriteSheet();
    }

    public byte[] GetBytesFromCharacter(char character)
    {
        if (!SpriteMap.ContainsKey(character))
        {
            return null;
        }

        return SpriteMap[character];
    }

    public void LoadSprite(Png png, char character, int row, int column)
    {
        // Make sure to not have duplicate characters
        if (SpriteMap.ContainsKey(character))
        {
            KLog.LogDebug($"The character {character} was already loaded from the sprite sheet");
            // Quick escape
            return;
        }
        
        // Get sprite pixels from sprite sheet
        byte[] spritePixels = GetCharacterSpritePixelsFromPng(png, row, column, Color.Red);
        
        // Assign the character to the sprite pixels
        SpriteMap.Add(character, spritePixels);
    }
    
    public byte[] GetCharacterSpritePixelsFromPng(Png png, int row, int column, Color color)
    {
        // Check if png exists
        Utils.Assert(png != null);
        
        // RGBA Data
        const int channels = 4;
        byte[] tilePixels = new byte[CharacterWidth * CharacterHeight * channels];

        for (int y = 0; y < CharacterHeight; y++)
        {
            for (int x = 0; x < CharacterWidth; x++)
            {
                int sheetX = (column * CharacterWidth) + x;
                int sheetY = (row * CharacterHeight) + y;

                int arrayIndex = (x + y * CharacterWidth) * channels;

                Pixel pixel = png.GetPixel(sheetX, sheetY);

                if (pixel.A != 0)
                {
                    tilePixels[arrayIndex + 0] = color.R;
                    tilePixels[arrayIndex + 1] = color.G;
                    tilePixels[arrayIndex + 2] = color.B;
                    tilePixels[arrayIndex + 3] = color.A;
                }
                else
                {
                    tilePixels[arrayIndex + 0] = pixel.R;
                    tilePixels[arrayIndex + 1] = pixel.G;
                    tilePixels[arrayIndex + 2] = pixel.B;
                    tilePixels[arrayIndex + 3] = pixel.A;
                }
            }
        }

        return tilePixels;
    }

    public void LoadDefaultSpriteSheet()
    {
        CharacterWidth = 5;
        CharacterHeight = 5;
        
        Png defaultMonospaceSpriteSheetPng = Png.Open(MonoSpaceFontSpriteSheetPath);

        LoadSprite(defaultMonospaceSpriteSheetPng, 'a', 0, 0);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'A', 0, 26);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'b', 0, 1);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'B', 0, 27);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'c', 0, 2);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'C', 0, 28);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'd', 0, 3);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'D', 0, 29);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'e', 0, 4);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'E', 0, 30);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'f', 0, 5);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'F', 0, 31);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'g', 0, 6);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'G', 0, 32);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'h', 0, 7);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'H', 0, 33);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'i', 0, 8);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'I', 0, 34);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'j', 0, 9);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'J', 0, 35);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'k', 0, 10);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'K', 0, 36);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'l', 0, 11);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'L', 0, 37);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'm', 0, 12);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'M', 0, 38);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'n', 0, 13);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'N', 0, 39);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'o', 0, 14);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'O', 0, 40);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'p', 0, 15);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'P', 0, 41);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'q', 0, 16);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'Q', 0, 42);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'r', 0, 17);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'R', 0, 43);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 's', 0, 18);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'S', 0, 44);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 't', 0, 19);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'T', 0, 45);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'u', 0, 20);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'U', 0, 46);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'v', 0, 21);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'V', 0, 47);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'w', 0, 22);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'W', 0, 48);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'x', 0, 23);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'X', 0, 49);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'y', 0, 24);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'Y', 0, 50);
        
        LoadSprite(defaultMonospaceSpriteSheetPng, 'z', 0, 25);
        LoadSprite(defaultMonospaceSpriteSheetPng, 'Z', 0, 51);
        
        
        LoadSprite(defaultMonospaceSpriteSheetPng, '0', 0, 52);
        LoadSprite(defaultMonospaceSpriteSheetPng, '1', 0, 53);
        LoadSprite(defaultMonospaceSpriteSheetPng, '2', 0, 54);
        LoadSprite(defaultMonospaceSpriteSheetPng, '3', 0, 55);
        LoadSprite(defaultMonospaceSpriteSheetPng, '4', 0, 56);
        LoadSprite(defaultMonospaceSpriteSheetPng, '5', 0, 57);
        LoadSprite(defaultMonospaceSpriteSheetPng, '6', 0, 58);
        LoadSprite(defaultMonospaceSpriteSheetPng, '7', 0, 59);
        LoadSprite(defaultMonospaceSpriteSheetPng, '8', 0, 60);
        LoadSprite(defaultMonospaceSpriteSheetPng, '9', 0, 61);
        LoadSprite(defaultMonospaceSpriteSheetPng, '.', 0, 62);
        LoadSprite(defaultMonospaceSpriteSheetPng, ':', 0, 63);
        LoadSprite(defaultMonospaceSpriteSheetPng, ',', 0, 64);
        LoadSprite(defaultMonospaceSpriteSheetPng, ';', 0, 65);
        LoadSprite(defaultMonospaceSpriteSheetPng, '(', 0, 66);
        LoadSprite(defaultMonospaceSpriteSheetPng, '*', 0, 67);
        LoadSprite(defaultMonospaceSpriteSheetPng, '!', 0, 68);
        LoadSprite(defaultMonospaceSpriteSheetPng, '?', 0, 69);
        LoadSprite(defaultMonospaceSpriteSheetPng, '}', 0, 70);
        LoadSprite(defaultMonospaceSpriteSheetPng, '^', 0, 71);
        LoadSprite(defaultMonospaceSpriteSheetPng, ')', 0, 72);
        LoadSprite(defaultMonospaceSpriteSheetPng, '#', 0, 73);
        LoadSprite(defaultMonospaceSpriteSheetPng, '$', 0, 74);
        LoadSprite(defaultMonospaceSpriteSheetPng, '{', 0, 75);
        LoadSprite(defaultMonospaceSpriteSheetPng, '%', 0, 76);
        LoadSprite(defaultMonospaceSpriteSheetPng, '_', 0, 77);
        LoadSprite(defaultMonospaceSpriteSheetPng, '/', 0, 78);
        LoadSprite(defaultMonospaceSpriteSheetPng, '+', 0, 79);
        LoadSprite(defaultMonospaceSpriteSheetPng, '-', 0, 80);
        LoadSprite(defaultMonospaceSpriteSheetPng, '[', 0, 81);
        LoadSprite(defaultMonospaceSpriteSheetPng, ']', 0, 82);
        LoadSprite(defaultMonospaceSpriteSheetPng, '&', 0, 83);
        LoadSprite(defaultMonospaceSpriteSheetPng, '=', 0, 84);
        LoadSprite(defaultMonospaceSpriteSheetPng, '<', 0, 85);
        LoadSprite(defaultMonospaceSpriteSheetPng, '>', 0, 86);
        LoadSprite(defaultMonospaceSpriteSheetPng, '\\', 0, 87);
    }
}