namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class EdgeList : MonoBehaviour {
        [SerializeReference] List<Edge> edges;

        [Header("Editor")]
        [SerializeField] Color edgeColor = new Color(0, 0, 0, 0.8f);

        public void Add(Edge edge) {
            if(edges.Contains(edge)) return;

            edges.Add(edge);
        }

        public void Clear() {
            edges.Clear();
        }

        void OnDrawGizmos() {
            Gizmos.color = edgeColor;
            foreach(Edge edge in edges) {
                Gizmos.DrawLine(edge.GetSourcePosition(), edge.GetTargetPosition());
            }
        }
    }
}
