using System;

namespace EventStateMachine
{
    [Serializable]
    public class StateMachine
    {
        public delegate void StateAction(ref Action onEnter, ref Action onExecute, ref Action onExit);
        private Action onEnter, onExecute, onExit;

        public void Execute()
        {
            onExecute?.Invoke();
        }

        public void ChangeState(StateAction stateAction)
        {
            onExit?.Invoke();
            stateAction.Invoke(ref onEnter, ref onExecute, ref onExit);
            onEnter?.Invoke();
        }
    }
}
