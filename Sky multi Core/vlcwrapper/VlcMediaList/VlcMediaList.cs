/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Sky_multi_Core.VlcWrapper.Core;

namespace Sky_multi_Core.VlcWrapper
{
    public sealed class VlcMediaList : IDisposable, IEnumerable<VlcMedia>
    {
        internal readonly IntPtr VlcMediaListInstance;
        internal readonly VlcInstance VlcInstance;
        private readonly object _syncLock = new object();
        private bool _nativeLock;

        public VlcMediaList(VlcMedia media)
        {
            VlcMediaListInstance = VlcNative.libvlc_media_subitems(media.MediaInstance);
            VlcInstance = media.VlcInstance;
        }

        internal VlcMediaList(VlcInstance Vlcinstance)
        {
            VlcMediaListInstance = VlcNative.LibVLCMediaListNew(Vlcinstance);
            VlcInstance = Vlcinstance;
        }

        internal VlcMediaList(VlcMediaInstance mediaInstance, VlcInstance vlcInstance)
        {
            VlcMediaListInstance = VlcNative.libvlc_media_subitems(mediaInstance);
            VlcInstance = vlcInstance;
        }

        internal VlcMediaList(IntPtr vlcMediaListInstance, VlcInstance VlcInstance)
        {
            VlcMediaListInstance = vlcMediaListInstance;
            this.VlcInstance = VlcInstance;
        }

        public void Dispose()
        {
            VlcNative.LibVLCMediaListRelease(VlcMediaListInstance);
        }

        /// <summary>
        /// Associate media instance with this media list instance. If another
        /// media instance was present it will be released.
        /// </summary>
        /// <param name="media">media instance to add</param>
        private void SetMedia(VlcMedia media)
        {
            VlcNative.LibVLCMediaListSetMedia(VlcMediaListInstance, media.MediaInstance);
        }

        /// <summary>
        /// Add media instance to media list
        /// </summary>
        /// <param name="media">a media instance</param>
        /// <returns>true on success, false if the media list is read-only</returns>
        public bool AddMedia(VlcMedia media)
        {
            return NativeSync(() => VlcNative.LibVLCMediaListAddMedia(VlcMediaListInstance, media.MediaInstance) == 0);
        }

        T NativeSync<T>(Func<T> operation)
        {
            try
            {
                lock (_syncLock)
                {
                    if (!_nativeLock)
                        Lock();
                    return operation();
                }
            }
            finally
            {
                lock (_syncLock)
                {
                    if (_nativeLock)
                        Unlock();
                }
            }
        }

        /// <summary>
        /// Insert media instance in media list on a position.
        /// </summary>
        /// <param name="media">a media instance</param>
        /// <param name="position">position in the array where to insert</param>
        /// <returns>true on success, false if the media list is read-only</returns>
        public bool InsertMedia(VlcMedia media, int position) =>
            NativeSync(() =>
            {
                if (media == null) throw new ArgumentNullException(nameof(media));
                return VlcNative.LibVLCMediaListInsertMedia(VlcMediaListInstance, media.MediaInstance, position) == 0;
            });

        /// <summary>
        /// Remove media instance from media list on a position.
        /// </summary>
        /// <param name="positionIndex">position in the array where to remove the iteam</param>
        /// <returns>true on success, false if the media list is read-only or the item was not found</returns>
        public bool RemoveIndex(int positionIndex) => NativeSync(() => VlcNative.LibVLCMediaListRemoveIndex(VlcMediaListInstance, positionIndex) == 0);

        /// <summary>
        /// Get count on media list items.
        /// </summary>
        public int Count => NativeSync(() => VlcNative.LibVLCMediaListCount(VlcMediaListInstance));

        /// <summary>
        /// Gets the element at the specified index
        /// </summary>
        /// <param name="position">position in array where to insert</param>
        /// <returns>media instance at position, or null if not found.
        /// In case of success, Media.Retain() is called to increase the refcount on the media. </returns>
        public VlcMedia? this[int position] => NativeSync(() =>
        {
            var ptr = VlcNative.LibVLCMediaListItemAtIndex(VlcMediaListInstance, position);
            return ptr == IntPtr.Zero ? null : new VlcMedia(VlcMediaInstance.New(in ptr), VlcInstance);
        });

        /// <summary>
        /// Find index position of List media instance in media list. Warning: the
        /// function will return the first matched position.
        /// </summary>
        /// <param name="media">media instance</param>
        /// <returns>position of media instance or -1 if media not found</returns>
        public int IndexOf(VlcMedia media) => NativeSync(() => VlcNative.LibVLCMediaListIndexOfItem(VlcMediaListInstance, media.MediaInstance));

        /// <summary>
        /// This indicates if this media list is read-only from a user point of view.
        /// True if readonly, false otherwise
        /// </summary>
        public bool IsReadonly => VlcNative.LibVLCMediaListIsReadonly(VlcMediaListInstance) == 1;

        /// <summary>
        /// Get lock on media list items
        /// </summary>
        internal void Lock()
        {
            lock (_syncLock)
            {
                if (_nativeLock)
                    throw new InvalidOperationException("already locked");

                _nativeLock = true;
                VlcNative.LibVLCMediaListLock(VlcMediaListInstance);
            }
        }

        /// <summary>
        /// Release lock on media list items The MediaList lock should be held upon entering this function.
        /// </summary>
        internal void Unlock()
        {
            lock (_syncLock)
            {
                if (!_nativeLock)
                    throw new InvalidOperationException("not locked");

                _nativeLock = false;
                VlcNative.LibVLCMediaListUnlock(VlcMediaListInstance);
            }
        }

        /// <summary>Increments the native reference counter for this medialist instance</summary>
        internal void Retain() => VlcNative.LibVLCMediaListRetain(VlcMediaListInstance);

        /// <summary>
        /// Returns an enumerator that iterates through a collection of media
        /// </summary>
        /// <returns>an enumerator over a media collection</returns>
        public IEnumerator<VlcMedia> GetEnumerator() => new MediaListEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class MediaListEnumerator : IEnumerator<VlcMedia>
    {
        int position = -1;
        VlcMediaList? _mediaList;

        internal MediaListEnumerator(VlcMediaList mediaList)
        {
            _mediaList = mediaList;
        }

        public bool MoveNext()
        {
            position++;
            return position < (_mediaList?.Count ?? 0);
        }

        void IEnumerator.Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            position = -1;
            _mediaList = default;
        }

        object IEnumerator.Current => Current;

        public VlcMedia Current
        {
            get
            {
                if (_mediaList == null)
                {
                    throw new ObjectDisposedException(nameof(MediaListEnumerator));
                }
                return _mediaList[position] ?? throw new ArgumentOutOfRangeException(nameof(position));
            }
        }
    }
}
