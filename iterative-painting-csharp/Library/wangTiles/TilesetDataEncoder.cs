using System.Text.Json;
using System.Text.Json.Serialization;

namespace WangTiles
{
    public class TilesetDataEncoder
    {
        public static JsonSerializerOptions Options;

        public static bool IsInitialized = false;

        
        // checks if the JsonSerializer Options are initialized
        // if not initilize them
        public static void Init()
        {
            if (!IsInitialized)
            {
                // Configure the JSON serializer options
                Options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                Options.Converters.Add(new JsonStringEnumConverter());
                
                IsInitialized = true;
            }
        }

        public static void Encode<Class>(string path, Class instance)
        {
            // right now we are using json to encode
            // but in the future we might use other formats
            EncodeJson(path, instance);
        }
        
        private static void EncodeJson<Class>(string path, Class instance)
        {
            // check if the state is Initialized
            // if not initalize it
            Init();

            var json = JsonSerializer.SerializeToUtf8Bytes(instance, Options);
                
            // replace this with Engine/File/
            File.WriteAllBytes(path, json);
        }
    }
}