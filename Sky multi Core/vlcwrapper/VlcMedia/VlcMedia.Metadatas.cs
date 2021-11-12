using System;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcMedia : IDisposable
    {
        public string Title
        {
            get { return GetMetaData(MediaMetadatas.Title); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Title, value); }
        }

        public string Artist
        {
            get { return GetMetaData(MediaMetadatas.Artist); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Artist, value); }
        }

        public string Genre
        {
            get { return GetMetaData(MediaMetadatas.Genre); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Genre, value); }
        }

        public string Copyright
        {
            get { return GetMetaData(MediaMetadatas.Copyright); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Copyright, value); }
        }

        public string Album
        {
            get { return GetMetaData(MediaMetadatas.Album); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Album, value); }
        }

        public string TrackNumber
        {
            get { return GetMetaData(MediaMetadatas.TrackNumber); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.TrackNumber, value); }
        }

        public string Description
        {
            get { return GetMetaData(MediaMetadatas.Description); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Description, value); }
        }

        public string Rating
        {
            get { return GetMetaData(MediaMetadatas.Rating); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Rating, value); }
        }

        public string Date
        {
            get { return GetMetaData(MediaMetadatas.Date); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Date, value); }
        }

        public string Setting
        {
            get { return GetMetaData(MediaMetadatas.Setting); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Setting, value); }
        }

        public string URL
        {
            get { return GetMetaData(MediaMetadatas.URL); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.URL, value); }
        }

        public string Language
        {
            get { return GetMetaData(MediaMetadatas.Language); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Language, value); }
        }

        public string NowPlaying
        {
            get { return GetMetaData(MediaMetadatas.NowPlaying); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.NowPlaying, value); }
        }

        public string Publisher
        {
            get { return GetMetaData(MediaMetadatas.Publisher); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.Publisher, value); }
        }

        public string EncodedBy
        {
            get { return GetMetaData(MediaMetadatas.EncodedBy); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.EncodedBy, value); }
        }

        public string ArtworkURL
        {
            get { return GetMetaData(MediaMetadatas.ArtworkURL); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.ArtworkURL, value); }
        }

        public string TrackID
        {
            get { return GetMetaData(MediaMetadatas.TrackID); }
            set { myVlcMediaPlayer.SetMediaMeta(MediaInstance, MediaMetadatas.TrackID, value); }
        }

        public void Parse()
        {
            myVlcMediaPlayer.ParseMedia(MediaInstance);
        }

        public void ParseAsync()
        {
            myVlcMediaPlayer.ParseMediaAsync(MediaInstance);
        }

        private string GetMetaData(MediaMetadatas metadata)
        {
            if (MediaInstance == IntPtr.Zero)
                return null;
            if (myVlcMediaPlayer.IsParsedMedia(MediaInstance))
                myVlcMediaPlayer.ParseMedia(MediaInstance);
            return myVlcMediaPlayer.GetMediaMeta(MediaInstance, metadata);
        }
    }
}