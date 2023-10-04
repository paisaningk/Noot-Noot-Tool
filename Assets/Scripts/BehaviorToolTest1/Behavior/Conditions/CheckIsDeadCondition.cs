using UnityEngine;

namespace BehaviorToolTest1.Behavior.Conditions
{
    [CreateAssetMenu(menuName = "")]
    public class CheckIsDeadCondition : Condition
    {
        public override bool CheckCondition(StateManger state)
        {
            return state.health <= 0;
        }
    }
}