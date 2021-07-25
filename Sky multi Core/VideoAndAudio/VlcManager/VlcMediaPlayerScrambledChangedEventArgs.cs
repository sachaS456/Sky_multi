using System;

namespace Sky_multi_Core
{
    public sealed class VlcMediaPlayerScrambledChangedEventArgs : EventArgs
    {
        public VlcMediaPlayerScrambledChangedEventArgs(int newScrambled)
        {
            NewScrambled = newScrambled;
        }

        public int NewScrambled { get; private set; }
    }
}