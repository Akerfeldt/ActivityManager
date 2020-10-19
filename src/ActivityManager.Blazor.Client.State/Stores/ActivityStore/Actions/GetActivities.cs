namespace ActivityManager.Blazor.Client.State
{
    public record GetActivities : IAction
    {
        public string Name { get; } = nameof(GetActivities);
    }
}
