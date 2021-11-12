using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed partial class VlcManager
    {
        /// <summary>
        /// Sets the application name.
        /// LibVLC passes this as the user agent string when a protocol requires it.
        /// </summary>
        /// <param name="name">human-readable application name, e.g. "FooBar player 1.2.3"</param>
        /// <param name="http">HTTP User Agent, e.g. "FooBar/1.2.3 Python/2.6.0"</param>
        public void SetUserAgent(string name, string http)
        {
            using (var nameInterop = Utf8InteropStringConverter.ToUtf8StringHandle(name))
            using (var httpInterop = Utf8InteropStringConverter.ToUtf8StringHandle(http))
            {
                VlcNative.libvlc_set_user_agent(this.myVlcInstance, nameInterop, httpInterop);
            }
        }
    }
}
