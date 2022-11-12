using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;

namespace Test1;

public class ConcurrentPriorityQueue<TP, TV> : IConcurrentPriorityQueue<TP, TV> where TP : IComparable<TP>
{
    private readonly SortedDictionary<TP, List<TV>> _elementsByPriority = new();
    private uint _elementsCount = 0;
    private readonly object _elementsLock = new();

    public void Enqueue(TP priority, TV value)
    {
        lock (_elementsLock)
        {
            if (!_elementsByPriority.ContainsKey(priority))
            {
                _elementsByPriority[priority] = new List<TV>();
            }

            _elementsByPriority[priority].Add(value);
            _elementsCount++;

            Monitor.PulseAll(_elementsLock);
        }
    }

    public TV Dequeue()
    {
        lock (_elementsLock)
        {
            if (_elementsCount == 0) Monitor.Wait(_elementsLock);

            var maxPriority = _elementsByPriority.Keys.Last();
            var element = _elementsByPriority[maxPriority].Unshift();
            if (_elementsByPriority[maxPriority].Count == 0)
            {
                _elementsByPriority.Remove(maxPriority);
            }

            _elementsCount--;

            return element;
        }
    }

    public uint Size() => _elementsCount;
}
