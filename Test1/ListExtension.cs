using System.Collections.Generic;

namespace Test1;

public static class ListExtension
{
    public static TV Unshift<TV>(this List<TV> list)
    {
        var element = list[0];
        list.RemoveAt(0);

        return element;
    }
}
