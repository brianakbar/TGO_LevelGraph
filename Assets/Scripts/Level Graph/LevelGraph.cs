namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class LevelGraph : ScriptableObject {
        [SerializeField] List<LevelGraphVertex> vertices;
        [SerializeField] List<LevelGraphEdge> edges;

#if UNITY_EDITOR
        public void ConvertTo(List<LevelGraphVertex> vertices, List<LevelGraphEdge> edges) {
            this.vertices = vertices;
            this.edges = edges;
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public IEnumerable<LevelGraphVertex> GetVertices() {
            return vertices;
        }

        public IEnumerable<LevelGraphEdge> GetEdges() {
            return edges;
        }
#endif

    }
}