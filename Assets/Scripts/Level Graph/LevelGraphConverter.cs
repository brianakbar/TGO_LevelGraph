namespace LevelGraph {
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class LevelGraphConverter : MonoBehaviour {
        [SerializeField] LevelGraph levelGraph;

        [Space]
        [SerializeField] Editor editor;

        [Serializable]
        class Editor {
            public Vertex vertexPrefab;
        }

        public void ConvertTo(LevelGraph levelGraph) {
            if(levelGraph == null) return;
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

        public void Unpack() {
            if(levelGraph == null) return;

            DestroyVertices();
            Dictionary<string, Vertex> vertexKey = new Dictionary<string, Vertex>();
            foreach(LevelGraphVertex vertex in levelGraph.GetVertices()) {
                Vertex instance = Instantiate(editor.vertexPrefab, 
                                                transform.position + vertex.GetPosition(), 
                                                Quaternion.identity, transform);
                vertexKey.Add(vertex.GetID(), instance);
            }
            foreach(LevelGraphEdge levelGraphEdge in levelGraph.GetEdges()) {
                if(TryGetComponent<EdgeList>(out EdgeList edgeList)) {
                    Vertex source = vertexKey[levelGraphEdge.GetSource()];
                    Vertex target = vertexKey[levelGraphEdge.GetTarget()];
                    Edge edge = new Edge(source, target, levelGraphEdge.GetWeight());
                    source.Connect(edge);
                    target.Connect(edge);
                    edgeList.Add(edge);
                }
            }
        }

        void DestroyVertices() {
            if(TryGetComponent<EdgeList>(out EdgeList edgeList)) {
                edgeList.Clear();
            }
            for (int i = transform.childCount; i > 0; --i) {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        LevelGraphVertex ConvertFromVertex(Vertex vertex) {
            return new LevelGraphVertex(vertex.transform.localPosition);
        }

        LevelGraphEdge ConvertFromEdge(Edge edge, string source, string target) {
            return new LevelGraphEdge(source, target, edge.GetWeight());
        }
    }
}