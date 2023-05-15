namespace CSG.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(CSGTest))]
    public class CSGTestEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            CSGTest csgTest = (CSGTest)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Primitive Substract", guiLayoutOptions)) {
                csgTest.TestPrimitiveSubstract();
            }
            if(GUILayout.Button("Primitive Union", guiLayoutOptions)) {
                csgTest.TestPrimitiveUnion();
            }
            if(GUILayout.Button("Model Union", guiLayoutOptions)) {
                csgTest.TestModelUnion();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
