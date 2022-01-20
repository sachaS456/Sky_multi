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
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Sky_multi_Core.VlcWrapper;
using Sky_multi_Core.VlcWrapper.Core;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Sky_multi_Viewer
{
    public class VideoView: Control, ISupportInitialize
    {
        public VlcMediaPlayer VlcMediaPlayer { get; private set; }
        public bool ControlLoaded { get; private set; } = false;

        public VideoView()
        {
            BackColor = Color.Black;
        }

        public HardwareAccelerationType HardwareAcceleration
        {
            get
            {
                return VlcMediaPlayer.HardwareAcceleration;
            }
            set
            {
                WaitSetHardwareAcceleration(value);
            }
        }

        public async void WaitSetHardwareAcceleration(HardwareAccelerationType value)
        {
            while (ControlLoaded == false)
            {
                await Task.Delay(10);
            }

            VlcMediaPlayer.HardwareAcceleration = value;
        }

        public void BeginInit()
        {
            
        }

        private async void LoadingVlcInstance()
        {
            Brush brush = Brushes.White;

            while (ControlLoaded == false)
            {
                int sizeElipse1 = 0;
                bool sizeElipse1D = false;
                int sizeElipse2 = 0;
                bool sizeElipse2D = false;
                int sizeElipse3 = 0;
                bool sizeElipse3D = false;

                while (true)
                {
                    this.CreateGraphics().Clear(Color.Black);

                    if (sizeElipse1 < 50 && sizeElipse1D == false)
                    {
                        sizeElipse1 += 2;
                        this.CreateGraphics().FillEllipse(brush, this.Width / 2 - 100 - sizeElipse1 / 2, this.Height / 2 - sizeElipse1 / 2, sizeElipse1, sizeElipse1);
                    }
                    else if (sizeElipse1 > 0)
                    {
                        sizeElipse1D = true;
                        sizeElipse1 -= 2;
                        this.CreateGraphics().FillEllipse(brush, this.Width / 2 - 100 - sizeElipse1 / 2, this.Height / 2 - sizeElipse1 / 2, sizeElipse1, sizeElipse1);
                    }

                    if (sizeElipse1 > 15 || sizeElipse1D == true)
                    {
                        if (sizeElipse2 < 50 && sizeElipse2D == false)
                        {
                            sizeElipse2 += 2;
                            this.CreateGraphics().FillEllipse(brush, this.Width / 2 - 25 - sizeElipse2 / 2, this.Height / 2 - sizeElipse2 / 2, sizeElipse2, sizeElipse2);
                        }
                        else if (sizeElipse2 > 0)
                        {
                            sizeElipse2D = true;
                            sizeElipse2 -= 2;
                            this.CreateGraphics().FillEllipse(brush, this.Width / 2 - 25 - sizeElipse2 / 2, this.Height / 2 - sizeElipse2 / 2, sizeElipse2, sizeElipse2);
                        }
                    }

                    if (sizeElipse2 > 15 || sizeElipse2D == true)
                    {
                        if (sizeElipse3 < 50 && sizeElipse3D == false)
                        {
                            sizeElipse3 += 2;
                            this.CreateGraphics().FillEllipse(brush, this.Width / 2 + 50 - sizeElipse3 / 2, this.Height / 2 - sizeElipse3 / 2, sizeElipse3, sizeElipse3);
                        }
                        else if (sizeElipse3 > 0)
                        {
                            sizeElipse3D = true;
                            sizeElipse3 -= 2;
                            this.CreateGraphics().FillEllipse(brush, this.Width / 2 + 50 - sizeElipse3 / 2, this.Height / 2 - sizeElipse3 / 2, sizeElipse3, sizeElipse3);
                        }
                        else
                        {
                            break;
                        }
                    }

                    await Task.Delay(16);
                }

                await Task.Delay(250);
            }
        }

        public async void EndInit()
        {
            if (IsInDesignMode || VlcMediaPlayer != null)
            {
                ControlLoaded = true;
                return;
            }

            LoadingVlcInstance();

            VlcLibDirectory = OnVlcLibDirectoryNeeded();

            //await Task.Run(() => LoadMediaPlayer());
            Thread thread = new Thread(LoadMediaPlayer);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            while (ControlLoaded == false)
            {
                await Task.Delay(10);
            }

            if (this.log != null)
            {
                this.RegisterLogging();
            }

            RegisterEvents();

            VlcMediaPlayer.VideoHostControlHandle = Handle;

            this.Video.IsKeyInputEnabled = false;
            this.Video.IsMouseInputEnabled = false;
            this.Audio.Volume = 100;
        }

        private void LoadMediaPlayer()
        {
            if (_vlcMediaPlayerOptions == null)
            {
                VlcMediaPlayer = new VlcMediaPlayer(_vlcLibDirectory, new string[0]);
            }
            else
            {
                VlcMediaPlayer = new VlcMediaPlayer(_vlcLibDirectory, _vlcMediaPlayerOptions);
            }

            ControlLoaded = true;
        }

        public event EventHandler<VlcLibDirectoryNeededEventArgs> VlcLibDirectoryNeeded;

        public DirectoryInfo OnVlcLibDirectoryNeeded()
        {
            var del = VlcLibDirectoryNeeded;
            if (del != null)
            {
                var args = new VlcLibDirectoryNeededEventArgs();
                del(this, args);
                return args.VlcLibDirectory;
            }
            return null;
        }

        private string[] _vlcMediaPlayerOptions = null;

        [Category("Media Player")]
        public string[] VlcMediaplayerOptions
        {
            get 
            { 
                return this._vlcMediaPlayerOptions; 
            }
            set
            {
                if (!(VlcMediaPlayer is null))
                {
                    throw new InvalidOperationException("Cannot modify VlcMediaplayerOptions if Media player has already been initialized. Modify VlcMediaplayerOptions before calling EndInit.");
                }

                this._vlcMediaPlayerOptions = value;
            }
        }

        private DirectoryInfo _vlcLibDirectory = null;
        [Category("Media Player")]
        public DirectoryInfo VlcLibDirectory
        {
            get { return this._vlcLibDirectory; }
            set
            {
                if (!(VlcMediaPlayer is null))
                {
                    throw new InvalidOperationException("Cannot modify VlcLibDirectory if Media player has already been initialized. Modify VlcLibDirectory before calling EndInit.");
                }

                this._vlcLibDirectory = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool IsPlaying
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.IsPlaying();
                }
                else
                {
                    return false;
                }
            }
        }

        private bool loggingRegistered = false;

        /// <summary>
        /// Connects (only the first time) the events from <see cref="myVlcMediaPlayer"/> to the event handlers registered on this instance
        /// </summary>
        private void RegisterLogging()
        {
            if (this.loggingRegistered)
                return;
            this.VlcMediaPlayer.Log += this.OnLogInternal;
            this.loggingRegistered = true;
        }

        // work around https://stackoverflow.com/questions/34664/designmode-with-nested-controls/2693338#2693338
        private bool IsInDesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    return true;

                Control ctrl = this;
                while (ctrl != null)
                {
                    if ((ctrl.Site != null) && ctrl.Site.DesignMode)
                        return true;
                    ctrl = ctrl.Parent;
                }
                return false;
            }
        }

        private async Task WaitControlLoaded()
        {
            while (ControlLoaded == false)
            {
                await Task.Delay(10);
            }
        }

        #region VlcControl Functions & Properties

        public async void Play()
        {
            await WaitControlLoaded();
            VlcMediaPlayer?.Play();
        }

        public async void Play(FileInfo file, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer?.Play(file, options);
        }

        public async void Play(Uri uri, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer?.Play(uri, options);
        }

        public async void Play(string mrl, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer?.Play(mrl, options);
        }

        public async void Play(Stream stream, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer?.Play(stream, options);
        }

        /// <summary>
        /// Toggle pause (no effect if there is no media) 
        /// </summary>
        public void Pause()
        {
            VlcMediaPlayer?.Pause();
        }

        /// <summary>
        /// Pause or resume (no effect if there is no media) 
        /// </summary>
        /// <param name="doPause">If set to <c>true</c>, pauses the media, resumes if <c>false</c></param>
        public void SetPause(bool doPause)
        {
            VlcMediaPlayer?.SetPause(doPause);
        }

        public void Stop()
        {
            if (VlcMediaPlayer != null)
            {
                VlcMediaPlayer.Stop();
            }

        }

        public VlcMedia GetCurrentMedia()
        {
            if (VlcMediaPlayer != null)
            {
                return VlcMediaPlayer.GetMedia();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Takes a snapshot of the currently playing video and saves it to the given file
        /// </summary>
        /// <param name="fileName">The name of the file to be written</param>
        public bool TakeSnapshot(string fileName)
        {
            return this.TakeSnapshot(fileName, 0, 0);
        }

        /// <summary>
        /// Takes a snapshot of the currently playing video and saves it to the given file
        /// </summary>
        /// <remarks>If width AND height is 0, original size is used. If width XOR height is 0, original aspect-ratio is preserved.</remarks>
        /// <param name="fileName">The name of the file to be written</param>
        /// <param name="width">The width of the snapshot (0 means auto)</param>
        /// <param name="height">The height of the snapshot (0 means auto)</param>
        public bool TakeSnapshot(string fileName, uint width, uint height)
        {
            return this.TakeSnapshot(new FileInfo(fileName), width, height);
        }


        /// <summary>
        /// Takes a snapshot of the currently playing video and saves it to the given file
        /// </summary>
        /// <param name="file">The file to be written</param>
        public bool TakeSnapshot(FileInfo file)
        {
            return this.TakeSnapshot(file, 0, 0);
        }


        /// <summary>
        /// Takes a snapshot of the currently playing video and saves it to the given file
        /// </summary>
        /// <remarks>If width AND height is 0, original size is used. If width XOR height is 0, original aspect-ratio is preserved.</remarks>
        /// <param name="file">The file to be written</param>
        /// <param name="width">The width of the snapshot (0 means auto)</param>
        /// <param name="height">The height of the snapshot (0 means auto)</param>
        public bool TakeSnapshot(FileInfo file, uint width, uint height)
        {
            return this.VlcMediaPlayer.TakeSnapshot(file, width, height);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public float Position
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Position;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (VlcMediaPlayer != null)
                {
                    VlcMediaPlayer.Position = value;
                }

            }
        }

        [Browsable(false)]
        public IChapterManagement Chapter
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Chapters;
                }
                else
                {
                    return null;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public float Rate
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Rate;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (VlcMediaPlayer != null)
                {
                    VlcMediaPlayer.Rate = value;
                }
            }
        }

        [Browsable(false)]
        public MediaStates State
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.State;
                }
                else
                {
                    return MediaStates.NothingSpecial;
                }
            }
        }

        [Browsable(false)]
        public ISubTitlesManagement SubTitles
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.SubTitles;
                }
                else
                {
                    return null;
                }

            }
        }

        [Browsable(false)]
        public IVideoManagement Video
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Video;
                }
                else
                {
                    return null;
                }
            }
        }

        [Browsable(false)]
        public IAudioManagement Audio
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Audio;
                }
                else
                {
                    return null;
                }
            }
        }

        [Browsable(false)]
        public long Length
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Length;
                }
                else
                {
                    return -1;
                }

            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public long Time
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Time;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (VlcMediaPlayer != null)
                {
                    VlcMediaPlayer.Time = value;
                }
            }
        }

        [Browsable(false)]
        public int Spu
        {
            get
            {
                if (VlcMediaPlayer != null)
                {
                    return VlcMediaPlayer.Spu;
                }
                return -1;
            }
            set
            {
                if (VlcMediaPlayer != null)
                {
                    VlcMediaPlayer.Spu = value;
                }
            }
        }

        public async void SetMedia(FileInfo file, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer.SetMedia(file, options);
        }

        public async void SetMedia(Uri file, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer.SetMedia(file, options);
        }

        public async void SetMedia(string mrl, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer.SetMedia(mrl, options);
        }

        public async void SetMedia(Stream stream, params string[] options)
        {
            await WaitControlLoaded();
            VlcMediaPlayer.SetMedia(stream, options);
        }

        public void ResetMedia()
        {
            VlcMediaPlayer.ResetMedia();
        }
        #endregion

        bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (disposed == false)
            {
                if (disposing)
                {
                    if (VlcMediaPlayer != null)
                    {
                        UnregisterEvents();
                        this.Stop();
                        VlcMediaPlayer.Dispose();
                    }
                    VlcMediaPlayer = null;
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }

        #region Event
        private object myEventSyncLocker = new object();

        private void RegisterEvents()
        {
            VlcMediaPlayer.Backward += OnBackwardInternal;
            VlcMediaPlayer.Buffering += OnBufferingInternal;
            VlcMediaPlayer.EncounteredError += OnEncounteredErrorInternal;
            VlcMediaPlayer.EndReached += OnEndReachedInternal;
            VlcMediaPlayer.Forward += OnForwardInternal;
            VlcMediaPlayer.LengthChanged += OnLengthChangedInternal;
            VlcMediaPlayer.MediaChanged += OnMediaChangedInternal;
            VlcMediaPlayer.Opening += OnOpeningInternal;
            VlcMediaPlayer.PausableChanged += OnPausableChangedInternal;
            VlcMediaPlayer.Paused += OnPausedInternal;
            VlcMediaPlayer.Playing += OnPlayingInternal;
            VlcMediaPlayer.PositionChanged += OnPositionChangedInternal;
            VlcMediaPlayer.ScrambledChanged += OnScrambledChangedInternal;
            VlcMediaPlayer.SeekableChanged += OnSeekableChangedInternal;
            VlcMediaPlayer.SnapshotTaken += OnSnapshotTakenInternal;
            VlcMediaPlayer.Stopped += OnStoppedInternal;
            VlcMediaPlayer.TimeChanged += OnTimeChangedInternal;
            VlcMediaPlayer.TitleChanged += OnTitleChangedInternal;
            VlcMediaPlayer.VideoOutChanged += OnVideoOutChangedInternal;
        }

        private void UnregisterEvents()
        {
            VlcMediaPlayer.Backward -= OnBackwardInternal;
            VlcMediaPlayer.Buffering -= OnBufferingInternal;
            VlcMediaPlayer.EncounteredError -= OnEncounteredErrorInternal;
            VlcMediaPlayer.EndReached -= OnEndReachedInternal;
            VlcMediaPlayer.Forward -= OnForwardInternal;
            VlcMediaPlayer.LengthChanged -= OnLengthChangedInternal;
            VlcMediaPlayer.MediaChanged -= OnMediaChangedInternal;
            VlcMediaPlayer.Opening -= OnOpeningInternal;
            VlcMediaPlayer.PausableChanged -= OnPausableChangedInternal;
            VlcMediaPlayer.Paused -= OnPausedInternal;
            VlcMediaPlayer.Playing -= OnPlayingInternal;
            VlcMediaPlayer.PositionChanged -= OnPositionChangedInternal;
            VlcMediaPlayer.ScrambledChanged -= OnScrambledChangedInternal;
            VlcMediaPlayer.SeekableChanged -= OnSeekableChangedInternal;
            VlcMediaPlayer.SnapshotTaken -= OnSnapshotTakenInternal;
            VlcMediaPlayer.Stopped -= OnStoppedInternal;
            VlcMediaPlayer.TimeChanged -= OnTimeChangedInternal;
            VlcMediaPlayer.TitleChanged -= OnTitleChangedInternal;
            VlcMediaPlayer.VideoOutChanged -= OnVideoOutChangedInternal;
        }

        #region Backward event
        private void OnBackwardInternal(object sender, VlcMediaPlayerBackwardEventArgs e)
        {
            OnBackward();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerBackwardEventArgs> Backward;

        public void OnBackward()
        {
            lock (myEventSyncLocker)
            {
                var del = Backward;
                if (del != null)
                    del(this, new VlcMediaPlayerBackwardEventArgs());
            }
        }
        #endregion

        #region Buffering event
        private void OnBufferingInternal(object sender, VlcMediaPlayerBufferingEventArgs e)
        {
            OnBuffering(e.NewCache);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerBufferingEventArgs> Buffering;

        public void OnBuffering(float newCache)
        {
            lock (myEventSyncLocker)
            {
                var del = Buffering;
                if (del != null)
                    del(this, new VlcMediaPlayerBufferingEventArgs(newCache));
            }
        }
        #endregion

        #region Encountered Error event
        private void OnEncounteredErrorInternal(object sender, VlcMediaPlayerEncounteredErrorEventArgs e)
        {
            OnEncounteredError();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerEncounteredErrorEventArgs> EncounteredError;

        public void OnEncounteredError()
        {
            lock (myEventSyncLocker)
            {
                var del = EncounteredError;
                if (del != null)
                    del(this, new VlcMediaPlayerEncounteredErrorEventArgs());
            }
        }
        #endregion

        #region EndReached event
        private void OnEndReachedInternal(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            OnEndReached();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerEndReachedEventArgs> EndReached;

        public void OnEndReached()
        {
            lock (myEventSyncLocker)
            {
                var del = EndReached;
                if (del != null)
                    del(this, new VlcMediaPlayerEndReachedEventArgs());
            }
        }
        #endregion

        #region Forward event
        private void OnForwardInternal(object sender, VlcMediaPlayerForwardEventArgs e)
        {
            OnForward();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerForwardEventArgs> Forward;

        public void OnForward()
        {
            lock (myEventSyncLocker)
            {
                var del = Forward;
                if (del != null)
                    del(this, new VlcMediaPlayerForwardEventArgs());
            }
        }
        #endregion

        #region Length Changed event
        private void OnLengthChangedInternal(object sender, VlcMediaPlayerLengthChangedEventArgs e)
        {
            OnLengthChanged(e.NewLength);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerLengthChangedEventArgs> LengthChanged;

        public void OnLengthChanged(long newLength)
        {
            lock (myEventSyncLocker)
            {
                var del = LengthChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerLengthChangedEventArgs(newLength));
            }
        }
        #endregion

        #region Log event
        private object _logLocker = new object();

        private EventHandler<VlcMediaPlayerLogEventArgs> log;

        private void OnLogInternal(object sender, VlcMediaPlayerLogEventArgs args)
        {
            lock (this._logLocker)
            {
                if (this.log != null)
                {
                    this.log(sender, args);
                }
            }
        }

        /// <summary>
        /// The event that is triggered when a log is emitted from libVLC.
        /// Listening to this event will discard the default logger in libvlc.
        /// </summary>
        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerLogEventArgs> Log
        {
            add
            {
                lock (this._logLocker)
                {
                    this.log += value;
                }
                if (this.VlcMediaPlayer != null)
                {
                    // Registers if not already done.
                    this.RegisterLogging();
                }
            }
            remove
            {
                lock (this._logLocker)
                {
                    this.log -= value;
                }
            }
        }
        #endregion

        #region Media Changed event
        private void OnMediaChangedInternal(object sender, VlcMediaPlayerMediaChangedEventArgs e)
        {
            OnMediaChanged(e.NewMedia);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerMediaChangedEventArgs> MediaChanged;

        public void OnMediaChanged(VlcMedia newMedia)
        {
            lock (myEventSyncLocker)
            {
                var del = MediaChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerMediaChangedEventArgs(newMedia));
            }
        }
        #endregion

        #region Opening event
        private void OnOpeningInternal(object sender, VlcMediaPlayerOpeningEventArgs e)
        {
            OnOpening();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerOpeningEventArgs> Opening;

        public void OnOpening()
        {
            lock (myEventSyncLocker)
            {
                var del = Opening;
                if (del != null)
                    del(this, new VlcMediaPlayerOpeningEventArgs());
            }
        }
        #endregion

        #region Pausable Changed event
        private void OnPausableChangedInternal(object sender, VlcMediaPlayerPausableChangedEventArgs e)
        {
            OnPausableChanged(e.IsPaused);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerPausableChangedEventArgs> PausableChanged;

        public void OnPausableChanged(bool isPaused)
        {
            lock (myEventSyncLocker)
            {
                var del = PausableChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerPausableChangedEventArgs(isPaused));
            }
        }

        #endregion

        #region Paused event
        private void OnPausedInternal(object sender, VlcMediaPlayerPausedEventArgs e)
        {
            OnPaused();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerPausedEventArgs> Paused;

        public void OnPaused()
        {
            lock (myEventSyncLocker)
            {
                var del = Paused;
                if (del != null)
                    del(this, new VlcMediaPlayerPausedEventArgs());
            }
        }
        #endregion

        #region Playing event
        private void OnPlayingInternal(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            OnPlaying();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerPlayingEventArgs> Playing;

        public void OnPlaying()
        {
            lock (myEventSyncLocker)
            {
                var del = Playing;
                if (del != null)
                    del(this, new VlcMediaPlayerPlayingEventArgs());
            }
        }

        #endregion

        #region Position Changed event
        private void OnPositionChangedInternal(object sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
            OnPositionChanged(e.NewPosition);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerPositionChangedEventArgs> PositionChanged;

        public void OnPositionChanged(float newPosition)
        {
            lock (myEventSyncLocker)
            {
                var del = PositionChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerPositionChangedEventArgs(newPosition));
            }
        }
        #endregion

        #region Scrambled Changed event
        private void OnScrambledChangedInternal(object sender, VlcMediaPlayerScrambledChangedEventArgs e)
        {
            OnScrambledChanged(e.NewScrambled);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerScrambledChangedEventArgs> ScrambledChanged;

        public void OnScrambledChanged(int newScrambled)
        {
            lock (myEventSyncLocker)
            {
                var del = ScrambledChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerScrambledChangedEventArgs(newScrambled));
            }
        }

        #endregion

        #region Seekable Changed event
        private void OnSeekableChangedInternal(object sender, VlcMediaPlayerSeekableChangedEventArgs e)
        {
            OnSeekableChanged(e.NewSeekable);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerSeekableChangedEventArgs> SeekableChanged;

        public void OnSeekableChanged(int newSeekable)
        {
            lock (myEventSyncLocker)
            {
                var del = SeekableChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerSeekableChangedEventArgs(newSeekable));
            }
        }
        #endregion

        #region Snapshot Taken event
        private void OnSnapshotTakenInternal(object sender, VlcMediaPlayerSnapshotTakenEventArgs e)
        {
            OnSnapshotTaken(e.FileName);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerSnapshotTakenEventArgs> SnapshotTaken;

        public void OnSnapshotTaken(string fileName)
        {
            lock (myEventSyncLocker)
            {
                var del = SnapshotTaken;
                if (del != null)
                    del(this, new VlcMediaPlayerSnapshotTakenEventArgs(fileName));
            }
        }

        #endregion

        #region Time Changed event
        private void OnTimeChangedInternal(object sender, VlcMediaPlayerTimeChangedEventArgs e)
        {
            OnTimeChanged(e.NewTime);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerTimeChangedEventArgs> TimeChanged;

        public void OnTimeChanged(long newTime)
        {
            lock (myEventSyncLocker)
            {
                var del = TimeChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerTimeChangedEventArgs(newTime));
            }
        }
        #endregion

        #region Title Changed event
        private void OnTitleChangedInternal(object sender, VlcMediaPlayerTitleChangedEventArgs e)
        {
            OnTitleChanged(e.NewTitle);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerTitleChangedEventArgs> TitleChanged;

        public void OnTitleChanged(int newTitle)
        {
            lock (myEventSyncLocker)
            {
                var del = TitleChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerTitleChangedEventArgs(newTitle));
            }
        }
        #endregion

        #region Stopped event
        private void OnStoppedInternal(object sender, VlcMediaPlayerStoppedEventArgs e)
        {
            OnStopped();
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerStoppedEventArgs> Stopped;

        public void OnStopped()
        {
            lock (myEventSyncLocker)
            {
                var del = Stopped;
                if (del != null)
                    del(this, new VlcMediaPlayerStoppedEventArgs());
            }
        }
        #endregion

        #region Video Out Changed event
        private void OnVideoOutChangedInternal(object sender, VlcMediaPlayerVideoOutChangedEventArgs e)
        {
            OnVideoOutChanged(e.NewCount);
        }

        [Category("Media Player")]
        public event EventHandler<VlcMediaPlayerVideoOutChangedEventArgs> VideoOutChanged;

        public void OnVideoOutChanged(int newCount)
        {
            lock (myEventSyncLocker)
            {
                var del = VideoOutChanged;
                if (del != null)
                    del(this, new VlcMediaPlayerVideoOutChangedEventArgs(newCount));
            }
        }
        #endregion
        #endregion
    }
}
