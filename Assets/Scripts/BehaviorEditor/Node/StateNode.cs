using Behavior;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace BehaviorEditor.Node
{
    public class StateNode : BaseNode
    {
        [AssetList]
        private bool collapse;
        public State currentstate;

        public override void DrawWindow()
        {
            if (currentstate)
            {
                if (collapse)
                {
                    windowRect.height = 100;
                    var iconContent = EditorGUIUtility.IconContent("winbtn_win_max_h");
                    if (GUILayout.Button(iconContent.image))
                    {
                        collapse = !collapse;
                    }
                }
                else
                {
                    windowRect.height = 300;
                    var iconContent = EditorGUIUtility.IconContent("winbtn_win_restore_h");
                    if (GUILayout.Button(iconContent.image))
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