namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Vertex : MonoBehaviour {
        [SerializeReference] List<Edge> edges = new List<Edge>();

        [Header("Editor")]
        [SerializeField] bool showGizmos = true;
        [SerializeField] Color gizmosColor = Color.red;
        [SerializeField] float gizmosEdgeLength = 5f;

        public void Connect(Edge connectedEdge) {
            edges.Add(connectedEdge);
        }

        public void RemoveAllEdges() {
            edges.Clear();
        }

        public int GetDegrees() {
            return edges.Count;
        }

        //EDITOR
        
        public void SetGizmosColor(Color color) {
            gizmosColor = color;
        }

        public void SetGizmosEdgeLength(float length) {
            gizmosEdgeLength = length;
        }

        public void SetShowGizmos(bool value) {
            showGizmos = value;
        }

        public void OnDrawGizmosSelected() {
            if(!showGizmos) return;

            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(transform.position, gizmosEdgeLength);
        }
    }
}
