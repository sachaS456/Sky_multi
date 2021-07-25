﻿using System;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    public partial class VlcMedia
    {
        private EventCallback myOnMediaStateChangedInternalEventCallback;
        public event EventHandler<VlcMediaStateChangedEventArgs> StateChanged;

        private void OnMediaStateChangedInternal(IntPtr ptr)
        {
            var args = MarshalHelper.PtrToStructure<VlcEventArg>(ref ptr);
            OnMediaStateChanged(args.eventArgsUnion.MediaStateChanged.NewState);
        }

        public void OnMediaStateChanged(MediaStates state)
        {
            StateChanged?.Invoke(this, new VlcMediaStateChangedEventArgs(state));
        }
    }
}