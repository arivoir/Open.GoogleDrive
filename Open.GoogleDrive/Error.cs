using System.Text.Json.Serialization;

namespace Open.GoogleDrive;

public class ErrorResponse
{
    [JsonPropertyName("error")]
    public Error Error { get; set; }
}

public class Error
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("errors")]
    public ErrorDetail[] Errors { get; set; }
}

public class ErrorDetail
{
    [JsonPropertyName("domain")]
    public string Domain { get; set; }

    [JsonPropertyName("reason")]
    public string Reason { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}

