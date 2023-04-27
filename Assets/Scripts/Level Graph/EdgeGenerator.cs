namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    [ExecuteInEditMode]
    public class EdgeGenerator : MonoBehaviour {
        [SerializeField] Vertex seedVertex;
        [SerializeField] List<EdgeGeneratorParameter> parameters;
        [SerializeField] [Min(0)] int maxNearestNeighborSize = 3;

        [Space]
        [SerializeField] Editor editor;

        EdgeList edgeList;

        [System.Serializable]
        class Editor {
            public bool showGizmos = true;
            public Color gizmosColor = new Color(255, 0, 0, 0.3f);
        }

        public void RegenerateEdges() {
            if(edgeList == null) {
                edgeList = GetComponent<EdgeList>();
            }
            edgeList.Clear();
            List<Vertex> unconnected = RebuildVerticesList();
            if(seedVertex == null) seedVertex = SelectRandomVertex(unconnected);
            else if(!unconnected.Contains(seedVertex)) seedVertex = SelectRandomVertex(unconnected);
            Queue<Vertex> traversal = new Queue<Vertex>();
            traversal.Enqueue(seedVertex);

            while(traversal.Count > 0) {
                seedVertex = traversal.Dequeue();
                if(unconnected.Contains(seedVertex)) {
                    unconnected.Remove(seedVertex);
                    List<Vertex> neighbors = GetNeighbors(seedVertex, unconnected);
                    while(neighbors.Count > 0) {
                        Vertex target = SelectRandomVertex(neighbors);

                        Edge edge = new Edge(seedVertex, target);
                        if(CheckParameters(edge)) {    
                            edgeList.Add(edge);
                            seedVertex.Connect(edge);
                            target.Connect(edge);
                        }

                        neighbors.Remove(target);
                        traversal.Enqueue(target);
                    }
                }

                if(traversal.Count == 0 && unconnected.Count > 0) traversal.Enqueue(unconnected[0]); 
            }
        }

        List<Vertex> GetNeighbors(Vertex source, List<Vertex> candidates) {
            List<Vertex> neighbors = new List<Vertex>();
            foreach(Vertex candidate in candidates) {
                if(neighbors.Count >= maxNearestNeighborSize) break;
                if(candidate == source) continue;
                Edge edge = new Edge(source, candidate);
                if(!CheckParameters(edge)) continue;

                neighbors.Add(candidate);
            }
            return neighbors;
        }

        List<Vertex> RebuildVerticesList() {
            List<Vertex> vertices = new List<Vertex>();
            foreach(Transform child in transform) {
                if(!child.TryGetComponent(out Vertex vertex)) continue;

                vertex.RemoveAllEdges();
                vertices.Add(vertex);
            }
            return vertices;
        }

        bool CheckParameters(Edge edge) {
            foreach(EdgeGeneratorParameter parameter in parameters) {
                if(!parameter.Check(edge)) return false;
            }
            return true;
        }

        Vertex SelectRandomVertex(List<Vertex> vertices) {
            return vertices[Random.Range(0, vertices.Count)];
        }

        void OnDrawGizmosSelected() {
            if(!editor.showGizmos) return;

            Gizmos.color = editor.gizmosColor;
            foreach(EdgeGeneratorParameter parameter in parameters) {
                parameter.DrawGizmos(gameObject);
            }
        }
    }
}
