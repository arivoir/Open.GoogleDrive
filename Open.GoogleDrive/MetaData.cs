using System.Runtime.Serialization;


namespace Open.GoogleDrive
{
    [DataContract]
    public class ImageMetadata
    {
        [DataMember(Name = "width")]
        public int Width { get; set; }
        [DataMember(Name = "height")]
        public int Height { get; set; }
        [DataMember(Name = "rotation")]
        public int Rotation { get; set; }
        [DataMember(Name = "location")]
        public Location Location { get; set; }
    }
}
