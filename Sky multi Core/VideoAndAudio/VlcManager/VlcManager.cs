using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Sky_multi_Core.Signatures;
using System.Collections.Generic;

namespace Sky_multi_Core
{
    public sealed partial class VlcManager : IDisposable
    {
        private readonly VlcLibraryLoader myLibraryLoader;
        private readonly VlcInstance myVlcInstance;
        private readonly VlcMediaPlayerInstance myMediaPlayerInstance;
        private VlcMedia myCurrentMedia;

        public string VlcVersion => Utf8InteropStringConverter.Utf8InteropToString(myLibraryLoader.GetInteropDelegate<GetVersion>().Invoke());

        public Version VlcVersionNumber
        {
            get
            {
                string versionString = this.VlcVersion;
                versionString = versionString.Split('-', ' ')[0];

                return new Version(versionString);
            }
        }

        public VlcManager(ref DirectoryInfo dynamicLinkLibrariesPath, string[] args)
        {
            this.myLibraryLoader = VlcLibraryLoader.GetOrCreateLoader(ref dynamicLinkLibrariesPath);

            IntPtr[] utf8Args = new IntPtr[args?.Length ?? 0];
            try
            {
                for (int i = 0; i < utf8Args.Length; i++)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(args[i]);
                    IntPtr buffer = Marshal.AllocHGlobal(bytes.Length + 1);
                    Marshal.Copy(bytes, 0, buffer, bytes.Length);
                    Marshal.WriteByte(buffer, bytes.Length, 0);
                    utf8Args[i] = buffer;
                }

                lock (utf8Args)
                {
                    lock (myLibraryLoader)
                    {
                        myVlcInstance = new VlcInstance(this, myLibraryLoader.GetInteropDelegate<CreateNewInstance>().Invoke(utf8Args.Length, utf8Args));
                        myMediaPlayerInstance = this.CreateMediaPlayer();
                    }
                }
            }
            finally
            {
                foreach (IntPtr arg in utf8Args)
                {
                    if (arg != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(arg);
                    }
                }
            }

            RegisterEvents();
            Chapters = new ChapterManagement(this, myMediaPlayerInstance);
            SubTitles = new SubTitlesManagement(this, myMediaPlayerInstance);
            Video = new VideoManagement(this, myMediaPlayerInstance);
            Audio = new AudioManagement(this, myMediaPlayerInstance);
            Dialogs = new DialogsManagement(this, myMediaPlayerInstance);
        }

        public VlcManager(ref DirectoryInfo dynamicLinkLibrariesPath, ref string[] args)
        {
            this.myLibraryLoader = VlcLibraryLoader.GetOrCreateLoader(ref dynamicLinkLibrariesPath);

            IntPtr[] utf8Args = new IntPtr[args?.Length ?? 0];
            try
            {
                for (int i = 0; i < utf8Args.Length; i++)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(args[i]);
                    IntPtr buffer = Marshal.AllocHGlobal(bytes.Length + 1);
                    Marshal.Copy(bytes, 0, buffer, bytes.Length);
                    Marshal.WriteByte(buffer, bytes.Length, 0);
                    utf8Args[i] = buffer;
                }

                myVlcInstance = new VlcInstance(this, myLibraryLoader.GetInteropDelegate<CreateNewInstance>().Invoke(utf8Args.Length, utf8Args));
                myMediaPlayerInstance = this.CreateMediaPlayer();
            }
            finally
            {
                foreach (IntPtr arg in utf8Args)
                {
                    if (arg != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(arg);
                    }
                }
            }

