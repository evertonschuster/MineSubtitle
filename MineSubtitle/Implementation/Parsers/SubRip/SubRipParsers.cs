using MineSubtitle.Implementation.Exceptions;
using MineSubtitle.Implementation.Parsers.SubRip;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineSubtitle.Implementation.Parsers
{
    public class SubRipParsers : ISubtitlesParser
    {
        #region Read file
        public List<SubtitleItem> ParseStream(Stream stream)
        {
            stream.Position = 0;
            var reader = new StreamReader(stream, true);
            var items = new List<SubtitleItem>();

            var srtSubtitleItems = this.ReadLines(reader);
            foreach (var strItem in srtSubtitleItems)
            {
                var item = strItem.FromSubRipFromString();
                items.Add(item);
            }

            if (items.Count == 0)
            {
                throw new NotContentFoundException();
            }

            return items;
        }

        private IEnumerable<string> ReadLines(StreamReader reader)
        {
            string line;
            var sb = new StringBuilder();

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                {
                    // return only if not empty
                    var res = sb.ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(res))
                    {
                        yield return res;
                    }
                    sb = new StringBuilder();
                }
                else
                {
                    sb.AppendLine(line);
                }
            }

            if (sb.Length > 0)
            {
                yield return sb.ToString();
            }
        }

        #endregion

        #region Write file
        public void WriteItems(Stream stream, List<SubtitleItem> items)
        {
            var writer = new StreamWriter(stream);

            foreach (var item in items)
            {
                var strItem = item.FromSubRipFromString();
                writer.WriteLine(strItem);
            }

            writer.Flush();
        }
        #endregion

    }
}
