namespace Bal.Bookmark.Domain
{
    using System.Xml.Serialization;

    public class Bookmark
    {
        [XmlAttribute]
        public string Alias { get; set; }

        [XmlAttribute]
        public string Path { get; set; }
    }
}
