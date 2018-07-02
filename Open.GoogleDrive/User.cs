using System.Runtime.Serialization;

namespace Open.GoogleDrive
{
    [DataContract]
    public class About
    {
        [DataMember(Name = "user")]
        public User User { get; set; }
        [DataMember(Name = "storageQuota")]
        public StorageQuota StorageQuota { get; set; }
        [DataMember(Name = "maxUploadSize")]
        public long MaxUploadSize { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "picture")]
        public Picture Picture { get; set; }
        [DataMember(Name = "isAuthenticatedUser")]
        public bool IsAuthenticatedUser { get; set; }
        [DataMember(Name = "permissionId")]
        public string PermissionId { get; set; }
    }

    [DataContract]
    public class Picture
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    public class StorageQuota
    {

        [DataMember(Name = "limit")]
        public long Limit { get; set; }
        [DataMember(Name = "usage")]
        public long Usage { get; set; }
        [DataMember(Name = "usageInDrive")]
        public long UsageInDrive { get; set; }
        [DataMember(Name = "usageInDriveTrash")]
        public long UsageInDriveTrash { get; set; }
    }
}
