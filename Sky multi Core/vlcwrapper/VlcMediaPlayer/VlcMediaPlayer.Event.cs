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
using Sky_multi_Core.VlcWrapper.Core;
using System.Runtime.InteropServices;
using System.Threading;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcMediaPlayer
    {
        private EventCallback myOnMediaPlayerBackwardInternalEventCallback;
        public event EventHandler<VlcMediaPlayerBackwardEventArgs> Backward;

        private void OnMediaPlayerBackwardInternal(IntPtr ptr)
        {
            OnMediaPlayerBackward();
        }

        public void OnMediaPlayerBackward()
        {
            var del = Backward;
            if (del != null)
                del(this, new VlcMediaPlayerBackwardEventArgs());
        }

        private EventCallback myOnMediaPlayerBufferingInternalEventCallback;
        public event EventHandler<VlcMediaPlayerBufferingEventArgs> Buffering;

        private void OnMediaPlayerBufferingInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerBuffering(args.eventArgsUnion.MediaPlayerBuffering.NewCache);
        }

        public void OnMediaPlayerBuffering(float newCache)
        {
            Buffering?.Invoke(this, new VlcMediaPlayerBufferingEventArgs(newCache));
        }

        private EventCallback myOnMediaPlayerEncounteredErrorInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEncounteredErrorEventArgs> EncounteredError;

        private void OnMediaPlayerEncounteredErrorInternal(IntPtr ptr)
        {
            OnMediaPlayerEncounteredError();
        }

        public void OnMediaPlayerEncounteredError()
        {
            var del = EncounteredError;
            if (del != null)
                del(this, new VlcMediaPlayerEncounteredErrorEventArgs());
        }

        private EventCallback myOnMediaPlayerEndReachedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEndReachedEventArgs> EndReached;

        private void OnMediaPlayerEndReachedInternal(IntPtr ptr)
        {
            OnMediaPlayerEndReached();
        }

        public void OnMediaPlayerEndReached()
        {
            var del = EndReached;
            if (del != null)
                del(this, new VlcMediaPlayerEndReachedEventArgs());
        }

        private EventCallback myOnMediaPlayerForwardInternalEventCallback;
        public event EventHandler<VlcMediaPlayerForwardEventArgs> Forward;

        private void OnMediaPlayerForwardInternal(IntPtr ptr)
        {
            OnMediaPlayerForward();
        }

        public void OnMediaPlayerForward()
        {
            var del = Forward;
            if (del != null)
                del(this, new VlcMediaPlayerForwardEventArgs());
        }

        private EventCallback myOnMediaPlayerLengthChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerLengthChangedEventArgs> LengthChanged;

        private void OnMediaPlayerLengthChangedInternal(IntPtr ptr)
        {
            VlcEventArg args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerLengthChanged(args.eventArgsUnion.MediaPlayerLengthChanged.NewLength);
        }

        public void OnMediaPlayerLengthChanged(long newLength)
        {
            LengthChanged?.Invoke(this, new VlcMediaPlayerLengthChangedEventArgs(newLength));
        }

        private EventCallback myOnMediaPlayerMediaChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerMediaChangedEventArgs> MediaChanged;

        private void OnMediaPlayerMediaChangedInternal(IntPtr ptr)
        {
            VlcEventArg args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerMediaChanged(new VlcMedia(VlcMediaInstance.New(args.eventArgsUnion.MediaPlayerMediaChanged.MediaInstance), myVlcInstance));
        }

        public void OnMediaPlayerMediaChanged(VlcMedia media)
        {
            MediaChanged?.Invoke(this, new VlcMediaPlayerMediaChangedEventArgs(media));
        }

        private EventCallback myOnMediaPlayerOpeningInternalEventCallback;
        public event EventHandler<VlcMediaPlayerOpeningEventArgs> Opening;

        private void OnMediaPlayerOpeningInternal(IntPtr ptr)
        {
            OnMediaPlayerOpening();
        }

        public void OnMediaPlayerOpening()
        {
            var del = Opening;
            if (del != null)
                del(this, new VlcMediaPlayerOpeningEventArgs());
        }

        private EventCallback myOnMediaPlayerPausableChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPausableChangedEventArgs> PausableChanged;

        private void OnMediaPlayerPausableChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerPausableChanged(args.eventArgsUnion.MediaPlayerPausableChanged.NewPausable == 1);
        }

        public void OnMediaPlayerPausableChanged(bool paused)
        {
            PausableChanged?.Invoke(this, new VlcMediaPlayerPausableChangedEventArgs(paused));
        }

        private EventCallback myOnMediaPlayerPausedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPausedEventArgs> Paused;

        private void OnMediaPlayerPausedInternal(IntPtr ptr)
        {
            OnMediaPlayerPaused();
        }

        public void OnMediaPlayerPaused()
        {
            var del = Paused;
            if (del != null)
                del(this, new VlcMediaPlayerPausedEventArgs());
        }

        private EventCallback myOnMediaPlayerPlayingInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPlayingEventArgs> Playing;

        private void OnMediaPlayerPlayingInternal(IntPtr ptr)
        {
            OnMediaPlayerPlaying();
        }

        public void OnMediaPlayerPlaying()
        {
            var del = Playing;
            if (del != null)
                del(this, new VlcMediaPlayerPlayingEventArgs());
        }

        private EventCallback myOnMediaPlayerPositionChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerPositionChangedEventArgs> PositionChanged;

        private void OnMediaPlayerPositionChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerPositionChanged(args.eventArgsUnion.MediaPlayerPositionChanged.NewPosition);
        }

        public void OnMediaPlayerPositionChanged(float newPosition)
        {
            PositionChanged?.Invoke(this, new VlcMediaPlayerPositionChangedEventArgs(newPosition));
        }

        private EventCallback myOnMediaPlayerScrambledChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerScrambledChangedEventArgs> ScrambledChanged;

        private void OnMediaPlayerScrambledChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerScrambledChanged(args.eventArgsUnion.MediaPlayerScrambledChanged.NewScrambled);
        }

        public void OnMediaPlayerScrambledChanged(int newScrambled)
        {
            ScrambledChanged?.Invoke(this, new VlcMediaPlayerScrambledChangedEventArgs(newScrambled));
        }

        private EventCallback myOnMediaPlayerSeekableChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerSeekableChangedEventArgs> SeekableChanged;

        private void OnMediaPlayerSeekableChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerSeekableChanged(args.eventArgsUnion.MediaPlayerSeekableChanged.NewSeekable);
        }

        public void OnMediaPlayerSeekableChanged(int newSeekable)
        {
            SeekableChanged?.Invoke(this, new VlcMediaPlayerSeekableChangedEventArgs(newSeekable));
        }

        private EventCallback myOnMediaPlayerSnapshotTakenInternalEventCallback;
        public event EventHandler<VlcMediaPlayerSnapshotTakenEventArgs> SnapshotTaken;

        private void OnMediaPlayerSnapshotTakenInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            var fileName = Utf8InteropStringConverter.Utf8InteropToString(args.eventArgsUnion.MediaPlayerSnapshotTaken.pszFilename);
            OnMediaPlayerSnapshotTaken(fileName);
        }

        public void OnMediaPlayerSnapshotTaken(string fileName)
        {
            SnapshotTaken?.Invoke(this, new VlcMediaPlayerSnapshotTakenEventArgs(fileName));
        }

        private EventCallback myOnMediaPlayerStoppedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerStoppedEventArgs> Stopped;

        private void OnMediaPlayerStoppedInternal(IntPtr ptr)
        {
            OnMediaPlayerStopped();
        }

        public void OnMediaPlayerStopped()
        {
            var del = Stopped;
            if (del != null)
                del(this, new VlcMediaPlayerStoppedEventArgs());
        }

        private EventCallback myOnMediaPlayerTimeChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerTimeChangedEventArgs> TimeChanged;

        private void OnMediaPlayerTimeChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerTimeChanged(args.eventArgsUnion.MediaPlayerTimeChanged.NewTime);
        }

        public void OnMediaPlayerTimeChanged(long newTime)
        {
            TimeChanged?.Invoke(this, new VlcMediaPlayerTimeChangedEventArgs(newTime));
        }

        private EventCallback myOnMediaPlayerTitleChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerTitleChangedEventArgs> TitleChanged;

        private void OnMediaPlayerTitleChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerTitleChanged(args.eventArgsUnion.MediaPlayerTitleChanged.NewTitle);
        }

        public void OnMediaPlayerTitleChanged(int newTitle)
        {
            TitleChanged?.Invoke(this, new VlcMediaPlayerTitleChangedEventArgs(newTitle));
        }

        private EventCallback myOnMediaPlayerVideoOutChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerVideoOutChangedEventArgs> VideoOutChanged;

        private void OnMediaPlayerVideoOutChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerVideoOutChanged(args.eventArgsUnion.MediaPlayerVideoOutChanged.NewCount);
        }

        public void OnMediaPlayerVideoOutChanged(int newCount)
        {
            VideoOutChanged?.Invoke(this, new VlcMediaPlayerVideoOutChangedEventArgs(newCount));
        }

        private EventCallback myOnMediaPlayerEsAddedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEsChangedEventArgs> EsAdded;

        private void OnMediaPlayerEsAddedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerEsAdded(args.eventArgsUnion.MediaPlayerEsChanged);
        }

        public void OnMediaPlayerEsAdded(MediaPlayerEsChanged eventArgs)
        {
            EsAdded?.Invoke(this, new VlcMediaPlayerEsChangedEventArgs(eventArgs.TrackType, eventArgs.Id));
        }

        private EventCallback myOnMediaPlayerEsDeletedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEsChangedEventArgs> EsDeleted;

        private void OnMediaPlayerEsDeletedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerEsDeleted(args.eventArgsUnion.MediaPlayerEsChanged);
        }

        public void OnMediaPlayerEsDeleted(MediaPlayerEsChanged eventArgs)
        {
            EsDeleted?.Invoke(this, new VlcMediaPlayerEsChangedEventArgs(eventArgs.TrackType, eventArgs.Id));
        }

        private EventCallback myOnMediaPlayerEsSelectedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerEsChangedEventArgs> EsSelected;

        private void OnMediaPlayerEsSelectedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerEsSelected(args.eventArgsUnion.MediaPlayerEsChanged);
        }

        public void OnMediaPlayerEsSelected(MediaPlayerEsChanged eventArgs)
        {
            EsSelected?.Invoke(this, new VlcMediaPlayerEsChangedEventArgs(eventArgs.TrackType, eventArgs.Id));
        }

        private EventCallback myOnMediaPlayerCorkedInternalEventCallback;
        public event EventHandler Corked;

        private void OnMediaPlayerCorkedInternal(IntPtr ptr)
        {
            OnMediaPlayerCorked();
        }

        public void OnMediaPlayerCorked()
        {
            Corked?.Invoke(this, new EventArgs());
        }

        private EventCallback myOnMediaPlayerUncorkedInternalEventCallback;
        public event EventHandler Uncorked;

        private void OnMediaPlayerUncorkedInternal(IntPtr ptr)
        {
            OnMediaPlayerUncorked();
        }

        public void OnMediaPlayerUncorked()
        {
            Uncorked?.Invoke(this, new EventArgs());
        }

        private EventCallback myOnMediaPlayerMutedInternalEventCallback;
        public event EventHandler Muted;

        private void OnMediaPlayerMutedInternal(IntPtr ptr)
        {
            OnMediaPlayerMuted();
        }

        public void OnMediaPlayerMuted()
        {
            Muted?.Invoke(this, new EventArgs());
        }

        private EventCallback myOnMediaPlayerUnmutedInternalEventCallback;
        public event EventHandler Unmuted;

        private void OnMediaPlayerUnmutedInternal(IntPtr ptr)
        {
            OnMediaPlayerUnmuted();
        }

        public void OnMediaPlayerUnmuted()
        {
            Unmuted?.Invoke(this, new EventArgs());
        }

        private EventCallback myOnMediaPlayerAudioVolumeInternalEventCallback;
        public event EventHandler<VlcMediaPlayerAudioVolumeEventArgs> AudioVolume;

        private void OnMediaPlayerAudioVolumeInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerAudioVolume(args.eventArgsUnion.MediaPlayerAudioVolume.volume);
        }

        public void OnMediaPlayerAudioVolume(float volume)
        {
            AudioVolume?.Invoke(this, new VlcMediaPlayerAudioVolumeEventArgs(volume));
        }

        private EventCallback myOnMediaPlayerAudioDeviceInternalEventCallback;
        public event EventHandler<VlcMediaPlayerAudioDeviceEventArgs> AudioDevice;

        private void OnMediaPlayerAudioDeviceInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerAudioDevice(Utf8InteropStringConverter.Utf8InteropToString(args.eventArgsUnion.MediaPlayerAudioDevice.pszDevice));
        }

        public void OnMediaPlayerAudioDevice(string audioDevice)
        {
            AudioDevice?.Invoke(this, new VlcMediaPlayerAudioDeviceEventArgs(audioDevice));
        }

        private EventCallback myOnMediaPlayerChapterChangedInternalEventCallback;
        public event EventHandler<VlcMediaPlayerChapterChangedEventArgs> ChapterChanged;

        private void OnMediaPlayerChapterChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(in ptr);
            OnMediaPlayerChapterChanged(args.eventArgsUnion.MediaPlayerChapterChanged.NewChapter);
        }

        public void OnMediaPlayerChapterChanged(int newChapter)
        {
            ChapterChanged?.Invoke(this, new VlcMediaPlayerChapterChangedEventArgs(newChapter));
        }

        private object _logLock = new object();

        /// <summary>
        /// The real log event handlers.
        /// </summary>
        private EventHandler<VlcMediaPlayerLogEventArgs> log;

        /// <summary>
        /// A boolean to make sure that we are calling SetLog only once
        /// </summary>
        private bool logAttached = false;

        /// <summary>
        /// The event that is triggered when a log is emitted from libVLC.
        /// Listening to this event will discard the default logger in libvlc.
        /// </summary>
        public event EventHandler<VlcMediaPlayerLogEventArgs> Log
        {
            add
            {
                lock (this._logLock)
                {
                    this.log += value;
                    if (!this.logAttached)
                    {
                        VlcNative.libvlc_log_set(this.myVlcInstance, this.OnLogInternal, IntPtr.Zero);
                        this.logAttached = true;
                    }
                }
            }

            remove
            {
                lock (this._logLock)
                {
                    this.log -= value;
                }
            }
        }

        private void OnLogInternal(IntPtr data, VlcLogLevel level, IntPtr ctx, string format, IntPtr args)
        {
            if (this.log != null)
            {
                // Original source for va_list handling: https://stackoverflow.com/a/37629480/2663813
                var byteLength = Win32Interops._vscprintf(format, args) + 1;
                var utf8Buffer = Marshal.AllocHGlobal(byteLength);

                string formattedDecodedMessage;
                try
                {
                    Win32Interops.vsprintf(utf8Buffer, format, args);
                    formattedDecodedMessage = Utf8InteropStringConverter.Utf8InteropToString(utf8Buffer);
                }
                finally
                {
                    Marshal.FreeHGlobal(utf8Buffer);
                }

                string module;
                string file;
                uint? line;
                this.GetLogContext(ctx, out module, out file, out line);

                // Do the notification on another thread, so that VLC is not interrupted by the logging

                ThreadPool.QueueUserWorkItem(eventArgs =>
                {
                    if (this.log != null)
                        this.log(this.myMediaPlayerInstance, (VlcMediaPlayerLogEventArgs)eventArgs);
                }, new VlcMediaPlayerLogEventArgs(level, formattedDecodedMessage, module, file, line));
            }
        }

        /// <summary>
        /// Gets log message debug infos.
        ///
        /// This function retrieves self-debug information about a log message:
        /// - the name of the VLC module emitting the message,
        /// - the name of the source code module (i.e.file) and
        /// - the line number within the source code module.
        ///
        /// The returned module name and file name will be NULL if unknown.
        /// The returned line number will similarly be zero if unknown.
        /// </summary>
        /// <param name="logContext">The log message context (as passed to the <see cref="LogCallback"/>)</param>
        /// <param name="module">The module name storage.</param>
        /// <param name="file">The source code file name storage.</param>
        /// <param name="line">The source code file line number storage.</param>
        private void GetLogContext(IntPtr logContext, out string module, out string file, out uint? line)
        {
            UIntPtr line2;
            IntPtr module2;
            IntPtr file2;
            VlcNative.libvlc_log_get_context(logContext, out module2, out file2, out line2);
            if (line2 == UIntPtr.Zero)
            {
                line = null;
            }
            else
            {
                line = line2.ToUInt32();
            }

            module = Utf8InteropStringConverter.Utf8InteropToString(in module2);
            file = Utf8InteropStringConverter.Utf8InteropToString(in file2);
        }
    }
}
