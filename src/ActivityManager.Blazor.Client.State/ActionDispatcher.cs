using System;

namespace ActivityManager.Blazor.Client.State
{
    public class ActionDispatcher : IActionDispatcher
    {
        private Action<IAction> ActionHandlers;

        public void Subscribe(Action<IAction> actionHandler)
        {
            ActionHandlers += actionHandler;
        }
        public void Unsubscribe(Action<IAction> actionHandler)
        {
            ActionHandlers -= actionHandler;
        }

        public void Dispatch(IAction action)
        {
            ActionHandlers?.Invoke(action);
        }
    }
}
