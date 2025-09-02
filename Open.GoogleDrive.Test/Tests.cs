using DotNetEnv;
using Open.GoogleDrive;

namespace Open.GoogleDrive.Test;

public class Tests
{
    private string _accessToken;
    private string _rootFolderId;

    [SetUp]
    public async Task Setup()
    {
        Env.Load();
        var clientId = Environment.GetEnvironmentVariable("CLIENT_ID")!;
        var refreshToken = Environment.GetEnvironmentVariable("REFRESH_TOKEN")!;
        var token = await GoogleDriveClient.RefreshAccessTokenAsync(refreshToken, clientId, "", CancellationToken.None);
        _accessToken = token.AccessToken;
        var client = new GoogleDriveClient(_accessToken);
        var rootFolderName = Guid.NewGuid().ToString();
        _rootFolderId = rootFolderName;
        await client.InsertFileAsync(new File
        {
            Name = rootFolderName,
            MimeType = "application/vnd.google-apps.folder"
        }, null, CancellationToken.None);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
