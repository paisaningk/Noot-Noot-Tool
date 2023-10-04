using UnityEngine;

namespace BehaviorToolTest1.BehaviorEditor.Node
{
    public class CommentNode : BaseNode
    {
        private string comment = "This is a Comment";

        public override void DrawWindow()
        {
            comment = GUILayout.TextArea(comment, 200);
        }
    }
}