using System;

namespace MineSubtitle.Implementation.Exceptions
{
    internal class NotContentFoundException : Exception
    {
        public NotContentFoundException() : base("File without content")
        {
        }
    }
}
