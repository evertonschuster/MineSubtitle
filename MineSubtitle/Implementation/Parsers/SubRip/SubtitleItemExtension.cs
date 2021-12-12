using System;
using System.Linq;

namespace MineSubtitle.Implementation.Parsers.SubRip
{
    public static class SubtitleItemExtension
    {
        private static readonly string[] timeDelemiter = { "-->" };

        public static SubtitleItem FromSubRipFromString(this String line)
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

            var textLines = lines.GetRange(2, lines.Count - 2);
            var text = string.Join("\n", textLines);

            return new SubtitleItem(index, startTime, endTime, text);
        }
    }
}
