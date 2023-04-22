namespace LevelGraph.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(LevelGraphMerger))]
    public class LevelGraphMergerEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            LevelGraphMerger levelGraphMerger = (LevelGraphMerger)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Merge", guiLayoutOptions)) {
                levelGraphMerger.Merge();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}