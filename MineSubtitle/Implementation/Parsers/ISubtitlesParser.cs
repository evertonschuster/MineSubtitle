using System.Collections.Generic;
using System.IO;

namespace MineSubtitle.Implementation.Parsers
{
    public interface ISubtitlesParser
    {
        List<SubtitleItem> ParseStream(Stream stream);

        void WriteItems(Stream stream, List<SubtitleItem> items);
    }
}
