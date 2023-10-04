using UnityEngine;

namespace BehaviorToolTest1.Behavior.Conditions
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool CheckCondition(StateManger state);
    }
}