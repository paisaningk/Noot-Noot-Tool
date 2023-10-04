using BehaviorToolTest1.Behavior.Conditions;
using UnityEditor;

namespace BehaviorToolTest1.BehaviorEditor.Node
{
    public class TransitionNode : BaseNode
    {
        public Transition targetTransition;
        public StateNode enterState;
        public StateNode targetState;

        public void Init(StateNode enterStateNode, Transition transition)
        {
            enterState = enterStateNode;
            targetTransition = transition;
        }

        public override void DrawWindow()
        {
            if (targetTransition == null)
            {
                return;
            }
            
            EditorGUILayout.LabelField("");
            targetTransition.condition =
                (Condition)EditorGUILayout.ObjectField(targetTransition.condition, typeof(Condition), false);

            if (!targetTransition.condition)
            {
                EditorGUILayout.LabelField("No Condition");
            }
            else
            {
                targetTransition.disable = EditorGUILayout.Toggle("Disable", targetTransition.disable);
            }

        }

        public override void DrawCurve()
        {
            base.DrawCurve();
        }
    }
}