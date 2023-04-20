namespace LevelGraph.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(MinimumSpanningTree))]
    public class MinimumSpanningTreeEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            MinimumSpanningTree minimumSpanningTree = (MinimumSpanningTree)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Compute", guiLayoutOptions)) {
                minimumSpanningTree.Compute();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}