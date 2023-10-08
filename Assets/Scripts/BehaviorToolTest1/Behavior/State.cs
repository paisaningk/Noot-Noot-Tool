using System.Collections.Generic;
using UnityEngine;

namespace BehaviorToolTest1.Behavior
{
    [CreateAssetMenu]
    public class State : ScriptableObject
    {
        public StateAction[] actions;
        public StateAction[] onEnter;
        public StateAction[] onExit;
        public List<Transition> transitionList = new();
        public void Tick()
        {
            
        }

        public Transition AddTransition()
        {
            var transition = new Transition();
            transitionList.Add(transition);
            return transition;
        }
    }
}