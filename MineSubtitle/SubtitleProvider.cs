using System;
using System.Collections.Generic;

namespace MineSubtitle
{
    public class SubtitleProvider : ISubtitleProvider
    {


        public bool OpenFile(string file)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubtitleItem> ReadToEnd()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public static ISubtitleProvider CreateSubtitleProvider(SubtitleType type = SubtitleType.SubRip)
        {
            return new SubtitleProvider();
        }
    }
}
