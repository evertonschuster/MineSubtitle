using MineSubtitle.Implementation.Exceptions;
using MineSubtitle.Implementation.Parsers.SubRip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MineSubtitle.Implementation.Parsers
{
    public class SubRipParsers : ISubtitlesParser
    {
        public List<SubtitleItem> ParseStream(Stream stream)
        {
            stream.Position = 0;
            var reader = new StreamReader(stream, Encoding.UTF8, true);
            var items = new List<SubtitleItem>();

            var srtSubtitleItems = this.ReadLines(reader);
            foreach (var strItem in srtSubtitleItems)
            {
                var item = strItem.CreateSubtitleItemFromString();
                items.Add(item);
            }

            if (!items.Any())
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

    }
}
