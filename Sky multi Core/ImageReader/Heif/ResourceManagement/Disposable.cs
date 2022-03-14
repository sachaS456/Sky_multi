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
using System.Threading;

namespace Sky_multi_Core.ImageReader.Heif.ResourceManagement
{
    /// <summary>
    /// A base class that implements <see cref="IDisposable" />.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public abstract class Disposable : IDisposable
    {
        private int disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Disposable"/> class.
        /// </summary>
        protected Disposable()
        {
            this.disposed = 0;
        }

        private bool IsDisposed => Volatile.Read(ref this.disposed) != 0;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Interlocked.Exchange(ref this.disposed, 1) == 0)
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Verifies that the object has not been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">The object has been disposed.</exception>
        protected void VerifyNotDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
