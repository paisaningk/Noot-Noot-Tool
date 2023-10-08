using System.Collections.Generic;
using System.Linq;
using BehaviorToolTest1.BehaviorEditor.Node;
using UnityEditor;
using UnityEngine;

namespace BehaviorToolTest1.BehaviorEditor
{
    public class BehaviorEditor : EditorWindow
    {
        #region Variables

        private static List<BaseNode> windows = new();
        private Vector2 mousePosition;
        private bool makeTransition;
        private bool clickedOnWindow;
        private BaseNode selectedNode;
        private int selectedIndex;

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
            selectedIndex = -1;
            selectedNode = null;
            foreach (var node in windows.Where(node => node.windowRect.Contains(e.mousePosition)))
            {
                clickedOnWindow = true;
                selectedNode = node;
                selectedIndex = windows.IndexOf(node);
                break;
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
            if (selectedNode is StateNode stateNode)
            {
                if (stateNode.currentState)
                {
                    menu.AddItem(new GUIContent("Add TransitionNode"),false, ContextCallBack, UserAction.AddTransitionNode);
                }
                else
                {
                    menu.AddDisabledItem(new GUIContent("Add TransitionNode"));
                }
               
                menu.AddItem(new GUIContent("Delete Node"),false, ContextCallBack, UserAction.DeleteNode);
            }
            if (selectedNode is CommentNode)
            {
                menu.AddItem(new GUIContent("Delete Node"),false, ContextCallBack, UserAction.DeleteNode);
            }
            if (selectedNode is TransitionNode)
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
                    var stateNode = CreateInstance<StateNode>();
                    stateNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 300);
                    stateNode.windowTitle = "State";

                    windows.Add(stateNode);
                    break;
                case UserAction.AddTransitionNode:
                    if (selectedNode is StateNode form)
                    {
                        var transition = form.AddTransition();
                        form.referencesNodesList.Add(AddTransitionNode(form.currentState.transitionList.Count, transition, form));
                    }
                    break;
                case UserAction.CommentNode:
                    var commentNode = CreateInstance<CommentNode>();
                    commentNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 150);
                    commentNode.windowTitle = "Comment";

                    windows.Add(commentNode);
                    break;
                case UserAction.DeleteNode:
                    switch (selectedNode)
                    {
                        case StateNode state:
                            state.ClearReferences();
                            windows.Remove(selectedNode);
                            break;
                        case TransitionNode transitionNode:
                            if (transitionNode.enterState.currentState.transitionList.Contains(transitionNode.targetTransition))
                            {
                                transitionNode.enterState.currentState.transitionList.Remove(transitionNode
                                    .targetTransition);
                            }
                            windows.Remove(selectedNode);
                            break;
                        case CommentNode:
                            windows.Remove(selectedNode);
                            break;
                    }

                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Helper Methods

        public static TransitionNode AddTransitionNode(int index, Transition transition, StateNode from)
        {
            var fromRect = from.windowRect;
            fromRect.x += 50;
            
            var targetY = fromRect.y - fromRect.height;
            
            if (from.currentState)
            {
                targetY += (index * 100);
            }

            fromRect.y = targetY;
            
            var transitionNode = CreateInstance<TransitionNode>();
            transitionNode.Init(from, transition);
            transitionNode.windowRect = new Rect(fromRect.x + 200 + 100, fromRect.y + (fromRect.height * 0.7f), 200, 80);
            transitionNode.windowTitle = "Condition Check";
            
            windows.Add(transitionNode);
            
            return transitionNode;
        }
           
        

        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
        {
            var startPos = new Vector3(
                (left) ? start.x + start.width : start.x, start.y + (start.height * 0.5f));

            var endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f));
            var startTan = startPos + Vector3.right * 50;
            var endTan = endPos + Vector3.left * 50;
            
            var shadow = new Color(0, 0, 0, 0.06f);

            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos,endPos,startTan,endTan,shadow, null, (i + 1) * 0.5f);
            }
            
            Handles.DrawBezier(startPos,endPos,startTan,endTan,curveColor, null, 1);
        }

        public static void ClearWindowsFromList(List<BaseNode> list)
        {
            foreach (var baseNode in list.Where(baseNode => windows.Contains(baseNode)))
            {
                windows.Remove(baseNode);
            }
        }

        #endregion
    }
}