using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineSubtitle.Implementation.Parsers
{
    public interface ISubtitlesParser
    {
        List<SubtitleItem> ParseStream(Stream stream);
    }
}
