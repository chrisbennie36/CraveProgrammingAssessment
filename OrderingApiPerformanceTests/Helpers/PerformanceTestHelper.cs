using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace OrderingApiPerformanceTests
{
    public static class PerformanceTestHelper
    {
        public static void MeasureCallTime(Action action, int maximumMillis)
        {
            var requestStopWatch = new Stopwatch();
            requestStopWatch.Start();
            action.Invoke();
            requestStopWatch.Stop();
            Assert.True(requestStopWatch.ElapsedMilliseconds < maximumMillis, $"Request:{action.Method.Name} Took {requestStopWatch.ElapsedMilliseconds}ms, should be less than {maximumMillis}ms.");
        }

        public static void MeasureCallTime(Action<Guid> action, int maximumMillis, Guid id)
        {
            var requestStopWatch = new Stopwatch();
            requestStopWatch.Start();
            action.Invoke(id);
            requestStopWatch.Stop();
            Assert.True(requestStopWatch.ElapsedMilliseconds < maximumMillis, $"Request:{action.Method.Name} Took {requestStopWatch.ElapsedMilliseconds}ms, should be less than {maximumMillis}ms.");
        }

        public static async Task MeasureCallTimeAsync(Func<Task> action, int maximumMillis)
        {
            var requestStopWatch = new Stopwatch();
            requestStopWatch.Start();
            await action.Invoke().ConfigureAwait(false);
            requestStopWatch.Stop();
            Assert.True(requestStopWatch.ElapsedMilliseconds < maximumMillis, $"Request:{action.Method.Name} Took {requestStopWatch.ElapsedMilliseconds}ms, should be less than {maximumMillis}ms.");
        }

        public static void Warmup(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch
            {
            }
        }

        public static async Task WarmupAsync(Func<Task> action)
        {
            try
            {
                await action.Invoke().ConfigureAwait(false);
            }
            catch
            {
            }
        }
    }
}
