using UnityEngine;

namespace BehaviorToolTest1.Behavior
{
    [CreateAssetMenu(menuName = "Action/Test")]
    public class ChangeHealth : StateAction
    {
        public override void Execute(StateManger stateManger)
        {
            stateManger.health += 10;
        }
    }
}