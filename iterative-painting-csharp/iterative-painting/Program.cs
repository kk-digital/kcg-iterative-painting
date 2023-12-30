using System;
using System.Text;
using Parallax;
using Utility;

public class CustomUUID
{
    public static void Main()
    {
        UInt64 uuid = ShortHash.GenerateUUID();

        string hash = ShortHash.HashUUID(uuid);

        UInt64 decodeUuid = ShortHash.ToUUID(hash);
        
        System.Console.WriteLine($"uuid : {uuid}");
        System.Console.WriteLine($"hash : {hash}");
        System.Console.WriteLine($"decodeUuid : {decodeUuid}");
        
        Directory.SetCurrentDirectory("./../../../");

        ParallaxManager parallaxManager = new ParallaxManager();
        
        parallaxManager.InitStage1();
        parallaxManager.InitStage2();

        for (int j = 0; j < 20; j++)
        {
            TilesetInformation tilesetInformation = parallaxManager.CreateTileset();
            tilesetInformation.StringId = $"tileset_{j}";

            for (int i = 0; i < 100; i++)
            {
                TilesetTileCenter tileCenter = parallaxManager.AddTileCenter(tilesetInformation.Uuid);
                tileCenter.StringId = $"tile_{i}";
            }

            parallaxManager.Save(tilesetInformation.Uuid, "random_description");
        }
    }
}
