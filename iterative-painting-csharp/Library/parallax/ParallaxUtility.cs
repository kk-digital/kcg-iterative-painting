using System.Security.Cryptography;
using Enums;
using KEngine;
using File = System.IO.File;
using SearchOption = KEngine.SearchOption;

namespace Parallax;

public class WangTilesUtility
{
    
    public static string GetStringFromVersionNumber(int version)
    {
        string versionString = version.ToString().PadLeft(4, '0');

        return "s" + versionString;
    }
    
    public static string GetStringFromDataId(int id)
    {
        string dataIdString = id.ToString().PadLeft(3, '0');

        return dataIdString;
    }

    public static string GetStringFromFileType(KcgData.FileType fileType)
    {
        string result = "unidentified";
        switch (fileType)
        {
            case KcgData.FileType.Json:
            {
                result = "json";
                break;
            }
            case KcgData.FileType.Jpeg:
            {
                result = "jpg";
                break;
            }
            case KcgData.FileType.Png:
            {
                result = "png";
                break;
            }
        }

        return result;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataType"></param>
    /// <returns></returns>
    public static string GetFolderStringFromDataType(DataType dataType)
    {
        string result = "unidentified";
        
        switch (dataType)
        {
            case DataType.Corner:
            {
                result = "corners";
                break;
            }
            case DataType.SpriteData:
            {
                result = "sprites";
                break;
            }
            case DataType.SpriteSheetData:
            {
                result = "sprite_sheets";
                break;
            }
            case DataType.HorizontalEdge:
            {
                result = "edges_horizontal";
                break;
            }
            case DataType.VerticalEdge:
            {
                result = "edges_vertical";
                break;
            }
            case DataType.Tile:
            {
                result = "tiles";
                break;
            }
        }

        return result;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataType"></param>
    /// <returns></returns>
    public static string GetStringFromDataType(DataType dataType)
    {
        string result = "unidentified";
        
        switch (dataType)
        {
            case DataType.Corner:
            {
                result = "corner";
                break;
            }
            case DataType.SpriteData:
            {
                result = "sprite";
                break;
            }
            case DataType.SpriteSheetData:
            {
                result = "sprite-sheet";
                break;
            }
            case DataType.HorizontalEdge:
            {
                result = "edge-horizontal";
                break;
            }
            case DataType.VerticalEdge:
            {
                result = "edge-vertical";
                break;
            }
            case DataType.Tile:
            {
                result = "tile";
                break;
            }
        }

        return result;
    }
    
    // tileset/s0000/corners/[one json file per corner]
    // tileset/s0000/edges_vertical/[one json file per vertical edge]
    // tileset/s0000/edges_horizontal/[one json file per horizontal]

    // tileset/s0000/sprites/[one json file per sprite]
    //      ^ one per sprite on each sheet

    // tileset/s0000/sprite_sheets/[one .png per spritesheet]
    // tileset/s0000/sprite_sheets/[one .json file per spritesheet]

    public static string GetDataPath(int version, int tilesetId, DataType dataType, int id, KcgData.FileType fileType)
    {
        string dataTypeFolderString = GetFolderStringFromDataType(dataType);
        string versionString = GetStringFromVersionNumber(version);
        string dataIdString = GetStringFromDataId(id);
        string tilesetIdString = GetStringFromDataId(tilesetId);
        string fileTypeString = GetStringFromFileType(fileType);
        string dataTypeString = GetStringFromDataType(dataType);
        
        string path = $"{Constants.CacheFolderName}/{Constants.TilesetsFolderName}/{tilesetIdString}/{versionString}/{dataTypeFolderString}/{dataTypeString}-{dataIdString}.{fileTypeString}";
        
        return path;
    }

    public static string GetManifestPath(int version, int tilesetId)
    {
        string versionString = GetStringFromVersionNumber(version);
        string tilesetIdString = GetStringFromDataId(tilesetId);
        
        string path = $"{Constants.CacheFolderName}/{Constants.TilesetsFolderName}/{tilesetIdString}/{versionString}/tileset-manifest.json";
        
        return path;
    }
    
    public static string GetTilesetPath(int version, int tilesetId)
    {
        string versionString = GetStringFromVersionNumber(version);
        string tilesetIdString = GetStringFromDataId(tilesetId);
        
        string path = $"{Constants.CacheFolderName}/{Constants.TilesetsFolderName}/{tilesetIdString}/{versionString}/tileset.json";
        
        return path;
    }
    
    
    public static string ComputeSHA256(string filePath)
    {
        using (var sha256 = SHA256.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }

    public static string GetDirectoryFromFilepath(string filePath)
    {
        // Get the directory (folder) path from the full file path
        string? folderPath = Path.GetDirectoryName(filePath);

        string result = "";
        if (folderPath != null)
        {
            result = folderPath;
        }
        
        return result;
    }

    public static List<int> ListTilesets()
    {
        List<int> tilesetIds = new List<int>();
        
        string path = $"{Constants.CacheFolderName}/{Constants.TilesetsFolderName}";
        
        if (!FileUtils.DirectoryExistsFull(path))
        {
            return tilesetIds;
        }
        
        string[] tilesetFolders =
            FileUtils.GetDirectoriesFull(path, "*", SearchOption.TopDirectoryOnly);

        foreach (string folder in tilesetFolders)
        {
            string folderCorrected = folder.Replace('\\', '/');
            string idString = folderCorrected.Split('/').LastOrDefault();
            int idInt = int.Parse(idString);
            
            tilesetIds.Add(idInt);
        }

        return tilesetIds;
    }
    
    public static List<TilesetManifest> ListVersions(int tilesetId)
    {
        string tilesetIdString = GetStringFromDataId(tilesetId);
        string path = $"{Constants.CacheFolderName}/{Constants.TilesetsFolderName}/{tilesetIdString}";
        
        if (!FileUtils.DirectoryExistsFull(path))
        {
            return new List<TilesetManifest>();
        }
        
        string[] versionFolders =
            FileUtils.GetDirectoriesFull(path, "*", SearchOption.TopDirectoryOnly);
        
        List<TilesetManifest> manifests = new List<TilesetManifest>();
        
        for (int versionIndex = 0; versionIndex < versionFolders.Length; versionIndex++)
        {
            string versionFolder = versionFolders[versionIndex];
                
            string sequenceNumberString = versionFolder.Split('/').LastOrDefault();
            int sequenceNumberInt = ExtractAndConvertToInt(sequenceNumberString);
            
            string manifestPath = $"{versionFolder}/{Constants.ManifestFileName}";
            
            if (!FileUtils.FileExistsFull(manifestPath))
            {
                continue;
            }
            
            // decode manifest json
            byte[] manifestBytes = FileUtils.ReadAllBytesFull(manifestPath);
            // Decode bytes to string using UTF-8 encoding
            TilesetManifest manifest = TilesetDataDecoder.Decode<TilesetManifest>(manifestBytes);

            manifests.Add(manifest);
        }

        return manifests;
    }
    
    // Convert the version string to int
    // example 's001' or 's005' 
    public static int ExtractAndConvertToInt(string input)
    {
        // Check if the string starts with "s" and has at least one digit
        if (input.Length > 1 && input[0] == 's' && Char.IsDigit(input[1]))
        {
            // Extract numeric part and convert to int
            string numericPart = input.Substring(1); // Skip the 's'
            return int.Parse(numericPart);
        }
        else
        {
            // Handle invalid format
            return 0; // or throw an exception, return a default value, etc.
        }
    }
}