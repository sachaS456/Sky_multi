using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.Signatures
{
    /// <summary>
    ///  Get long name of device, if not available short name given.
    /// </summary>
    [LibVlcFunction("libvlc_audio_output_device_longname")]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [Obsolete("Use GetAudioOutputDeviceList instead")]
    internal delegate IntPtr GetAudioOutputDeviceLongName(IntPtr instance, Utf8StringHandle audioOutputName, int deviceIndex);
}
