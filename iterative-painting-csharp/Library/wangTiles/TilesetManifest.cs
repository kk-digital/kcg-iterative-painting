using Enums;

namespace WangTiles;


//A manifest file of all manifest files

public class Manifest
{
    public List<TilesetManifest> TilesetManifests { get; set; }
}

// Each tilesheet has a csv manifest file
// The name/path of all files in folders
// The size of each file
// The sha256 hash of each file
// Optional: Sequence number (incremented every update/change)

public class TilesetManifest
{
    public int Version { get; set; }
    public int SequenceNumber { get; set; }
    public string CreationDate { get; set; }
    public List<TilesetManifestItem> Files { get; set; }
}

public class TilesetManifestItem
{
    public DataType DataType { get; set; }
    public string Filepath { get; set; }
    public int FileSize { get; set; }
    public string HashSha256 { get; set; }
    public Int64 SequenceNumber { get; set; }
}