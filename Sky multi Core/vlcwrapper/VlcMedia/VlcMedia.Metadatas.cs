/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcMedia : IDisposable
    {
        public string Title
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Title);
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.Title, value);
            }
        }

        public string Artist
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Artist); 
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.Artist, value);
            }
        }

        public string Genre
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Genre);
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.Genre, value);
            }
        }

        public string Copyright
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Copyright);
            }
            set
            {
                SetMediaMeta(MediaMetadatas.Copyright, value); 
            }
        }

        public string Album
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Album);
            }
            set
            {
                SetMediaMeta(MediaMetadatas.Album, value);
            }
        }

        public string TrackNumber
        {
            get 
            {
                return GetMetaData(MediaMetadatas.TrackNumber); 
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.TrackNumber, value); 
            }
        }

        public string Description
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Description);
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.Description, value); 
            }
        }

        public string Rating
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Rating);
            }
            set
            {
                SetMediaMeta(MediaMetadatas.Rating, value);
            }
        }

        public string Date
        {
            get 
            {
                return GetMetaData(MediaMetadatas.Date); 
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.Date, value); 
            }
        }

        public string Setting
        {
            get
            {
                return GetMetaData(MediaMetadatas.Setting); 
            }
            set
            {
                SetMediaMeta(MediaMetadatas.Setting, value);
            }
        }

        public string URL
        {
            get
            { 
                return GetMetaData(MediaMetadatas.URL);
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.URL, value); 
            }
        }

        public string Language
        {
            get
            {
                return GetMetaData(MediaMetadatas.Language); 
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.Language, value);
            }
        }

        public string NowPlaying
        {
            get
            {
                return GetMetaData(MediaMetadatas.NowPlaying); 
            }
            set
            {
                SetMediaMeta(MediaMetadatas.NowPlaying, value);
            }
        }

        public string Publisher
        {
            get 
            { 
                return GetMetaData(MediaMetadatas.Publisher); 
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.Publisher, value); 
            }
        }

        public string EncodedBy
        {
            get
            {
                return GetMetaData(MediaMetadatas.EncodedBy);
            }
            set
            {
                SetMediaMeta(MediaMetadatas.EncodedBy, value); 
            }
        }

        public string ArtworkURL
        {
            get 
            {
                return GetMetaData(MediaMetadatas.ArtworkURL);
            }
            set
            {
                SetMediaMeta(MediaMetadatas.ArtworkURL, value); 
            }
        }

        public string TrackID
        {
            get 
            {
                return GetMetaData(MediaMetadatas.TrackID); 
            }
            set 
            {
                SetMediaMeta(MediaMetadatas.TrackID, value); 
            }
        }

        public void Parse()
        {
            MediaInstanceIsLoad();
            VlcNative.libvlc_media_parse(MediaInstance);
        }

        public void ParseAsync()
        {
            MediaInstanceIsLoad();
            VlcNative.libvlc_media_parse_async(MediaInstance);
        }

        private string GetMetaData(MediaMetadatas metadata)
        {
            if (MediaInstance == IntPtr.Zero)
            {
                return null;
            }

            MediaInstanceIsLoad();

            if (VlcNative.libvlc_media_is_parsed(MediaInstance) == 1)
            {
                VlcNative.libvlc_media_parse(MediaInstance);
            }

            return Utf8InteropStringConverter.Utf8InteropToString(VlcNative.libvlc_media_get_meta(MediaInstance, metadata));
        }

        private void SetMediaMeta(in MediaMetadatas metadata, in string value)
        {
            MediaInstanceIsLoad();
            using (Utf8StringHandle handle = Utf8InteropStringConverter.ToUtf8StringHandle(value))
            {
                VlcNative.libvlc_media_set_meta(MediaInstance, metadata, handle);
            }
        }
    }
}