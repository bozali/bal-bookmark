namespace Bal.Bookmark.Cmdlets
{
    using System;
    using System.IO;

    using Bal.Bookmark.Helpers;
    using Bal.Bookmark.Domain;

    using System.Management.Automation;

    [Alias("mark")]
    [Cmdlet(VerbsCommon.Add, "BalBookmark")]
    public class AddBalBookmarkCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public string Alias { get; set; }

        [Parameter(Mandatory = false, Position = 1)]
        public string Path { get; set; }

        [Parameter]
        public SwitchParameter Remote { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Path))
                {
                    this.Path = this.SessionState.Path.CurrentFileSystemLocation.Path;
                }
                else
                {
                    var attributes = File.GetAttributes(this.Path);

                    if ((attributes & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        throw new DirectoryNotFoundException($"The provided path is not a directory.");
                    }
                }

                var bookmark = new Bookmark
                {
                    Path = this.Path,
                    Alias = this.Alias
                };

                var collection = ImmediateXmlSerializer.DeserializeOrDefault<BookmarkCollection>(BookmarkProvider.GetBookmarkPath());
                collection.Bookmarks.Add(bookmark);

                ImmediateXmlSerializer.Serialize(BookmarkProvider.GetBookmarkPath(), collection);

                this.WriteObject(bookmark);
            }
            catch (DirectoryNotFoundException e)
            {
                this.WriteError(new ErrorRecord(e, "NoDirectory", ErrorCategory.InvalidArgument, this.Path));
            }
            catch (Exception e)
            {
                this.WriteError(new ErrorRecord(e, "System", ErrorCategory.InvalidOperation, null));
            }
        }
    }
}
