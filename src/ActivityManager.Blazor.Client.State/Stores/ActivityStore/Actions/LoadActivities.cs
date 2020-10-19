using ActivityManager.Models;

namespace ActivityManager.Blazor.Client.State
{
    public record LoadActivities : IAction
    {
        public string Name { get; } = nameof(LoadActivities);

        public Activity[] Activities { get; }

        public LoadActivities(Activity[] activities) => (Activities) = (activities);
    }
}
