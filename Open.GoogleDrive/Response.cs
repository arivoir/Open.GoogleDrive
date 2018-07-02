using System.Runtime.Serialization;

namespace Open.GoogleDrive
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "kind")]
        public string Kind { get; set; }
        [DataMember(Name = "nextPageToken")]
        public string NextPageToken { get; set; }
        [DataMember(Name = "files")]
        public File[] Items { get; set; }
    }
}
