using System;
using System.Text;

public class CustomUUID
{
    private const string ALPHABET = "1234567890ABCDEFGHIJK";

    public long Timestamp { get; private set; }
    public int RandomBits { get; private set; }

    public CustomUUID(long timestamp = 0)
    {
        if (timestamp == 0)
        {
            timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        Timestamp = timestamp;
        Random random = new Random();
        RandomBits = random.Next();
    }
    
    public CustomUUID(long timestamp, int randomBits)
    {

        Timestamp = timestamp;
        RandomBits = randomBits;
    }

    public long ToInt64()
    {
        return (Timestamp << 32) | (uint)RandomBits;
    }

    public string ToStringValue()
    {
        long int64Value = ToInt64();
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < 10; i++)
        {
            int64Value = Math.DivRem(int64Value, ALPHABET.Length, out var remainder);
            result.Insert(0, ALPHABET[(int)remainder]);
        }

        result.Append(RandomBits.ToString("X").Substring(0, 2)); // Include the first 2 hex characters

        return result.ToString().PadLeft(10, '0');
    }

    public static CustomUUID FromInt64(long int64Value)
    {
        long timestamp = int64Value >> 32;
        int randomBits = (int)(int64Value & 0xFFFFFFFF);
        return new CustomUUID(timestamp, randomBits);
    }

    public static CustomUUID FromStringValue(string stringValue)
    {
        string timestampStr = stringValue.Substring(0, 10);
        string randomBitsStr = stringValue.Substring(10, 2);

        long int64Value = 0;
        foreach (char c in timestampStr)
        {
            int64Value = int64Value * ALPHABET.Length + ALPHABET.IndexOf(c);
        }

        int randomBits = int.Parse(randomBitsStr, System.Globalization.NumberStyles.HexNumber);
        int64Value = (int64Value << 32) | (uint)randomBits;

        return FromInt64(int64Value);
    }

    public static void Main()
    {
        // Example usage
        CustomUUID customUUID = new CustomUUID();
        Console.WriteLine("Original UUID:");
        Console.WriteLine($"Timestamp: {customUUID.Timestamp}");
        Console.WriteLine($"Random bits: {customUUID.RandomBits}");

        long int64Value = customUUID.ToInt64();
        string stringValue = customUUID.ToStringValue();

        Console.WriteLine("\nEncoded values:");
        Console.WriteLine($"Int64: {int64Value}");
        Console.WriteLine($"String: {stringValue}");

        CustomUUID decodedUUID = CustomUUID.FromStringValue(stringValue);
        Console.WriteLine("\nDecoded UUID:");
        Console.WriteLine($"Timestamp: {decodedUUID.Timestamp}");
        Console.WriteLine($"Random bits: {decodedUUID.RandomBits}");
    }
}
