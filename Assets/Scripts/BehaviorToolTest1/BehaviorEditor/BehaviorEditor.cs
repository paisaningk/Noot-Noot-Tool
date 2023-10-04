using System.Collections.Generic;
using BehaviorToolTest1.BehaviorEditor.Node;
using UnityEditor;
using UnityEngine;

namespace BehaviorToolTest1.BehaviorEditor
{
    public class BehaviorEditor : EditorWindow
    {
        #region Variables

        private static List<BaseNode> windows = new List<BaseNode>();
        private Vector2 mousePosition;
        private bool makeTransition;
        private bool clickedOnWindow;
        private BaseNode selectedNode;

        #endregion
        
        #region Init
        [MenuItem("BehaviorEditor/Editor")]
        private static void ShowEditor()
        {
            var editor = GetWindow<BehaviorEditor>();
            editor.minSize = new Vector2(800, 500);
        }
        #endregion

        #region GUI Methods

        private void OnGUI()
        {
            var e = Event.current;
            mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindow();
        }

        // private void OnEnable()
        // {
        //     windows.Clear();
        // }

        private void DrawWindow()
        {
            BeginWindows();
            
            foreach (var baseNode in windows)
            {
                baseNode.DrawCurve();
            }

            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
            }
            
            EndWindows();
        }

        private void DrawNodeWindow(int id)
        {
            windows[id].DrawWindow();
            GUI.DragWindow();
        }

        private void UserInput(Event e)
        {
            if (e.button == 1 && !makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }
            
            if (e.button == 0 && !makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                   
                }
            }
        }

        private void RightClick(Event e)
        {
            selectedNode = null;
            foreach (var node in windows)
            {
                if (node.windowRect.Contains(e.mousePosition))
                {
                    clickedOnWindow = true;
                    selectedNode = node;
                    break;
                }
            }

            if (!clickedOnWindow)
            {
                AddNewNode(e);
            }
            else
            {
                ModifyNode(e);
            }
            
            clickedOnWindow = false;
        }

        private void AddNewNode(Event e)
        {
            var menu = new GenericMenu();
           
            menu.AddItem(new GUIContent("Add State"),false, ContextCallBack, UserAction.AddState);
            menu.AddItem(new GUIContent("Comment Node"),false, ContextCallBack, UserAction.CommentNode);
            
            menu.ShowAsContext();
            e.Use();
        }

        private void ModifyNode(Event e)
        {
            var menu = new GenericMenu();
            if (selectedNode is StateNode)
            {
                menu.AddItem(new GUIContent("Add TransitionNode"),false, ContextCallBack, UserAction.AddTransitionNode);
                menu.AddItem(new GUIContent("Delete Node"),false, ContextCallBack, UserAction.DeleteNode);
            }
            if (selectedNode is CommentNode)
            {
                menu.AddItem(new GUIContent("Delete Node"),false, ContextCallBack, UserAction.DeleteNode);
            }
           
            
            menu.ShowAsContext();
            e.Use();
        }

        private void ContextCallBack(object o)
        {
            var userAction = (UserAction)o;

            switch (userAction)
            {
                case UserAction.AddState:
                    var stateNode = ScriptableObject.CreateInstance<StateNode>();
                    stateNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 300);
                    stateNode.windowTitle = "State";

                    windows.Add(stateNode);
                   
                    break;
                case UserAction.AddTransitionNode:
                    break;
                case UserAction.CommentNode:
                    var commentNode = ScriptableObject.CreateInstance<CommentNode>();
                    commentNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 150);
                    commentNode.windowTitle = "Comment";

                    windows.Add(commentNode);
                    break;
                case UserAction.DeleteNode:
                    if (selectedNode)
                    {
                        windows.Remove(selectedNode);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}