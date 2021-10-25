/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

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
