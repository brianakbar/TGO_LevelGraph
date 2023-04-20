namespace LevelGraph.Editor {
    using System;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(LevelGraphConverter))]
    public class LevelGraphConverterEditor : Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            LevelGraphConverter levelGraphConverter = (LevelGraphConverter)target;

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayoutOption[] guiLayoutOptions = {
                GUILayout.MaxWidth(120),
                GUILayout.MinHeight(25)
            };
            if(GUILayout.Button("Convert", guiLayoutOptions)) {
                levelGraphConverter.ConvertTo(SaveAsset(typeof(LevelGraph), "Assets"));
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public LevelGraph SaveAsset(Type type, string path) {
            path = EditorUtility.SaveFilePanelInProject("Save Level Graph", type.Name + ".asset", "asset", "Enter a file name for the Level Graph.", path);
			if (path == "") return null;
			ScriptableObject asset = ScriptableObject.CreateInstance(type);
			AssetDatabase.CreateAsset(asset, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
			EditorGUIUtility.PingObject(asset);
			return asset as LevelGraph;
        }
    }
}