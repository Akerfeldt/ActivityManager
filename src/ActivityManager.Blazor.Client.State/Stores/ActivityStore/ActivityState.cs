using ActivityManager.Models;

namespace ActivityManager.Blazor.Client.State
{
    public record ActivityState
    {
        public Activity[] Activities { get; }

        public ActivityState(Activity[] activities) => (Activities) = (activities);

        public static ActivityState InitialState { get; } = new ActivityState(System.Array.Empty<Activity>());
    }
}
