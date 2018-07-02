using Open.Google;
using Open.IO;
using Open.Net.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Open.GoogleDrive
{
    public class GoogleDriveClient : GoogleClient
    {
        #region ** fields

        private string _oauth2Token;
        private static readonly string ApiServiceUri = "https://www.googleapis.com/drive/v3/";
        private static readonly string ApiServiceUploadUri = "https://www.googleapis.com/upload/drive/v3/";

        #endregion

        #region ** initialization

        public GoogleDriveClient(string oauth2Token)
        {
            _oauth2Token = oauth2Token;
        }

        #endregion

        #region ** public methods

        public async Task<Response> GetFilesAsync(string q = null, string fields = null, int? maxResults = null, string pageToken = null, string orderBy = null, string spaces = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var uri = BuildUri(ApiServiceUri + "files", q, fields, maxResults, pageToken, orderBy, spaces);
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Response>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<File> GetFileAsync(string fileId, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var uri = BuildUri(GetFileUri(fileId).AbsoluteUri, fields: fields);
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<File>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Stream> DownloadFileAsync(string fileId, CancellationToken cancellationToken)
        {
            var client = GetClient();
            var uri = BuildUri(GetFileUri(fileId).AbsoluteUri, alt: "media");
            var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                return new StreamWithLength(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentLength);
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<File> InsertFileAsync(File file, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var uri = BuildUri(ApiServiceUri + "files", fields: fields);
            var content = new StringContent(file.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<File>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<File> UploadMultipartFileAsync(File file, Stream fileStream, IProgress<StreamProgress> progress, string fields, CancellationToken cancellationToken)
        {
            var uri = BuildUri(ApiServiceUploadUri + "files", fields: fields, uploadType: "multipart");
            var client = GetClient();
            var text = file.SerializeJson<File>();
            var content = new MultipartContent("related");
            var textContent = new StringContent(text);
            textContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Add(textContent);
            var fileContent2 = new StreamedContent(fileStream, progress, cancellationToken);
            fileContent2.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "file", FileName = file.Name };
            content.Add(fileContent2);
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<File>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Uri> UploadResumableFileAsync(File file, long fileLength, string fields, CancellationToken cancellationToken)
        {
            var uri = BuildUri(ApiServiceUploadUri + "files", fields: fields, uploadType: "resumable");
            var client = GetClient();
            client.DefaultRequestHeaders.Add("X-Upload-Content-Type", file.MimeType);
            client.DefaultRequestHeaders.Add("X-Upload-Content-Length", fileLength.ToString());
            var content = new StringContent(file.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return response.Headers.Location;
            }
            else
            {
                throw await ProcessException(response);
            }
        }


        public async Task<Tuple<File, ContentRangeHeaderValue>> SendResumableFileAsync(Uri sessionUri, string contentType, StreamPartition fileStream, long from, long to, long length, IProgress<StreamProgress> progress, CancellationToken cancellationToken)
        {
            var client = GetClient();
            var content = new StreamedContent(fileStream, progress, cancellationToken);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            if (from > 0 || to < length - 1)
                content.Headers.ContentRange = new ContentRangeHeaderValue(from, to, length);
            var response = await client.PutAsync(sessionUri, content, cancellationToken);
            return await ProcessResumableUploadResponse(response);
        }

        public async Task<Tuple<File, ContentRangeHeaderValue>> GetResumableFileRangeAsync(Uri sessionUri, long fileLength, CancellationToken cancellationToken)
        {
            var client = GetClient();
            var content = HttpClientEx.GetEmptyContent();
            content.Headers.ContentRange = new ContentRangeHeaderValue(fileLength);
            var response = await client.PutAsync(sessionUri, content, cancellationToken);
            return await ProcessResumableUploadResponse(response);
        }

        private async Task<Tuple<File, ContentRangeHeaderValue>> ProcessResumableUploadResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new Tuple<File, ContentRangeHeaderValue>(await response.Content.ReadJsonAsync<File>(), null);
            }
            if ((int)response.StatusCode == 308)
            {
                var range = response.Headers.GetValues("Range").First();
                var r = ContentRangeHeaderValue.Parse("bytes " + range.Substring(6) + "/*");
                return new Tuple<File, ContentRangeHeaderValue>(null, r);
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<File> UpdateFileAsync(string fileId, File file = null, string fields = null, string addParents = null, string removeParents = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var uri = BuildUri(GetFileUri(fileId).AbsoluteUri, fields: fields, addParents: addParents, removeParents: removeParents);
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
            var content = new StringContent((file ?? new File()).SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await client.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<File>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<File> CopyFileAsync(string fileId, File file, string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var uri = BuildUri(GetCopyFileUri(fileId).AbsoluteUri, fields: fields);
            var content = new StringContent(file.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<File>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task DeleteFileAsync(string fileId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var uri = GetFileUri(fileId);
            var response = await client.DeleteAsync(uri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response);
            }
        }

        public async Task<About> GetAbout(string fields = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var uri = BuildUri(ApiServiceUri + "about", fields: fields);
            var response = await client.GetAsync(uri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<About>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        #endregion

        #region ** private stuff

        private Uri GetFileUri(string fileId)
        {
            return new Uri(string.Format(ApiServiceUri + "files/{0}", fileId), UriKind.Absolute);
        }

        private Uri GetCopyFileUri(string fileId)
        {
            return new Uri(string.Format(ApiServiceUri + "files/{0}/copy", fileId), UriKind.Absolute);
        }

        public new HttpClient GetClient()
        {
            HttpClient client;
            client = new HttpClient(new GoogleDriveMessageHandler());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("AuthSub", "token=" + _oauth2Token);
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }

        public static Uri BuildUri(string uri, string q = null, string fields = null, int? maxResults = null, string pageToken = null, string orderBy = null, string spaces = null, string alt = null, string uploadType = null, string addParents = null, string removeParents = null)
        {
            var builder = new UriBuilder(uri);
            var query = builder.Query ?? "";
            if (query.StartsWith("?", StringComparison.CurrentCulture))
                query = query.Substring(1);
            if (!string.IsNullOrWhiteSpace(q))
                query += "&q=" + EscapeUriString(q);
            if (!string.IsNullOrWhiteSpace(spaces))
                query += "&spaces=" + spaces;
            if (!string.IsNullOrWhiteSpace(fields))
                query += "&fields=" + fields;
            if (!string.IsNullOrEmpty(orderBy))
                query += "&orderBy=" + orderBy;
            if (maxResults.HasValue)
                query += "&pageSize=" + maxResults.Value.ToString();
            if (!string.IsNullOrWhiteSpace(pageToken))
                query += "&pageToken=" + pageToken;
            if (!string.IsNullOrWhiteSpace(alt))
                query += "&alt=" + alt;
            if (!string.IsNullOrWhiteSpace(uploadType))
                query += "&uploadType=" + uploadType;
            if (!string.IsNullOrWhiteSpace(addParents))
                query += "&addParents=" + addParents;
            if (!string.IsNullOrWhiteSpace(removeParents))
                query += "&removeParents=" + removeParents;
            builder.Query = query;
            return builder.Uri;
        }

        private static string EscapeUriString(string text)
        {
            var builder = new StringBuilder(Uri.EscapeDataString(text));
            builder.Replace("!", "%21");
            builder.Replace("'", "%27");
            builder.Replace("(", "%28");
            builder.Replace(")", "%29");
            return builder.ToString();
        }

        private async Task<Exception> ProcessException(HttpResponseMessage response)
        {
            var error = await response.Content.TryReadJsonAsync<ErrorResponse>();
            return error != null ? new GoogleDriveException(response.StatusCode, error.Error) : new GoogleDriveException(response.StatusCode);
        }

        #endregion
    }
}
