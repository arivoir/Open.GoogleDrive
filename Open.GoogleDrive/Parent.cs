using System.Text.Json.Serialization;

namespace Open.GoogleDrive;

public class Parent
{
    [JsonPropertyName("kind")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Kind { get; set; }

    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Id { get; set; }

    [JsonPropertyName("selfLink")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SelfLink { get; set; }

    [JsonPropertyName("parentLink")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string ParentLink { get; set; }

    [JsonPropertyName("isRoot")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsRoot { get; set; }
}