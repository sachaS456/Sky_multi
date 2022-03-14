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
    internal sealed class DisposableLazy<T> : Lazy<T>, IDisposable where T : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableLazy{T}"/> class.
        /// </summary>
        public DisposableLazy() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableLazy{T}"/> class. When lazy initialization
        /// occurs, the default constructor of the target type and the specified initialization mode are used.
        /// </summary>
        /// <param name="isThreadSafe">
        /// <see langword="true"/> to make this instance usable concurrently by multiple threads; <see langword="false"/> to make
        /// this instance usable by only one thread at a time.
        /// </param>
        public DisposableLazy(bool isThreadSafe) : base(isThreadSafe)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableLazy{T}"/> class. When lazy initialization
        /// occurs, the specified initialization function is used.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is null.</exception>
        public DisposableLazy(Func<T> valueFactory) : base(valueFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableLazy{T}"/> class that uses the default constructor
        /// of T and the specified thread-safety mode.
        /// </summary>
        /// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> contains an invalid value.</exception>
        public DisposableLazy(LazyThreadSafetyMode mode) : base(mode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableLazy{T}"/> class. When lazy initialization
        /// occurs, the specified initialization function and initialization mode are used.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        /// <param name="isThreadSafe">
        /// <see langword="true"/> to make this instance usable concurrently by multiple threads; <see langword="false"/> to make
        /// this instance usable by only one thread at a time.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is null.</exception>
        public DisposableLazy(Func<T> valueFactory, bool isThreadSafe) : base(valueFactory, isThreadSafe)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableLazy{T}"/> class that uses the specified
        /// initialization function and thread-safety mode.
        /// </summary>
        /// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
        /// <param name="mode">One of the enumeration values that specifies the thread safety mode.</param>
        /// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> contains an invalid value.</exception>
        public DisposableLazy(Func<T> valueFactory, LazyThreadSafetyMode mode) : base(valueFactory, mode)
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.IsValueCreated)
            {
                this.Value.Dispose();
            }
        }
    }
}
