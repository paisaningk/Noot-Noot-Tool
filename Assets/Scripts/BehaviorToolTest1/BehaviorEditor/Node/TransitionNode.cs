using BehaviorToolTest1.Behavior.Conditions;
using UnityEditor;
using UnityEngine;

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
                //Todo : แก้เป็นปุ่ม
                targetTransition.disable = EditorGUILayout.Toggle("Disable", targetTransition.disable);
            }

        }

        public override void DrawCurve()
        {
            if (enterState)
            {
                var rect = windowRect;
                rect.y += windowRect.height * 0.5f;
                rect.width = 1;
                rect.height = 1;
                
                BehaviorEditor.DrawNodeCurve(enterState.windowRect, rect, true, (targetTransition.disable) ? Color.red : Color.green);
            }
        }
    }
}