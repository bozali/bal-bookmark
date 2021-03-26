namespace Bal.Bookmark.Domain
{
    using System;
    using System.IO;

    public static class BookmarkProvider
    {
        public static string GetBookmarkPath()
        {
            string profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = Path.Combine(profilePath, "Bookmark", "Bookmark.xml");

            return filePath;
        }
    }
}
