using System;
using System.Threading;
using System.Threading.Tasks;
using ActivityManager.Services.Caches;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityManager.HostedServices
{
    public class CachePrimerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public CachePrimerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                var lookupsCache = scope.ServiceProvider.GetRequiredService<ILookupsCache>();

                await lookupsCache.Prime();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
