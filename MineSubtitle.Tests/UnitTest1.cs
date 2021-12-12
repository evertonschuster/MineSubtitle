using System;
using System.Linq;
using Xunit;

namespace MineSubtitle.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void OpenSubtitleFile_Success()
        {
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile("Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE.srt");
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");


            var subtitles = provider.ReadToEnd();
            Assert.NotEmpty(subtitles);


            provider.Dispose();
        }

        [Fact]
        public void SetOffset_Success()
        {
            var offset = new TimeSpan(0, 0, 2);
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile("Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE.srt");
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");

            var subtitles = provider.ReadToEnd();
            Assert.NotEmpty(subtitles);

            provider.SetOffset(offset);
            var subtitlesWithOffset = provider.ReadToEnd();
            Assert.NotEmpty(subtitlesWithOffset);
            Assert.Equal(subtitles.Count(), subtitlesWithOffset.Count());


            for (int index = 0; index < subtitles.Count(); index++)
            {
                var item = subtitles.ElementAt(index);
                var itemOffSet = subtitlesWithOffset.ElementAt(index);

                Assert.Equal(item.StartTime.Add(offset), itemOffSet.StartTime);
                Assert.Equal(item.EndTime.Add(offset), itemOffSet.EndTime);
            }

            provider.Dispose();
        }

        [Fact]
        public void OpenAndSaveSubtitleFileWithOutReadToEnd_Success()
        {
            var fileName = $"Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE{DateTimeOffset.UtcNow}.srt";
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile("Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE.srt");
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");
            var canSave = provider.Save(fileName);
            Assert.True(canSave, "Não foi possível salvar o arquivo de legenda");


            provider.Dispose();
        }

        [Fact]
        public void OpenAndSaveSubtitleFileWithReadToEnd_Success()
        {
            var fileName = $"Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE{DateTimeOffset.UtcNow}.srt";
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile("Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE.srt");
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");


            var subtitles = provider.ReadToEnd();
            Assert.NotEmpty(subtitles);

            var canSave = provider.Save(fileName);
            Assert.True(canSave, "Não foi possível salvar o arquivo de legenda");

            provider.Dispose();
        }

        [Fact]
        public void OpenAndSaveSubtitleFileWithOffset_Success()
        {
            var offset = new TimeSpan(0, 0, 2);
            var fileName = $"Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE{DateTimeOffset.UtcNow}.srt";
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile("Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE.srt");
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");


            var subtitles = provider.ReadToEnd();
            Assert.NotEmpty(subtitles);
            provider.SetOffset(offset);
            var canSave = provider.Save(fileName);
            Assert.True(canSave, "Não foi possível salvar o arquivo de legenda");


            canOpen = provider.OpenFile("Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE.srt");
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");
            var subtitlesWithOffset = provider.ReadToEnd();

            for (int index = 0; index < subtitles.Count(); index++)
            {
                var item = subtitles.ElementAt(index);
                var itemOffSet = subtitlesWithOffset.ElementAt(index);

                Assert.Equal(item.StartTime.Add(offset), itemOffSet.StartTime);
                Assert.Equal(item.EndTime.Add(offset), itemOffSet.EndTime);
            }

            provider.Dispose();
        }
    }
}
