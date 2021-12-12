using System;

namespace MineSubtitle
{
    public class SubtitleItem
    {
        /// <summary>
        /// Indext item
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Start time.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// End time.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// The raw subtitle string from the file
        /// </summary>
        public string Text { get; set; }

        public SubtitleItem(int index, TimeSpan startTime, TimeSpan endTime, string text)
        {
            Index = index;
            StartTime = startTime;
            EndTime = endTime;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

    }
}
