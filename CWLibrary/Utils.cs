using System;

namespace CWLibrary
{
    public static class Utils
    {
        internal static Random Random = new Random(DateTime.Now.Millisecond);
    }
}