namespace Bal.Bookmark.Cmdlets
{
    using Bal.Bookmark.Domain;
    using Bal.Bookmark.Helpers;

    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "BalBookmark")]
    public class GetBalBookmarkCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string Alias { get; set; }

        protected override void ProcessRecord()
        {
            var serializer = new XmlSerializer(typeof(BookmarkCollection));
            BookmarkCollection collection = null;

            var file = new FileInfo(BookmarkProvider.GetBookmarkPath());

            if (!file.Exists)
            {
                var empty = Enumerable.Empty<Bookmark>();

                ImmediateXmlSerializer.Serialize(file, empty);
                this.WriteObject(empty);
                return;
            }

            collection = ImmediateXmlSerializer.DeserializeOrDefault<BookmarkCollection>(BookmarkProvider.GetBookmarkPath());

            if (!string.IsNullOrEmpty(this.Alias))
            {
                collection.Bookmarks = collection.Bookmarks.Where(b => string.Equals(b.Alias, this.Alias, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            this.WriteObject(collection.Bookmarks);
        }
    }
}
