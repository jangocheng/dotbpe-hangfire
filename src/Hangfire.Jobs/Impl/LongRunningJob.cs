using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hangfire.Jobs.Impl
{
    [AutomaticRetry(Attempts = 0)]
    [DisableConcurrentExecution(90)]
    public class LongRunningJob : IRecurringJob
    {
        public void Execute(PerformContext context)
        {
            context.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} LongRunningJob Running ...");

            var runningTimes = context.GetJobData<int>("RunningTimes");

            context.WriteLine($"get job data parameter-> RunningTimes: {runningTimes}");

         
            var progressBar = context.WriteProgressBar();

            foreach (var i in Enumerable.Range(1, runningTimes).ToList().WithProgress(progressBar))
            {
                Thread.Sleep(10);
            }
        }
    }
}
