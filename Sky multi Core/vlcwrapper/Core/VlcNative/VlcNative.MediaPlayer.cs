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
using System.Runtime.InteropServices;

namespace Sky_multi_Core.VlcWrapper.Core
{
    internal static partial class VlcNative
    {
        /// <summary>
        /// Get current audio channel.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_audio_get_channel(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current audio delay.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern long libvlc_audio_get_delay(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current mute status.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_audio_get_mute(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current audio track.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_audio_get_track(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get number of available audio tracks.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_audio_get_track_count(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the description of available audio tracks.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_audio_get_track_description(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current software audio volume.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_audio_get_volume(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get count of devices for audio output, these devices are hardware oriented like analor or digital output of sound card.
        /// </summary>
        [Obsolete("Use GetAudioOutputDeviceList instead")]
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_audio_output_device_count(IntPtr instance, Utf8StringHandle outputName);

        /// <summary>
        ///  Get id name of device.
        /// </summary>
        [Obsolete("Use GetAudioOutputDeviceList instead")]
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_audio_output_device_id(IntPtr instance, Utf8StringHandle audioOutputName, int deviceIndex);

        /// <summary>
        /// Gets a list of audio output devices for a given audio output module
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_audio_output_device_list_get(IntPtr instance, Utf8StringHandle aout);

        /// <summary>
        /// Frees a list of available audio output devices.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_audio_output_device_list_release(IntPtr deviceList);

        /// <summary>
        ///  Get long name of device, if not available short name given.
        /// </summary>
        [Obsolete("Use GetAudioOutputDeviceList instead")]
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_audio_output_device_longname(IntPtr instance, Utf8StringHandle audioOutputName, int deviceIndex);

        /// <summary>
        ///  Set audio output device. Changes are only effective after stop and play.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_output_device_set(IntPtr mediaPlayerInstance, Utf8StringHandle audioOutputName, Utf8StringHandle deviceName);

        /// <summary>
        /// Get the list of available audio outputs.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_audio_output_list_get(IntPtr instance);

        /// <summary>
        /// It will release the list of available audio outputs.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_output_list_release(IntPtr audioOutput);

        /// <summary>
        /// Set the audio output. Change will be applied after stop and play.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_audio_output_set(IntPtr mediaPlayerInstance, Utf8StringHandle audioOutputName);

        /// <summary>
        /// Set current audio channel.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_set_channel(IntPtr mediaPlayerInstance, int channel);

        /// <summary>
        /// Set current audio delay. The audio delay will be reset to zero each time the media changes.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_set_delay(IntPtr mediaPlayerInstance, long channel);

        /// <summary>
        /// Set current mute status.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_set_mute(IntPtr mediaPlayerInstance, int status);

        /// <summary>
        /// Set audio track.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_set_track(IntPtr mediaPlayerInstance, int trackId);

        /// <summary>
        /// Set current software audio volume.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_set_volume(IntPtr mediaPlayerInstance, int volume);

