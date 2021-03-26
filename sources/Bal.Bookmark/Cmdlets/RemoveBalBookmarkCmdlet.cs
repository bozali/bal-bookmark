namespace Bal.Bookmark.Cmdlets
{
    using System.Linq;
    using System.IO;
    using System.Management.Automation;

    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    using Bal.Bookmark.Domain;
    using Bal.Bookmark.Helpers;


    [Cmdlet(VerbsCommon.Remove, "BalBookmark")]
    public class RemoveBalBookmarkCmdlet : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true, ParameterSetName = "Alias")]
        public string[] Alias { get; set; }

        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Bookmark")]
        public Bookmark[] Bookmarks { get; set; }

        protected override void ProcessRecord()
        {
            var file = new FileInfo(BookmarkProvider.GetBookmarkPath());
            string[] items = this.Alias ?? this.Bookmarks.Select(b => b.Alias).ToArray();

            var collection = ImmediateXmlSerializer.DeserializeOrDefault<BookmarkCollection>(file);

            foreach (string alias in items)
            {
                collection.Bookmarks.RemoveAll(b => LikeOperator.LikeString(alias, b.Alias, CompareMethod.Text));
            }

            ImmediateXmlSerializer.Serialize(file, collection);
        }
    }
}
