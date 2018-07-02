using System;
using System.Runtime.Serialization;

namespace Open.GoogleDrive
{
    [DataContract]
    public class File
    {
        [DataMember(Name = "kind", EmitDefaultValue=false)]
        public string Kind { get; set; }
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(Name = "mimeType", EmitDefaultValue = false)]
        public string MimeType { get; set; }
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }
        [DataMember(Name = "starred", EmitDefaultValue = false)]
        public bool Starred { get; set; }
        [DataMember(Name = "trashed", EmitDefaultValue = false)]
        public bool? Trashed { get; set; }
        [DataMember(Name = "explicitlyTrashed", EmitDefaultValue = false)]
        public bool ExplicitlyTrashed { get; set; }
        [DataMember(Name = "parents", EmitDefaultValue = false)]
        public string[] Parents { get; set; }
        [DataMember(Name = "webContentLink", EmitDefaultValue = false)]
        public string WebContentLink { get; set; }
        [DataMember(Name = "webViewLink", EmitDefaultValue = false)]
        public string WebViewLink { get; set; }
        [DataMember(Name = "iconLink", EmitDefaultValue = false)]
        public string IconLink { get; set; }
        [DataMember(Name = "thumbnailLink", EmitDefaultValue = false)]
        public string ThumbnailLink { get; set; }
        [DataMember(Name = "viewedByMe", EmitDefaultValue = false)]
        public bool ViewedByMe { get; set; }
        [DataMember(Name = "ViewedByMeTime", EmitDefaultValue = false)]
        public string ViewedByMeTime { get; set; }
        [DataMember(Name = "createdTime", EmitDefaultValue = false)]
        public string CreatedTime { get; set; }
        [DataMember(Name = "modifiedTime", EmitDefaultValue = false)]
        public string ModifiedTime { get; set; }
        [DataMember(Name = "modifiedByMeTime", EmitDefaultValue = false)]
        public string ModifiedByMeTime { get; set; }
        [DataMember(Name = "sharedWithMeTime", EmitDefaultValue = false)]
        public string SharedWithMeTime { get; set; }
        [DataMember(Name = "owners", EmitDefaultValue = false)]
        public User[] Owners { get; set; }
        [DataMember(Name = "lastModifyingUser", EmitDefaultValue = false)]
        public User LastModifyingUser { get; set; }
        [DataMember(Name = "shared", EmitDefaultValue = false)]
        public bool Shared { get; set; }
        [DataMember(Name = "ownedByMe", EmitDefaultValue = false)]
        public bool OwnedByMe { get; set; }
        [DataMember(Name = "writersCanShare", EmitDefaultValue = false)]
        public bool WritersCanShare { get; set; }
        [DataMember(Name = "permission", EmitDefaultValue = false)]
        public FilePermissions Permission { get; set; }
        [DataMember(Name = "folderColorRgb", EmitDefaultValue = false)]
        public string FolderColorRgb { get; set; }
        [DataMember(Name = "originalFilename", EmitDefaultValue = false)]
        public string OriginalFilename { get; set; }
        [DataMember(Name = "fullFileExtension", EmitDefaultValue = false)]
        public string FullFileExtension { get; set; }
        [DataMember(Name = "fileExtension", EmitDefaultValue = false)]
        public string FileExtension { get; set; }
        [DataMember(Name = "md5Checksum", EmitDefaultValue = false)]
        public string Md5Checksum { get; set; }
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public long Size { get; set; }
        [DataMember(Name = "quotaBytesUsed", EmitDefaultValue = false)]
        public long QuotaBytesUsed { get; set; }
        [DataMember(Name = "imageMediaMetadata", EmitDefaultValue = false)]
        public ImageMetadata ImageMediaMetadata { get; set; }
        [DataMember(Name = "videoMediaMetadata", EmitDefaultValue = false)]
        public VideoMetadata VideoMediaMetadata { get; set; }
        [DataMember(Name = "isAppAuthorized", EmitDefaultValue = false)]
        public bool IsAppAuthorized { get; set; }
    }

    [DataContract]
    public class VideoMetadata
    {
        [DataMember(Name = "width", EmitDefaultValue = false)]
        public int Width { get; set; }
        [DataMember(Name = "height", EmitDefaultValue = false)]
        public int Height { get; set; }
        [DataMember(Name = "durationMillis", EmitDefaultValue = false)]
        public long DurationMillis { get; set; }
    }

    [DataContract]
    public class FilePermissions
    {
        [DataMember(Name = "kind", EmitDefaultValue = false)]
        public string Kind { get; set; }
        [DataMember(Name = "etag", EmitDefaultValue = false)]
        public string Etag { get; set; }
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        [DataMember(Name = "selfLink", EmitDefaultValue = false)]
        public string SelfLink { get; set; }
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(Name = "role", EmitDefaultValue = false)]
        public string Role { get; set; }
        [DataMember(Name = "additionalRoles", EmitDefaultValue = false)]
        public string[] AdditionalRoles { get; set; }
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
        [DataMember(Name = "authKey", EmitDefaultValue = false)]
        public string AuthKey { get; set; }
        [DataMember(Name = "withLink", EmitDefaultValue = false)]
        public bool WithLink { get; set; }
        [DataMember(Name = "photoLink", EmitDefaultValue = false)]
        public string PhotoLink { get; set; }
    }
}
