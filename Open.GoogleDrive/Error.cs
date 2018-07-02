using System.Runtime.Serialization;

namespace Open.GoogleDrive
{
    [DataContract]
    public class ErrorResponse
    {
        [DataMember(Name = "error")]
        public Error Error { get; set; }
    }

    [DataContract]
    public class Error
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "errors")]
        public ErrorDetail[] Errors { get; set; }
    }

    [DataContract]
    public class ErrorDetail
    {
        [DataMember(Name = "domain")]
        public string Domain { get; set; }
        [DataMember(Name = "reason")]
        public string Reason { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
