using System;

namespace Sky_multi_Core.VlcWrapper
{
    internal struct HardwareAccelerationTypeString
    {
        public const string d3d11 = ":avcodec-hw=d3d11va";
        public const string dxva2 = ":avcodec-hw=dxva2";
        public const string none = ":avcodec-hw=none";
    }

    public enum HardwareAccelerationType
    {
        d3d11,
        dxva2,
        none
    }
}
