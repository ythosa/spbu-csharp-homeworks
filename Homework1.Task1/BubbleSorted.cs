using System.Collections;

namespace Homework1.Task1;

public class BubbleSorted<T> : IEnumerable<T> where T : IComparable<T>
{
    private readonly Lazy<T[]> _sortedElements;

    public BubbleSorted(IEnumerable<T> elements)
    {
        _sortedElements = new Lazy<T[]>(() => ApplyBubbleSort(elements.ToArray()));
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _sortedElements.Value.Cast<T>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static T[] ApplyBubbleSort(T[] elements)
    {
        for (var i = 0; i < elements.Length; i++)
        {
            for (var j = 0; j < elements.Length - i - 1; j++)
            {
                if (elements[j].CompareTo(elements[j + 1]) > 0)
                {
                    (elements[j], elements[j + 1]) = (elements[j + 1], elements[j]);
                }
            }
        }

        return elements;
    }
}
