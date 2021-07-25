using System;
using System.IO;

namespace Sky_multi_Viewer
{
    public sealed class VlcLibDirectoryNeededEventArgs : EventArgs
    {
        public DirectoryInfo VlcLibDirectory { get; set; }

        public VlcLibDirectoryNeededEventArgs()
        {
            
        }
    }
}