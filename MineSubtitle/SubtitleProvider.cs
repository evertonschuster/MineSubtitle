using MineSubtitle.Implementation.Exceptions;
using MineSubtitle.Implementation.Parsers;
using System;
using System.Collections.Generic;
using System.IO;

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

            this.Items = this.Parser.ParseStream(stream);

            return true;
        }

        public List<SubtitleItem> ReadToEnd()
        {
            return Items;
        }

        public void SetOffset(TimeSpan timeSpan)
        {
            foreach (var item in Items)
            {
                item.StartTime += timeSpan;
                item.EndTime += timeSpan;
            }
        }

        public bool Save(string file)
        {
            throw new NotImplementedException();
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
