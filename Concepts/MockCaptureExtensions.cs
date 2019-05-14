using Moq.Language;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;

namespace Concepts
{
    public static class MockCaptureExtensions
    {
        public static ICallbackResult Capture<T1>(this ICallback callback, List<T1> results)
        {
            return callback
                .Callback<T1>(p1 => results.Add(p1));
        }

        public static ICallbackResult Capture<T1, T2>(this ICallback callback, List<Tuple<T1, T2>> results)
        {
            return callback.Callback<T1, T2>((p1, p2) => results.Add(new Tuple<T1, T2>(p1, p2)));
        }

        public static ICallbackResult Capture<T1, T2>(this ICallback callback, List<T1> t1Results, List<T2> t2Results)
        {
            return callback.Callback<T1, T2>((p1, p2) =>
            {
                t1Results.Add(p1);
                t2Results.Add(p2);
            });
        }
    }
}
