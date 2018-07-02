using System;
using System.Net;
using System.Net.Http.Headers;

namespace Open.GoogleDrive
{
    public class GoogleDriveException : Exception
    {

        internal GoogleDriveException(HttpStatusCode statusCode)
            : base(statusCode.ToString())
        {
            StatusCode = statusCode;
        }

        internal GoogleDriveException(HttpStatusCode statusCode, Error error)
            : base(error.Message)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public GoogleDriveException(HttpStatusCode statusCode, ContentRangeHeaderValue range) : this(statusCode)
        {
            Range = range;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public ContentRangeHeaderValue Range { get; private set; }
        public Error Error { get; private set; }
    }
}
