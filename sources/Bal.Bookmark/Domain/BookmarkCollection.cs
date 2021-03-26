namespace Bal.Bookmark.Domain
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("Collection")]
    public class BookmarkCollection
    {
        public BookmarkCollection()
        {
            this.Bookmarks = new List<Bookmark>();
        }

        [XmlArray]
        [XmlArrayItem("Add")]
        public List<Bookmark> Bookmarks { get; set; }
    }
}
