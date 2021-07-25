using System;

namespace Sky_multi_Core
{
    [AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false)]
    internal sealed class LibVlcFunctionAttribute : Attribute
    {
        public string FunctionName { get; private set; }

        public LibVlcFunctionAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }
}
