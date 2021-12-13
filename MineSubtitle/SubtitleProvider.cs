using MineSubtitle.Implementation.Exceptions;
using MineSubtitle.Implementation.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MineSubtitle
{
    public class SubtitleProvider : ISubtitleProvider
    {

        protected ISubtitlesParser Parser { get; }

        public SubtitleProvider(ISubtitlesParser parser)
        {
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        protected List<SubtitleItem> Items { get; set; }


        public bool OpenFile(string filePath)
        {
            using var stream = File.Open(filePath, FileMode.Open);
            if (!stream.CanRead || !stream.CanSeek)
            {
                return false;
            }

            this.Items = this.Parser.ReadStreamItems(stream);

            return true;
        }

        public List<SubtitleItem> ReadToEnd()
        {
            return this.Items
                .Select(Items => Items.Clone())
                .ToList();
        }

        public void SetOffset(TimeSpan timeSpan)
        {
            foreach (var item in Items)
            {
                item.StartTime += timeSpan;
                item.EndTime += timeSpan;
            }
        }

        public bool SaveFile(string filePath)
        {
            var path = Path.Combine(filePath, "..");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var stream = File.Create(filePath);
            if (!stream.CanRead || !stream.CanSeek)
            {
                return false;
            }

            this.Parser.WriteStreamItems(stream, this.Items);
            stream.Flush();

            return true;
        }

        public void Dispose()
        {
            this.Items = null;
        }

        public static ISubtitleProvider CreateSubtitleProvider(SubtitleType type = SubtitleType.SubRip)
        {
            return type switch
            {
                SubtitleType.SubRip => new SubtitleProvider(new SubRipParsers()),
                _ => throw new NotFoundSubtitleTypeException(type),
            };
        }
    }
}
