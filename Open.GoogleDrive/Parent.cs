using System.Runtime.Serialization;

namespace Open.GoogleDrive
{
    [DataContract]
    public class Parent
    {
        [DataMember(Name = "kind", EmitDefaultValue = false)]
        public string Kind { get; set; }
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        [DataMember(Name = "selfLink", EmitDefaultValue = false)]
        public string SelfLink { get; set; }
        [DataMember(Name = "parentLink", EmitDefaultValue = false)]
        public string ParentLink { get; set; }
        [DataMember(Name = "isRoot", EmitDefaultValue = false)]
        public bool IsRoot { get; set; }
    }
}
