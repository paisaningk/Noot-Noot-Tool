using UnityEngine;

namespace BehaviorToolTest1.Behavior
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void Execute(StateManger stateManger);
    }
}