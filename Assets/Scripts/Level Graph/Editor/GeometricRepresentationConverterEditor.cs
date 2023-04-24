namespace LevelGraph.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GeometricRepresentationConverter))]
    public class GeometricRepresentationConverterEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            GeometricRepresentationConverter geometricRepresentationConverter = (GeometricRepresentationConverter)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Convert", guiLayoutOptions)) {
                geometricRepresentationConverter.Convert();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}