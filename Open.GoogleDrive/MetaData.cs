using System.Text.Json.Serialization;

namespace Open.GoogleDrive;

public class ImageMetadata
{
    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("rotation")]
    public int Rotation { get; set; }

    [JsonPropertyName("location")]
    public Location Location { get; set; }
}

