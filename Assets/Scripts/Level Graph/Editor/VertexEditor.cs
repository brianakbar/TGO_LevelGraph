namespace LevelGraph.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Vertex))]
    public class VertexEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            Vertex vertex = (Vertex)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Delete", guiLayoutOptions)) {
                vertex.Delete();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}