using System.Collections.Generic;
using BehaviorToolTest1.Behavior;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BehaviorToolTest1.BehaviorEditor.Node
{
    public class StateNode : BaseNode
    {
        private bool collapse;
        public State currentState;
        public State previousState;
        public List<BaseNode> referencesNodesList = new();

        // Icon
        public GUIContent iconCollapse;
        public GUIContent iconUnCollapse;
        public void OnEnable()
        {
            iconCollapse = EditorGUIUtility.IconContent("winbtn_win_max_h");
            iconUnCollapse = EditorGUIUtility.IconContent("winbtn_win_restore_h");
        }

        public override void DrawWindow()
        {
            if (currentState)
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

            currentState = (State) EditorGUILayout.ObjectField(currentState, typeof(State), false);

            if (previousState != currentState)
            {
                previousState = currentState;
                ClearReferences();

                for (var i = 0; i < currentState.transitionList.Count; i++)
                {
                    referencesNodesList.Add(BehaviorEditor.AddTransitionNode(i, currentState.transitionList[i], this));
                }
            }
        }

        public override void DrawCurve()
        {
            
        }

        public Transition AddTransition()
        {
            return currentState.AddTransition();
        }

        public void ClearReferences()
        {
            BehaviorEditor.ClearWindowsFromList(referencesNodesList);
            referencesNodesList.Clear();
        }
    }
}