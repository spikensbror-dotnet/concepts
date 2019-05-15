using Moq.Language;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concepts
{
    public static class MockAsyncCaptureExtensions
    {
        public static IReturnsThrows<TMock, Task> Capture<TMock, T1>(this ICallback<TMock, Task> callback, List<T1> results)
            where TMock : class
        {
            return callback
                .Callback<T1>(p1 => results.Add(p1));
        }

        public static IReturnsThrows<TMock, Task> Capture<TMock, T1, T2>(this ICallback<TMock, Task> callback, List<Tuple<T1, T2>> results)
            where TMock : class
        {
            return callback.Callback<T1, T2>((p1, p2) => results.Add(new Tuple<T1, T2>(p1, p2)));
        }

        public static IReturnsThrows<TMock, Task> Capture<TMock, T1, T2>(this ICallback<TMock, Task> callback, List<T1> t1Results, List<T2> t2Results)
            where TMock : class
        {
            return callback.Callback<T1, T2>((p1, p2) =>
            {
                t1Results.Add(p1);
                t2Results.Add(p2);
            });
        }
    }
}
