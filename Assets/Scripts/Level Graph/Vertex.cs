namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Vertex : MonoBehaviour {
        [SerializeReference] List<Edge> edges = new List<Edge>();

        public void Connect(Edge connectedEdge) {
            edges.Add(connectedEdge);
        }

        public void RemoveEdge(Edge edge) {
            edges.Remove(edge);
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
        }
    }
}
