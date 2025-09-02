using Open.Net.Http;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Open.GoogleDrive;

public class GoogleDriveMessageHandler : RetryMessageHandler
{
    private static Random _rand = new Random();

    protected override async Task<TimeSpan?> ShouldRetry(HttpResponseMessage response, int retries, CancellationToken cancellationToken)
    {
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            var error = await response.Content.ReadJsonAsync<ErrorResponse>();
            if (error.Error != null && error.Error.Errors != null)
            {
                var e = error.Error.Errors.FirstOrDefault();
                if (e != null)
                {
                    if (e.Reason == "rateLimitExceeded" || e.Reason == "userRateLimitExceeded")
                    {
                        var timeSpan = Math.Pow(2, retries) * 1000 + _rand.Next(0, 1000);
                        return TimeSpan.FromMilliseconds(timeSpan);
                    }
                }
            }
        }
        return null;
    }
}
