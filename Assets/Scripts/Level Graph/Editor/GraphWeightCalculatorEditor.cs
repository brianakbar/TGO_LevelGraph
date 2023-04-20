namespace LevelGraph.Editor {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GraphWeightCalculator))]
    public class GraphWeightCalculatorEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            GraphWeightCalculator graphWeightCalculator = (GraphWeightCalculator)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Calculate", guiLayoutOptions)) {
                graphWeightCalculator.CalculateAllEdgeWeight();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}