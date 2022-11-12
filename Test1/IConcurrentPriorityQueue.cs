using System;

namespace Test1;

public interface IConcurrentPriorityQueue<in TP, TV> where TP : IComparable<TP>
{
    void Enqueue(TP priority, TV value);
    TV Dequeue();
    uint Size();
}
