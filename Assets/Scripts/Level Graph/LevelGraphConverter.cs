namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class LevelGraphConverter : MonoBehaviour {
        [SerializeField] LevelGraph levelGraph;

        public void ConvertTo(LevelGraph levelGraph) {
            if(TryGetComponent<EdgeList>(out EdgeList edgeList)) {
                List<LevelGraphVertex> vertices = new List<LevelGraphVertex>();
                List<LevelGraphEdge> edges = new List<LevelGraphEdge>();
                foreach(Edge edge in edgeList.GetEdges()) {
                    var source = ConvertFromVertex(edge.GetSource());
                    var target = ConvertFromVertex(edge.GetTarget());
                    var find = vertices.Find((vertex) => vertex.Equals(source));
                    if(find != null) { source = find; }
                    else { vertices.Add(source); }
                    find = vertices.Find((vertex) => vertex.Equals(target));
                    if(find != null) {  target = find;  }
                    else { vertices.Add(target); }
                    edges.Add(ConvertFromEdge(edge, source.GetID(), target.GetID()));
                }
                this.levelGraph = levelGraph;
                this.levelGraph.ConvertTo(vertices, edges);
            }
        }

        public void Open(LevelGraph levelGraph) {

        }

        LevelGraphVertex ConvertFromVertex(Vertex vertex) {
            return new LevelGraphVertex(vertex.transform.localPosition);
        }

        LevelGraphEdge ConvertFromEdge(Edge edge, string source, string target) {
            return new LevelGraphEdge(source, target, edge.GetWeight());
        }
    }
}