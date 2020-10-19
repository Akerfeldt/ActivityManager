using System;

namespace ActivityManager.Blazor.Client.State
{
    public interface IActionDispatcher
    {
        void Dispatch(IAction action);
        void Subscribe(Action<IAction> actionHandler);
        void Unsubscribe(Action<IAction> actionHandler);
    }
}