            RegisterEvents();
            Chapters = new ChapterManagement(this, myMediaPlayerInstance);
            SubTitles = new SubTitlesManagement(this, myMediaPlayerInstance);
            Video = new VideoManagement(this, myMediaPlayerInstance);
            Audio = new AudioManagement(this, myMediaPlayerInstance);
            Dialogs = new DialogsManagement(this, myMediaPlayerInstance);
        }

        public void Dispose()
        {
            if (myMediaPlayerInstance == IntPtr.Zero)
                return;
            UnregisterEvents();
            if (IsPlaying())
                Stop();

            myCurrentMedia?.Dispose();
            myMediaPlayerInstance.Dispose();
            myVlcInstance.Dispose();

            VlcLibraryLoader.ReleaseLoader(this.myLibraryLoader);

            if (this.dialogCallbacksPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.dialogCallbacksPointer);
            }
        }

        /// <summary>
        /// WARNING : USE AT YOUR OWN RISK!
        /// Gets the low-level interop manager that calls the methods on the libvlc library.
        /// This is useful if a higher-level API is missing.
        /// </summary>

        public IntPtr VideoHostControlHandle
        {
            get { return GetMediaPlayerVideoHostHandle(myMediaPlayerInstance); }
            set { SetMediaPlayerVideoHostHandle(myMediaPlayerInstance, value); }
        }

        public int SetAudioOutput(string outputName)
        {
            return this.SetAudioOutput(myMediaPlayerInstance, outputName);
        }

        public VlcMedia SetMedia(FileInfo file, params string[] options)
        {
            return SetMedia(new VlcMedia(this, ref file, options));
        }

        public VlcMedia SetMedia(Uri uri, params string[] options)
        {
            return SetMedia(new VlcMedia(this, ref uri, options));
        }

        public VlcMedia SetMedia(string mrl, params string[] options)
        {
            return SetMedia(new VlcMedia(this, ref mrl, options));
        }

        public VlcMedia SetMedia(Stream stream, params string[] options)
        {
            return SetMedia(new VlcMedia(this, ref stream, options));
        }

        public void ResetMedia()
        {
            SetMedia((VlcMedia)null);
        }

        private VlcMedia SetMedia(VlcMedia media)
        {
            // If there is a previous media, dispose it.
            myCurrentMedia?.Dispose();

            // Set it to the media player.
            this.SetMediaToMediaPlayer(myMediaPlayerInstance, media?.MediaInstance);

            // Register Events.
            media?.Initialize();
            myCurrentMedia = media;

            return media;
        }

        public VlcMedia GetMedia()
        {
            return myCurrentMedia;
        }

        public void Play()
        {
            this.Play(myMediaPlayerInstance);
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(System.IO.FileInfo,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="file">The file to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(FileInfo file, params string[] options)
        {
            this.SetMedia(file, options);
            this.Play();
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(System.Uri,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="uri">The uri to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(Uri uri, params string[] options)
        {
            this.SetMedia(uri, options);
            this.Play();
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(string,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="mrl">The mrl to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(string mrl, params string[] options)
        {
            this.SetMedia(mrl, options);
            this.Play();
        }

        /// <summary>
        /// Overload, provided for convenience that calls <see cref="SetMedia(System.IO.Stream,string[])"/> before <see cref="Play()"/>
        /// </summary>
        /// <param name="stream">The stream to play</param>
        /// <param name="options">The options to be given</param>
        public void Play(Stream stream, params string[] options)
        {
            this.SetMedia(stream, options);
            this.Play();
        }

        /// <summary>
        /// Toggle pause (no effect if there is no media) 
        /// </summary>
        public void Pause()
        {
            this.Pause(myMediaPlayerInstance);
        }

        /// <summary>
        /// Pause or resume (no effect if there is no media) 
        /// </summary>
        /// <param name="doPause">If set to <c>true</c>, pauses the media, resumes if <c>false</c></param>
        public void SetPause(bool doPause)
        {
            this.SetPause(myMediaPlayerInstance, doPause);
        }

        public void Stop()
        {
            this.Stop(myMediaPlayerInstance);
        }

        public bool IsPlaying()
        {
            return this.IsPlaying(myMediaPlayerInstance);
        }

        public bool IsPausable()
        {
            return this.IsPausable(myMediaPlayerInstance);
        }

        public void NextFrame()
        {
            this.NextFrame(myMediaPlayerInstance);
        }

        public IEnumerable<FilterModuleDescription> GetAudioFilters()
        {
            var module = this.GetAudioFilterList();
            ModuleDescriptionStructure nextModule = MarshalHelper.PtrToStructure<ModuleDescriptionStructure>(ref module);
            var result = GetSubFilter(nextModule);
            if (module != IntPtr.Zero)
                this.ReleaseModuleDescriptionInstance(module);
            return result;
        }

        private List<FilterModuleDescription> GetSubFilter(ModuleDescriptionStructure module)
        {
            var result = new List<FilterModuleDescription>();
            var filterModule = FilterModuleDescription.GetFilterModuleDescription(module);
            if (filterModule == null)
            {
                return result;
            }
            result.Add(filterModule);
            if (module.NextModule != IntPtr.Zero)
            {
                ModuleDescriptionStructure nextModule = MarshalHelper.PtrToStructure<ModuleDescriptionStructure>(ref module.NextModule);
                var data = GetSubFilter(nextModule);
                if (data.Count > 0)
                    result.AddRange(data);
            }
            return result;
        }

        public IEnumerable<FilterModuleDescription> GetVideoFilters()
        {
            var module = this.GetVideoFilterList();
            ModuleDescriptionStructure nextModule = MarshalHelper.PtrToStructure<ModuleDescriptionStructure>(ref module);
            var result = GetSubFilter(nextModule);
            if (module != IntPtr.Zero)
                this.ReleaseModuleDescriptionInstance(module);
            return result;
        }

        public float Position
        {
            get { return this.GetMediaPosition(myMediaPlayerInstance); }
            set { this.SetMediaPosition(myMediaPlayerInstance, value); }
        }

        /*public bool CouldPlay
        {
            get { return this.CouldPlay(myMediaPlayerInstance); }
        }*/

        public IChapterManagement Chapters { get; private set; }

        public float Rate
        {
            get { return this.GetRate(myMediaPlayerInstance); }
            set { this.SetRate(myMediaPlayerInstance, value); }
        }

        public MediaStates State
        {
            get { return this.GetMediaPlayerState(myMediaPlayerInstance); }
        }

        public float FramesPerSecond
        {
            get { return this.GetFramesPerSecond(myMediaPlayerInstance); }
        }

        /*public bool IsSeekable
        {
            get { return Half this.IsSeekable(myMediaPlayerInstance); }
        }*/

        public void Navigate(NavigateModes navigateMode)
        {
            this.Navigate(myMediaPlayerInstance, navigateMode);
        }

        public ISubTitlesManagement SubTitles { get; }

        public IVideoManagement Video { get; }

        public IAudioManagement Audio { get; }

        public IDialogsManagement Dialogs { get; }

        public long Length
        {
            get { return this.GetLength(myMediaPlayerInstance); }
        }

        public long Time
        {
            get { return this.GetTime(myMediaPlayerInstance); }
            set { this.SetTime(myMediaPlayerInstance, value); }
        }

        public int Spu
        {
            get { return this.GetVideoSpu(myMediaPlayerInstance); }
            set { this.SetVideoSpu(myMediaPlayerInstance, value); }
        }

        public bool TakeSnapshot(FileInfo file)
        {
            return TakeSnapshot(file, 0, 0);
        }

        public bool TakeSnapshot(FileInfo file, uint width, uint height)
        {
            return TakeSnapshot(0, file.FullName, width, height);
        }

        public void SetVideoTitleDisplay(Position position, int timeout)
        {
            this.SetVideoTitleDisplay(myMediaPlayerInstance, position, timeout);
        }

        /// <summary>
        /// Take a snapshot of the current video window.
        /// </summary>
        /// <param name="outputNumber">The number of video output (typically 0 for the first/only one)</param>
        /// <param name="file">The path of a file or a folder to save the screenshot into</param>
        /// <param name="width">the snapshot's width</param>
        /// <param name="height">the snapshot's height</param>
        /// <returns>A boolean indicating whether the screenshot was sucessfully taken</returns>
        /// <remarks>
        /// If i_width AND i_height is 0, original size is used.
        /// If i_width XOR i_height is 0, original aspect-ratio is preserved.
        /// </remarks>
        public bool TakeSnapshot(uint outputNumber, string file, uint width, uint height)
        {
            return this.TakeSnapshot(myMediaPlayerInstance, outputNumber, file, width, height);
        }

        private void RegisterEvents()
        {
            var vlcEventManager = this.GetMediaPlayerEventManager(myMediaPlayerInstance);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerBackward, myOnMediaPlayerBackwardInternalEventCallback = OnMediaPlayerBackwardInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerBuffering, myOnMediaPlayerBufferingInternalEventCallback = OnMediaPlayerBufferingInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEncounteredError, myOnMediaPlayerEncounteredErrorInternalEventCallback = OnMediaPlayerEncounteredErrorInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEndReached, myOnMediaPlayerEndReachedInternalEventCallback = OnMediaPlayerEndReachedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerForward, myOnMediaPlayerForwardInternalEventCallback = OnMediaPlayerForwardInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerLengthChanged, myOnMediaPlayerLengthChangedInternalEventCallback = OnMediaPlayerLengthChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerMediaChanged, myOnMediaPlayerMediaChangedInternalEventCallback = OnMediaPlayerMediaChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerOpening, myOnMediaPlayerOpeningInternalEventCallback = OnMediaPlayerOpeningInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPausableChanged, myOnMediaPlayerPausableChangedInternalEventCallback = OnMediaPlayerPausableChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPaused, myOnMediaPlayerPausedInternalEventCallback = OnMediaPlayerPausedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPlaying, myOnMediaPlayerPlayingInternalEventCallback = OnMediaPlayerPlayingInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerPositionChanged, myOnMediaPlayerPositionChangedInternalEventCallback = OnMediaPlayerPositionChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerScrambledChanged, myOnMediaPlayerScrambledChangedInternalEventCallback = OnMediaPlayerScrambledChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerSeekableChanged, myOnMediaPlayerSeekableChangedInternalEventCallback = OnMediaPlayerSeekableChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerSnapshotTaken, myOnMediaPlayerSnapshotTakenInternalEventCallback = OnMediaPlayerSnapshotTakenInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerStopped, myOnMediaPlayerStoppedInternalEventCallback = OnMediaPlayerStoppedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerTimeChanged, myOnMediaPlayerTimeChangedInternalEventCallback = OnMediaPlayerTimeChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerTitleChanged, myOnMediaPlayerTitleChangedInternalEventCallback = OnMediaPlayerTitleChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerVout, myOnMediaPlayerVideoOutChangedInternalEventCallback = OnMediaPlayerVideoOutChangedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEsAdded, myOnMediaPlayerEsAddedInternalEventCallback = OnMediaPlayerEsAddedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEsDeleted, myOnMediaPlayerEsDeletedInternalEventCallback = OnMediaPlayerEsDeletedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerEsSelected, myOnMediaPlayerEsSelectedInternalEventCallback = OnMediaPlayerEsSelectedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerCorked, myOnMediaPlayerCorkedInternalEventCallback = OnMediaPlayerCorkedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerUncorked, myOnMediaPlayerUncorkedInternalEventCallback = OnMediaPlayerUncorkedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerMuted, myOnMediaPlayerMutedInternalEventCallback = OnMediaPlayerMutedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerUnmuted, myOnMediaPlayerUnmutedInternalEventCallback = OnMediaPlayerUnmutedInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerAudioVolume, myOnMediaPlayerAudioVolumeInternalEventCallback = OnMediaPlayerAudioVolumeInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerAudioDevice, myOnMediaPlayerAudioDeviceInternalEventCallback = OnMediaPlayerAudioDeviceInternal);
            this.AttachEvent(vlcEventManager, EventTypes.MediaPlayerChapterChanged, myOnMediaPlayerChapterChangedInternalEventCallback = OnMediaPlayerChapterChangedInternal);
            vlcEventManager.Dispose();
        }

        private void UnregisterEvents()
        {
            var vlcEventManager = this.GetMediaPlayerEventManager(myMediaPlayerInstance);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerBackward, myOnMediaPlayerBackwardInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerBuffering, myOnMediaPlayerBufferingInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEncounteredError, myOnMediaPlayerEncounteredErrorInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEndReached, myOnMediaPlayerEndReachedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerForward, myOnMediaPlayerForwardInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerLengthChanged, myOnMediaPlayerLengthChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerMediaChanged, myOnMediaPlayerMediaChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerOpening, myOnMediaPlayerOpeningInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPausableChanged, myOnMediaPlayerPausableChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPaused, myOnMediaPlayerPausedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPlaying, myOnMediaPlayerPlayingInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerPositionChanged, myOnMediaPlayerPositionChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerScrambledChanged, myOnMediaPlayerScrambledChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerSeekableChanged, myOnMediaPlayerSeekableChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerSnapshotTaken, myOnMediaPlayerSnapshotTakenInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerStopped, myOnMediaPlayerStoppedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerTimeChanged, myOnMediaPlayerTimeChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerTitleChanged, myOnMediaPlayerTitleChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerVout, myOnMediaPlayerVideoOutChangedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEsAdded, myOnMediaPlayerEsAddedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEsDeleted, myOnMediaPlayerEsDeletedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerEsSelected, myOnMediaPlayerEsSelectedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerCorked, myOnMediaPlayerCorkedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerUncorked, myOnMediaPlayerUncorkedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerMuted, myOnMediaPlayerMutedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerUnmuted, myOnMediaPlayerUnmutedInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerAudioVolume, myOnMediaPlayerAudioVolumeInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerAudioDevice, myOnMediaPlayerAudioDeviceInternalEventCallback);
            this.DetachEvent(vlcEventManager, EventTypes.MediaPlayerChapterChanged, myOnMediaPlayerChapterChangedInternalEventCallback);
            vlcEventManager.Dispose();
        }
    }
}