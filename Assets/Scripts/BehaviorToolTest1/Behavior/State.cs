using System.Collections.Generic;
using UnityEngine;

namespace BehaviorToolTest1.Behavior
{
    [CreateAssetMenu]
    public class State : ScriptableObject
    {
        public List<Transition> transitionList = new();
        public void Tick()
        {
            
        }
    }
}