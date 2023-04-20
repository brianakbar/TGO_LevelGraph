namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    
    public class EdgeList : MonoBehaviour {
        [SerializeReference] List<Edge> edges;

        [Space]
        [SerializeField] Editor editor;

        [System.Serializable]
        class Editor {
            public Color edgeColor = new Color(0, 0, 0, 0.8f);
            public bool showWeight = true;
            public Color weightColor = new Color(255, 255, 255, 0.4f);
            public int weightSize;
        }

        public void Add(Edge edge) {
            if(edges.Contains(edge)) return;

            edges.Add(edge);
        }

        public void Clear() {
            edges.Clear();
        }

        public IEnumerable<Edge> GetEdges() {
            return edges;
        }

        public float GetAverageLength() {
            return GetTotalLength() / edges.Count;
        }

        public float GetTotalLength() {
            float total = 0;
            foreach(Edge edge in edges) {
                total += edge.GetLength();
            }
            return total;
        }

        void OnDrawGizmos() {
            Gizmos.color = editor.edgeColor;
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = editor.weightColor;
            style.fontSize = editor.weightSize;
            foreach(Edge edge in edges) {
                Gizmos.DrawLine(edge.GetSourcePosition(), edge.GetTargetPosition());
                if(!editor.showWeight) continue;
                
                Vector3 weightPosition = (edge.GetSourcePosition() + edge.GetTargetPosition())/2;
                Handles.Label(weightPosition, edge.GetWeight().ToString(), style);
            }
        }
    }
}
