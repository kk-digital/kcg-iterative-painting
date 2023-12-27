

using System.Security.Cryptography;
using System.Text;
using KMath.Random;

namespace Utility;


public class ShortHash
{
    private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
    private const int HASH_LENGTH = 11;
    
    public static UInt64 GenerateUUID()
    {
        // Get current Unix time (32-bit)
        int unixTime32Bit = (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() & 0xFFFFFFFF);
        
        // Get random number
        int random32Bit = (int)Mt19937.genrand_int32();
        
        // Combine random number & time into a 64-bit integer
        UInt64 uuid = ((UInt64)unixTime32Bit & 0xFFFFFFFFUL) | ((UInt64)random32Bit << 32);
        
        return uuid;
    }

    public static UInt64 ToUUID(string hash)
    {
        UInt64 result = 0;

        for (int i = 0; i < hash.Length; i++)
        {
            char currentChar = hash[i];
            // TODO(): Use map instead of this shit
            // TODO(): Too slow fix it
            int index = ALPHABET.IndexOf(currentChar);
            result = result * 64 + (UInt64)index;
        }

        return result;
    }

    public static string HashUUID(UInt64 uuid)
    {
        char[] result = new char[HASH_LENGTH];

        for (int i = result.Length - 1; i >= 0; i--)
        {
            int index = (int)(uuid % 64);
            result[i] = ALPHABET[index];
            uuid /= 64;
        }

        return new string(result);
    }
}