using System.Text.Json.Serialization;

namespace Open.GoogleDrive;

public class Response
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("nextPageToken")]
    public string NextPageToken { get; set; }

    [JsonPropertyName("files")]
    public File[] Items { get; set; }
}
