using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit;

namespace MineSubtitle.Tests
{
    public class MineSubtitleTest
    {
        const string CaseChildrenOfMen = "Files/Children.of.Men.2006.DVD5.720p.HDDVD.x264-REVEiLLE.srt";
        const string CaseFastAndFurious = "Files/Fast.and.Furious.F9.The.Fast.Saga.2021.1080p.WEBRip.x265.srt";

        public MineSubtitleTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
        }


        [Theory]
        [InlineData(CaseChildrenOfMen)]
        [InlineData(CaseFastAndFurious)]
        public void OpenSubtitleFile_Success(string path)
        {
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile(path);
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");


            var subtitles = provider.ReadToEnd();
            Assert.NotEmpty(subtitles);


            provider.Dispose();
        }

        [Theory]
        [InlineData(CaseChildrenOfMen)]
        [InlineData(CaseFastAndFurious)]
        public void SetOffset_Success(string path)
        {
            var offset = new TimeSpan(0, 0, 2);
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile(path);
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

        [Theory]
        [InlineData(CaseChildrenOfMen)]
        [InlineData(CaseFastAndFurious)]
        public void OpenAndSaveSubtitleFileWithOutReadToEnd_Success(string path)
        {
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(path), $"{DateTimeOffset.UtcNow.ToString("dd-MMMM-yy-HH-mm-ss-ff")}-{Path.GetFileName(path)}");
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile(path);
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");
            var canSave = provider.SaveFile(fileName);
            Assert.True(canSave, "Não foi possível salvar o arquivo de legenda");


            provider.Dispose();
        }

        [Theory]
        [InlineData(CaseChildrenOfMen)]
        [InlineData(CaseFastAndFurious)]
        public void OpenAndSaveSubtitleFileWithReadToEnd_Success(string path)
        {
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(path), $"{DateTimeOffset.UtcNow.ToString("dd-MMMM-yy-HH-mm-ss-ff")}-{Path.GetFileName(path)}");
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile(path);
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");


            var subtitles = provider.ReadToEnd();
            Assert.NotEmpty(subtitles);

            var canSave = provider.SaveFile(fileName);
            Assert.True(canSave, "Não foi possível salvar o arquivo de legenda");

            provider.Dispose();
        }

        [Theory]
        [InlineData(CaseChildrenOfMen)]
        [InlineData(CaseFastAndFurious)]
        public void OpenAndSaveSubtitleFileWithOffset_Success(string path)
        {
            var offset = new TimeSpan(0, 0, 2);
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(path), $"{DateTimeOffset.UtcNow.ToString("dd-MMMM-yy-HH-mm-ss-ff")}-{Path.GetFileName(path)}");
            var provider = SubtitleProvider.CreateSubtitleProvider();

            var canOpen = provider.OpenFile(path);
            Assert.True(canOpen, "Não foi possível localizar o arquivo de legenda");


            var subtitles = provider.ReadToEnd();
            Assert.NotEmpty(subtitles);
            provider.SetOffset(offset);
            var canSave = provider.SaveFile(fileName);
            Assert.True(canSave, "Não foi possível salvar o arquivo de legenda");


            canOpen = provider.OpenFile(fileName);
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
