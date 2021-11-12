using System;
using System.Runtime.InteropServices;

namespace Sky_multi_Core.VlcWrapper.Core
{
    /// <summary>
    /// The helper class that helps to call Marshal.XXX methods in a multi-framework way
    /// </summary>
    internal static class MarshalHelper
    {
        /// <summary>
        /// Converts a pointer to a C structure into a C# structure
        /// </summary>
        /// <typeparam name="T">The type of structure to convert</typeparam>
        /// <param name="ptr">The pointer</param>
        /// <returns>The converted structure</returns>
        internal static T PtrToStructure<T>(ref IntPtr ptr) where T: struct
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        internal static T PtrToStructure<T>(IntPtr ptr) where T : struct
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        /// <summary>
        /// Gets the size in bytes of a structure
        /// </summary>
        /// <typeparam name="T">The structure type</typeparam>
        /// <returns>The number of bytes in the structure</returns>
        internal static int SizeOf<T>()
        {
            return Marshal.SizeOf<T>();
        }

        /// <summary>
        /// Gets the delegate of the function at the given address
        /// </summary>
        /// <typeparam name="T">The delegate type</typeparam>
        /// <param name="ptr">The pointer to the C function</param>
        /// <returns>The delegate</returns>
        internal static T GetDelegateForFunctionPointer<T>(ref IntPtr ptr)
        {
            // The GetDelegateForFunctionPointer with two parameters is now deprecated.
            return Marshal.GetDelegateForFunctionPointer<T>(ptr);
        }
    }
}