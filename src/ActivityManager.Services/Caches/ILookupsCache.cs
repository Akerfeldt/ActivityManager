using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityManager.Models;

namespace ActivityManager.Services.Caches
{
    public interface ILookupsCache
    {
        Task<IDictionary<int, Activity>> GetActivities(bool fromSource = false);
        Task Prime();
    }
}