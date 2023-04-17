namespace LevelGraph.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(EdgeGenerator))]
    public class EdgeGeneratorEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            EdgeGenerator edgeGenerator = (EdgeGenerator)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Generate", guiLayoutOptions)) {
                edgeGenerator.RegenerateEdges();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}