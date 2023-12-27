using System.Text.Json;
using System.Text.Json.Serialization;

namespace WangTiles
{
    public class TilesetDataDecoder
    {
        public static JsonSerializerOptions Options;

        public static bool IsInitialized;

        
        // checks if the JsonSerializer Options are initialized
        // if not initilize them
        public static void Init()
        {
            if (!IsInitialized)
            {
                // Configure the JSON serializer options
                Options = new JsonSerializerOptions { };
                Options.Converters.Add(new JsonStringEnumConverter());
                
                IsInitialized = true;
            }
        }

        public static Class Decode<Class>(byte[] data)
        {
            // right now we are using json to decode
            // but in the future we might use other formats
            return DecodeJson<Class>(data);
        }
        
        private static Class DecodeJson<Class>(byte[] data)
        {
            // check if the state is Initialized
            // if not initalize it
            Init();
            
            return JsonSerializer.Deserialize<Class>(data, Options);
        }
    }
}