        /// <summary>
        /// Toggle mute status.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_audio_toggle_mute(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current fullscreen status. 
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_get_fullscreen(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Check if media player can pause.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_player_can_pause(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the Event Manager from which the media player send event.
        /// </summary>
        /// <returns>Return the event manager associated with media player.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_player_event_manager(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get media chapter (if applicable).
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_player_get_chapter(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get media chapter count.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_player_get_chapter_count(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get media fps rate.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float libvlc_media_player_get_fps(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the Windows API window handle (HWND) previously set.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_player_get_hwnd(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get length of movie playing
        /// </summary>
        /// <returns>Get the requested movie play rate.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern long libvlc_media_player_get_length(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the media used by the media_player.
        /// </summary>
        /// <returns>Return the media associated with p_mi, or NULL if no media is associated.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_player_get_media(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get media position.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float libvlc_media_player_get_position(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the requested media play rate.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float libvlc_media_player_get_rate(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the media state.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MediaStates libvlc_media_player_get_state(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get Rate at which movie is playing.
        /// </summary>
        /// <returns>Get the requested movie play rate.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern long libvlc_media_player_get_time(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Check if media player is playing.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_player_is_playing(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get media is seekable.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_player_is_seekable(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Navigate through DVD Menu.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_navigate(IntPtr mediaPlayerInstance, NavigateModes navigate);

        /// <summary>
        /// Create an empty Media Player object.
        /// </summary>
        /// <returns>Return a new media player object, or NULL on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_player_new(IntPtr instance);

        /// <summary>
        /// Create a Media Player object from a Media.
        /// </summary>
        /// <returns>Return a new media player object, or NULL on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_media_player_new_from_media(IntPtr mediaInstance);

        /// <summary>
        /// Set next media chapter (if applicable).
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_next_chapter(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Display the next frame (if supported).
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_next_frame(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Toggle pause (no effect if there is no media).
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_pause(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Play.
        /// </summary>
        /// <returns>Return 0 if playback started (and was already started), or -1 on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_player_play(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Set previous media chapter (if applicable).
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_previous_chapter(IntPtr mediaPlayerInstance);

        /// <summary>
        /// It will release the media player object. If the media player object has been released, then it should not be used again.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_release(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Set media chapter (if applicable).
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_chapter(IntPtr mediaPlayerInstance, int chapter);

        /// <summary>
        /// Set a Win32/Win64 API window handle (HWND) where the media player should render its video output. If LibVLC was built without Win32/Win64 API output support, then this has no effects.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_hwnd(IntPtr mediaPlayerInstance, IntPtr videoHostHandle);

        /// <summary>
        /// Set the media that will be used by the media_player. If any, previous media will be released.
        /// </summary>
        /// <returns>Return a new media player object, or NULL on error.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_media(IntPtr mediaPlayerInstance, IntPtr mediaInstance);

        /// <summary>
        /// Pause or resume (no effect if there is no media) 
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_pause(IntPtr mediaPlayerInstance, bool doPause);

        /// <summary>
        /// Get media position.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_position(IntPtr mediaPlayerInstance, float position);

        /// <summary>
        /// Set the requested media play rate.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_rate(IntPtr mediaPlayerInstance, float rate);

        /// <summary>
        /// Get time at which movie is playing.
        /// </summary>
        /// <returns>Get the requested movie play time.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_time(IntPtr mediaPlayerInstance, long time);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_set_video_title_display(IntPtr mp, Position position, int timeout);

        /// <summary>
        /// Stop.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_media_player_stop(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Is the player able to play.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_media_player_will_play(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Set fullscreen.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_set_fullscreen(IntPtr mediaPlayerInstance, int value);

        /// <summary>
        /// It will release the libvlc_track_description_t object.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_track_description_list_release(IntPtr trackDescription);

        /// <summary>
        /// Get an float adjust option value.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float libvlc_video_get_adjust_float(IntPtr mediaPlayerInstance, VideoAdjustOptions option);

        /// <summary>
        /// Get an integer adjust option value.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_adjust_int(IntPtr mediaPlayerInstance, VideoAdjustOptions option);

        /// <summary>
        /// Get current crop filter geometry.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_video_get_aspect_ratio(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current crop filter geometry.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_video_get_crop_geometry(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get integer logo option.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_logo_int(IntPtr mediaPlayerInstance, VideoLogoOptions option);

        /// <summary>
        /// Get an integer marquee option value.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_marquee_int(IntPtr mediaPlayerInstance, VideoMarqueeOptions option);

        /// <summary>
        /// Get a string marquee option value.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_video_get_marquee_string(IntPtr mediaPlayerInstance, VideoMarqueeOptions option);

        /// <summary>
        /// Get current video subtitle.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_spu(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the number of available video subtitles.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_spu_count(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the current subtitle delay.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern long libvlc_video_get_spu_delay(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the description of available video subtitles.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_video_get_spu_description(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the scale video.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float libvlc_video_get_scale(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current teletext page requested.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_teletext(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get current video track.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_track(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get number of available video tracks.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_get_track_count(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Get the description of available video tracks.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr libvlc_video_get_track_description(IntPtr mediaPlayerInstance);

        /// <summary>
        /// Set adjust option as float.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_adjust_float(IntPtr mediaPlayerInstance, VideoAdjustOptions option, float value);

        /// <summary>
        /// Set adjust option as integer.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_adjust_int(IntPtr mediaPlayerInstance, VideoAdjustOptions option, int value);

        /// <summary>
        /// Set current crop filter geometry.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_aspect_ratio(IntPtr mediaPlayerInstance, Utf8StringHandle cropGeometry);

        /// <summary>
        /// Set callbacks and private data to render decoded video to a custom area
        /// in memory.
        /// Use <see cref="SetVideoFormatCallbacks"/> to configure the decoded format.
        /// </summary>
        /// <param name="mp">
        /// The media player.
        /// </param>
        /// <param name="lockCallback">
        /// Callback to lock video memory (must not be NULL)
        /// </param>
        /// <param name="unlockCallback">
        /// Callback to unlock video memory (or NULL if not needed)
        /// </param>
        /// <param name="displayCallback">
        /// Callback to display video (or NULL if not needed)
        /// </param>
        /// <param name="userData">
        /// Private pointer for the three callbacks (as first parameter)
        /// </param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_callbacks(IntPtr mp, LockVideoCallback lockCallback, UnlockVideoCallback unlockCallback, DisplayVideoCallback displayCallback, IntPtr userData);

        /// <summary>
        /// Set current crop filter geometry.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_crop_geometry(IntPtr mediaPlayerInstance, Utf8StringHandle cropGeometry);

        /// <summary>
        /// Enable or disable deinterlace filter.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_deinterlace(IntPtr mediaPlayerInstance, Utf8StringHandle mode);

        /// <summary>
        /// Set decoded video chroma and dimensions. This only works in combination with
        /// libvlc_video_set_callbacks().
        /// </summary>
        /// <param name="mp">
        /// The media player
        /// </param>
        /// <param name="setup">
        /// Callback to select the video format (cannot be NULL)
        /// </param>
        /// <param name="cleanup">
        /// callback to release any allocated resources (or NULL)
        /// </param>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_format_callbacks(IntPtr mp, VideoFormatCallback setup, CleanupVideoCallback cleanup);

        /// <summary>
        /// Set current key input status.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_key_input(IntPtr mediaPlayerInstance, uint status);

        /// <summary>
        /// Set logo option as integer.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_logo_int(IntPtr mediaPlayerInstance, VideoLogoOptions option, int value);

        /// <summary>
        /// Set logo option as string.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_logo_string(IntPtr mediaPlayerInstance, VideoLogoOptions option, Utf8StringHandle value);

        /// <summary>
        /// Set an integer marquee option value.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_marquee_int(IntPtr mediaPlayerInstance, VideoMarqueeOptions option, int value);

        /// <summary>
        /// Set a string marquee option value.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_marquee_string(IntPtr mediaPlayerInstance, VideoMarqueeOptions option, Utf8StringHandle value);

        /// <summary>
        /// Set current mouse input status.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_mouse_input(IntPtr mediaPlayerInstance, uint status);

        /// <summary>
        /// Set new video subtitle.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_spu(IntPtr mediaPlayerInstance, int spu);

        /// <summary>
        /// Set the subtitle delay.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_spu_delay(IntPtr mediaPlayerInstance, long delay);

        /// <summary>
        /// Set scale video.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_scale(IntPtr mediaPlayerInstance, float factor);

        /// <summary>
        /// Set new teletext page to retrieve.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_teletext(IntPtr mediaPlayerInstance, int page);

        /// <summary>
        /// Set video track.
        /// </summary>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void libvlc_video_set_track(IntPtr mediaPlayerInstance, int trackId);

        /// <summary>
        /// Take a snapshot of the current video window.
        /// </summary>
        /// <returns>Return 0 on success, -1 if the video was not found.</returns>
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int libvlc_video_take_snapshot(IntPtr mediaPlayerInstance, uint num, Utf8StringHandle fileName, uint width, uint height);
    }
}
