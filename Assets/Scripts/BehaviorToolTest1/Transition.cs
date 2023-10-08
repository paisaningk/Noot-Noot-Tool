using BehaviorToolTest1.Behavior;
using BehaviorToolTest1.Behavior.Conditions;

namespace BehaviorToolTest1
{
    [System.Serializable]
    public class Transition
    {
        public Condition condition;
        public State targetState;
        public bool disable;
    }
}
