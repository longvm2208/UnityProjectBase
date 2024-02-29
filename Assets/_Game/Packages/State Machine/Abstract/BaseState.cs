using UnityEngine;

namespace AbstractStateMachine
{
    public abstract class BaseState<T> where T : MonoBehaviour
    {
        public abstract void EnterState(T t);
        public abstract void UpdateState(T t);
        public abstract void ExitState(T t);
    }
}
