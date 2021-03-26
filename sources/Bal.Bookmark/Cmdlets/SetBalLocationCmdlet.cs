namespace Bal.Bookmark.Cmdlets
{
    using System;
    using System.Linq;
    using System.Management.Automation;

    using Bal.Bookmark.Domain;
    using Bal.Bookmark.Helpers;

    [Alias("goto")]
    [Cmdlet(VerbsCommon.Set, "BalLocation")]
    public class SetBalLocationCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public string Alias { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                var collection = ImmediateXmlSerializer.DeserializeOrDefault<BookmarkCollection>(BookmarkProvider.GetBookmarkPath());

                var found = collection.Bookmarks.FirstOrDefault(b => string.Equals(b.Alias, this.Alias, StringComparison.OrdinalIgnoreCase));

                if (found == null)
                {
                    throw new Exception($"Alias does not exist");
                }

                this.SessionState.Path.SetLocation(found.Path);
            }
            catch (Exception e)
            {
                this.WriteError(new ErrorRecord(e, "NotFound", ErrorCategory.InvalidArgument, this.Alias));
            }
        }
    }
}
