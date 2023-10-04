using BehaviorToolTest1.Behavior;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviorToolTest1.BehaviorEditor.Node
{
    public class StateNode : BaseNode
    {
        [AssetList]
        private bool collapse;
        public State currentstate;
        public GUIContent iconCollapse;
        public GUIContent iconUnCollapse;
        public void OnEnable()
        {
            iconCollapse = EditorGUIUtility.IconContent("winbtn_win_max_h");
            iconUnCollapse = EditorGUIUtility.IconContent("winbtn_win_restore_h");
        }

        public override void DrawWindow()
        {
            if (currentstate)
            {
                if (collapse)
                {
                    windowRect.height = 100;
                    if (GUILayout.Button(iconCollapse.image))
                    {
                        collapse = !collapse;
                    }
                }
                else
                {
                    windowRect.height = 300;
                    if (GUILayout.Button(iconUnCollapse.image))
                    {
                        collapse = !collapse;
                    }
                }
                
            }
            else
            {
                EditorGUILayout.LabelField("Add State to Modify");
            }

            currentstate = (State) EditorGUILayout.ObjectField(currentstate, typeof(State), false);
        }

        public override void DrawCurve()
        {
            base.DrawCurve();
        }
    }
}