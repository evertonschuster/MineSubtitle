using MineSubtitle.Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MineSubtitle.Implementation.Parsers
{
    public class SubRipParsers : ISubtitlesParser
    {
        private static readonly string[] timeDelemiter = { "-->" };


        #region Read file

        public List<SubtitleItem> ReadStreamItems(Stream stream)
        {
            stream.Position = 0;
            var reader = new StreamReader(stream, true);
            var items = new List<SubtitleItem>();

            var srtSubtitleItems = this.ReadStringSubtitleItems(reader);
            foreach (var strItem in srtSubtitleItems)
            {
                var item = this.ParceSubtitleItemFromString(strItem);
                items.Add(item);
            }

            if (items.Count == 0)
            {
                throw new NotContentFoundException();
            }

            return items;
        }

        private IEnumerable<string> ReadStringSubtitleItems(StreamReader reader)
        {
            string line;
            var stringItem = new StringBuilder();

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                {
                    // return only if not empty
                    var res = stringItem.ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(res))
                    {
                        yield return res;
                    }
                    stringItem = new StringBuilder();
                }
                else
                {
                    stringItem.AppendLine(line);
                }
            }

            if (stringItem.Length > 0)
            {
                yield return stringItem.ToString();
            }
        }

        private SubtitleItem ParceSubtitleItemFromString(string line)
        {
            var lines = line.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                            .Select(s => s.Trim())
                            .Where(l => !string.IsNullOrEmpty(l))
                            .ToList();

            var index = int.Parse(lines[0]);

            var timeLine = lines[1];
            var partsParts = timeLine.Split(timeDelemiter, StringSplitOptions.None);
            var startTime = TimeSpan.Parse(partsParts[0]);
            var endTime = TimeSpan.Parse(partsParts[1]);

            //read to the remaining lines
            var textLines = lines.GetRange(2, lines.Count - 2);
            var text = string.Join("\n", textLines);

            return new SubtitleItem(index, startTime, endTime, text);
        }

        #endregion

        #region Write file

        public void WriteStreamItems(Stream stream, List<SubtitleItem> items)
        {
            var writer = new StreamWriter(stream);

            foreach (var item in items)
            {
                var strItem = this.ParceSubtitleItemToString(item);
                writer.WriteLine(strItem);
            }

            writer.Flush();
        }

        public string ParceSubtitleItemToString(SubtitleItem item)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(item.Index.ToString());

            var timeSpanFormat = @"hh\:mm\:ss\.fff";

            var timeLine = $"{item.StartTime.ToString(timeSpanFormat)} {timeDelemiter[0]} {item.EndTime.ToString(timeSpanFormat)}";
            stringBuilder.AppendLine(timeLine);

            var textLines = item.Text.Split("\n");
            foreach (var textLine in textLines)
            {
                stringBuilder.AppendLine($" {textLine}");
            }

            return stringBuilder.ToString();
        }
        #endregion

    }
}
