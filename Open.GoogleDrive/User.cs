using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Open.GoogleDrive;

public class About
{
    [JsonPropertyName("user")]
    public User User { get; set; }

    [JsonPropertyName("storageQuota")]
    public StorageQuota StorageQuota { get; set; }

    [JsonPropertyName("maxUploadSize")]
    public long MaxUploadSize { get; set; }
}

public class User
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("picture")]
    public Picture Picture { get; set; }

    [JsonPropertyName("isAuthenticatedUser")]
    public bool IsAuthenticatedUser { get; set; }

    [JsonPropertyName("permissionId")]
    public string PermissionId { get; set; }
}

public class Picture
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class StorageQuota
{

    [JsonPropertyName("limit")]
    public long Limit { get; set; }

    [JsonPropertyName("usage")]
    public long Usage { get; set; }

    [JsonPropertyName("usageInDrive")]
    public long UsageInDrive { get; set; }

    [JsonPropertyName("usageInDriveTrash")]
    public long UsageInDriveTrash { get; set; }
}
