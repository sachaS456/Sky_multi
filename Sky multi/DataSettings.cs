using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky_multi
{
    public class DataSettings
    {
        public bool CliqueMadiaPause { get; set; } = false;
        public bool MinimizeWindowsMediaPause { get; set; } = false;
        public bool VideoPreviewOnProgressBar { get; set; } = true;
        public bool VideoPreviewOnSetTime { get; set; } = true;
        public bool UsingDefinitionMax { get; set; } = false;
        public bool WhenVideoSetFullScreen { get; set; } = false;
        public EndMediaAction EndMedia { get; set; } = EndMediaAction.ReadMultimediaNext;
        public Language Language { get; set; }

        public DataSettings()
        {
            if (System.Globalization.CultureInfo.CurrentCulture.Name == "fr-FR")
            {
                Language = Language.French;
            }
            else
            {
                Language = Language.English;
            }
        }
    }

    public enum EndMediaAction
    {
         ReadMultimediaNext,
         RestartMultimedia,
         DoNothing
    }

    public enum Language
    {
        French,
        English
    }
}
