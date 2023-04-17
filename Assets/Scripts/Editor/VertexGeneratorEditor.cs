namespace LevelGraph.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(VertexGenerator))]
    public class VertexGeneratorEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            VertexGenerator vertexGenerator = (VertexGenerator)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Generate", guiLayoutOptions)) {
                vertexGenerator.RegenerateVertices();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}