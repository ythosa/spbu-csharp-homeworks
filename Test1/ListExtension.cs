using System.Collections.Generic;

namespace Test1;

public static class ListExtension
{
    public static TV Pop<TV>(this List<TV> list)
    {
        var index = list.Count - 1;
        var element = list[index];
        list.RemoveAt(index);

        return element;
    }
}
