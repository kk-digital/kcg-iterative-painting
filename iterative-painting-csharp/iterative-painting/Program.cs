using System;
using System.Text;
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

    }
}
