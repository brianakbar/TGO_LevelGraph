namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Vertex : MonoBehaviour {
        [SerializeReference] List<Edge> edges = new List<Edge>();

        [Space]
        [SerializeField] Editor editor;

        [System.Serializable]
        class Editor {
            public bool showGizmos = true;
            public Color gizmosColor = Color.red;
            public float gizmosEdgeLength = 5f;
        }

        public void Connect(Edge connectedEdge) {
            edges.Add(connectedEdge);
        }

        public void RemoveAllEdges() {
            edges.Clear();
        }

        public int GetDegrees() {
            return edges.Count;
        }

        public IEnumerable<Edge> GetEdges() {
            return edges;
        }

        public void Copy(Vertex vertexToCopy) {
            edges = vertexToCopy.edges;
            editor = vertexToCopy.editor;
        }

        //EDITOR
        
        public void SetGizmosColor(Color color) {
            editor.gizmosColor = color;
        }

        public void SetGizmosEdgeLength(float length) {
            editor.gizmosEdgeLength = length;
        }

        public void SetShowGizmos(bool value) {
            editor.showGizmos = value;
        }

        public void OnDrawGizmosSelected() {
            if(!editor.showGizmos) return;

            Gizmos.color = editor.gizmosColor;
            Gizmos.DrawWireSphere(transform.position, editor.gizmosEdgeLength);
        }
    }
}
