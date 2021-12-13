using System.Collections.Generic;
using System.IO;

namespace MineSubtitle.Implementation.Parsers
{
    public interface ISubtitlesParser
    {
        /// <summary>
        /// Read SubtitleItem from Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        List<SubtitleItem> ReadStreamItems(Stream stream);

        /// <summary>
        /// Write SubtitleItem in Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="items"></param>
        void WriteStreamItems(Stream stream, List<SubtitleItem> items);
    }
}
