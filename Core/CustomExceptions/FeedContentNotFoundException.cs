using System;

namespace Core.CustomExceptions
{
    public class FeedContentNotFoundException : Exception
    {
        public string Url { get; set; }

        public FeedContentNotFoundException()
        {

        }

        public FeedContentNotFoundException(string message, string url)
            :base(message)
        {
            Url = url;
        }

        public FeedContentNotFoundException(string message, Exception inner)
            :base(message, inner)
        {

        }
    }
}
