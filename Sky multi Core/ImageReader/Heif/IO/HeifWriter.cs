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
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Sky_multi_Core.ImageReader.Heif.Interop;
using Sky_multi_Core.ImageReader.Heif.ResourceManagement;

namespace Sky_multi_Core.ImageReader.Heif
{
    internal abstract class HeifWriter : Disposable, IHeifIOError
    {
        private DisposableLazy<SafeCoTaskMemHandle> heifWriterHandle;
        private WriterErrors writerErrors;

        private readonly WriteDelegate writeDelegate;

        protected HeifWriter()
        {
            this.heifWriterHandle = new DisposableLazy<SafeCoTaskMemHandle>(CreateHeifWriter);
            this.writerErrors = new WriterErrors();
            this.writeDelegate = Write;
        }

        public ExceptionDispatchInfo CallbackExceptionInfo
        {
            get;
            private set;
        }

        public SafeHandle WriterHandle
        {
            get
            {
                VerifyNotDisposed();

                return this.heifWriterHandle.Value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposableUtil.Free(ref this.heifWriterHandle);
                DisposableUtil.Free(ref this.writerErrors);
            }

            base.Dispose(disposing);
        }

        protected abstract void WriteCore(IntPtr data, long count);

        private SafeCoTaskMemHandle CreateHeifWriter()
        {
            var writerHandle = SafeCoTaskMemHandle.Allocate(MarshalHelper.SizeOf<heif_writer>());

            unsafe
            {
                var writer = (heif_writer*)writerHandle.DangerousGetHandle();

                writer->heif_writer_version = 1;
                writer->write = Marshal.GetFunctionPointerForDelegate(this.writeDelegate);
            }

            return writerHandle;
        }

        private heif_error Write(IntPtr ctx, IntPtr data, UIntPtr size, IntPtr userData)
        {
            ulong count = size.ToUInt64();

            if (count > 0)
            {
                try
                {
                    WriteCore(data, checked((long)count));
                }
                catch (Exception ex)
                {
                    this.CallbackExceptionInfo = ExceptionDispatchInfo.Capture(ex);
                    return this.writerErrors.WriteError;
                }
            }

            return this.writerErrors.Success;
        }

        private sealed class WriterErrors : Disposable
        {
            private SafeCoTaskMemHandle successMessage;
            private SafeCoTaskMemHandle writeErrorMessage;

            public WriterErrors()
            {
                this.successMessage = null;
                this.writeErrorMessage = null;

                try
                {
                    this.successMessage = SafeCoTaskMemHandle.FromStringAnsi("Success");
                    this.writeErrorMessage = SafeCoTaskMemHandle.FromStringAnsi("Write error");
                }
                catch (Exception)
                {
                    DisposableUtil.Free(ref this.successMessage);
                    throw;
                }
            }

            public heif_error Success
            {
                get
                {
                    VerifyNotDisposed();

                    return new heif_error(heif_error_code.Ok,
                                          heif_suberror_code.Unspecified,
                                          this.successMessage.DangerousGetHandle());
                }
            }

            public heif_error WriteError
            {
                get
                {
                    VerifyNotDisposed();

                    return new heif_error(heif_error_code.Encoding_error,
                                          heif_suberror_code.Cannot_write_output_data,
                                          this.writeErrorMessage.DangerousGetHandle());
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    DisposableUtil.Free(ref this.successMessage);
                    DisposableUtil.Free(ref this.writeErrorMessage);
                }

                base.Dispose(disposing);
            }
        }
    }
}
