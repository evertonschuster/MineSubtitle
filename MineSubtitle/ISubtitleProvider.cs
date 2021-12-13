using System;
using System.Collections.Generic;

namespace MineSubtitle
{
    public interface ISubtitleProvider : IDisposable
    {
        /// <summary>
        /// Load subtitle file into memory 
        /// </summary>
        /// <param name="filePath">Subtitle file path</param>
        /// <returns>Success open Subtitle file</returns>
        bool OpenFile(string filePath);

        /// <summary>
        /// Reads the subtitle file to the end and returns a subtitle item list
        /// </summary>
        /// <returns>Subtitle item list</returns>
        List<SubtitleItem> ReadToEnd();

        /// <summary>
        /// Applies time shift (offset) to all timecodes of a subtitle
        /// </summary>
        /// <param name="timeSpan"></param>
        void SetOffset(TimeSpan timeSpan);

        /// <summary>
        /// Save subtitle file to disk
        /// </summary>
        /// <param name="filePath">Subtitle file path</param>
        /// <returns>Success save Subtitle file</returns>
        bool SaveFile(string filePath);
    }
}
