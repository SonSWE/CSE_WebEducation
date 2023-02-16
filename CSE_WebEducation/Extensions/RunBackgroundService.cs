using CommonLib;

namespace CSE_WebEducation
{
    public class RunBackgroundService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(CacheMemoryData, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return Task.CompletedTask;
        }

        public Task CacheMemoryData()
        {
            while (true)
            {
                try
                {
                    MemoryData.LoadMemory();

                    // đếm số đăng nhập
                }
                catch (Exception ex)
                {
                    Logger.log.Error(ex.ToString());
                }
                Thread.Sleep(5 * 60 * 1000);
            }
        }
    }
}
