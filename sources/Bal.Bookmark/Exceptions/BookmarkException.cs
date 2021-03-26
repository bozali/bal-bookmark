namespace Bal.Bookmark.Exceptions
{
    using System;

    public class BookmarkException : Exception
    {
        public BookmarkException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
