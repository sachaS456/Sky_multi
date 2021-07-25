﻿using System;
using System.Collections.Generic;
using Sky_multi_Core.Signatures;

namespace Sky_multi_Core
{
    internal sealed class VlcMediaInstance : InteropObjectInstance
    {
        private readonly VlcManager myManager;
        private static readonly Dictionary<IntPtr, VlcMediaInstance> AllInstances = new Dictionary<IntPtr, VlcMediaInstance>();

        internal static VlcMediaInstance New(VlcManager manager, IntPtr pointer)
        {
            lock (AllInstances)
            {
                AllInstances.TryGetValue(pointer, out var instance);

                if (null == instance)
                {
                    instance = new VlcMediaInstance(manager, pointer);
                    AllInstances.Add(pointer, instance);
                }

                return instance;
            }
        }

        private VlcMediaInstance(VlcManager manager, IntPtr pointer) : base(ref pointer)
        {
            myManager = manager;
        }

        protected override void Dispose(bool disposing)
        {
            lock (AllInstances)
            {
                AllInstances.Remove(this);
            }
            
            if (Pointer != IntPtr.Zero)
                myManager.ReleaseMedia(this);
            base.Dispose(disposing);
        }

        public static implicit operator IntPtr(VlcMediaInstance instance)
        {
            return instance.Pointer;
        }
    }
}