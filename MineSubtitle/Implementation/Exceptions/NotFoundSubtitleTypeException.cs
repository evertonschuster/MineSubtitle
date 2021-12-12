using System;

namespace MineSubtitle.Implementation.Exceptions
{
    internal class NotFoundSubtitleTypeException : Exception
    {
        public NotFoundSubtitleTypeException(SubtitleType type) : base($"Not found parcer for {type}")
        {
        }
    }
}
