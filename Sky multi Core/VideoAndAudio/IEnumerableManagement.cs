using System.Collections.Generic;

namespace Sky_multi_Core
{
    public interface IEnumerableManagement<T>
    {
        int Count { get; }
        T Current { get; set; }
        IEnumerable<T> All { get; }
    }
}
