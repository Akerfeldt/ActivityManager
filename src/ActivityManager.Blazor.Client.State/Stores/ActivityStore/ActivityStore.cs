using System;

namespace ActivityManager.Blazor.Client.State
{
    public class ActivityStore
    {
        private IActionDispatcher _actionDispatcher;
        private Action<ActivityState> _listeners;
        private ActivityState _state = ActivityState.InitialState;

        public ActivityStore(IActionDispatcher actionDispatcher)
        {
            _actionDispatcher = actionDispatcher;
            _actionDispatcher.Subscribe(HandleAction);
        }

        public void HandleAction(IAction action)
        {
            switch (action)
            {
                case LoadActivities c:
                    _state = new ActivityState(c.Activities);
                    break;
            }

            Broadcast();
        }

        public void Subscribe(Action<ActivityState> listener)
        {
            _listeners += listener;
        }

        public void Unsubscribe(Action<ActivityState> listener)
        {
            _listeners -= listener;
        }

        private void Broadcast()
        {
            _listeners?.Invoke(_state);
        }
    }
}
