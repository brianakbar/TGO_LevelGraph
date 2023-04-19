namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    [ExecuteInEditMode]
    public class EdgeGenerator : MonoBehaviour {
        [SerializeField] Vertex seedVertex;
        [SerializeField] [Min(0)] float maxEdgeLength = 5f;
        [SerializeField] [Min(0)] int maxNearestNeighborSize = 3;
        [SerializeField] [Min(0)] int maxVertexDegree = 3;

        [Header("Editor")]
        [SerializeField] bool showGizmos = true;
        [SerializeField] Color gizmosColor = Color.red;

        EdgeList edgeList;

        void OnEnable() {
            GetComponent<VertexGenerator>().onVertexGenerated += SetVerticesGizmos;
        }

        void OnDisable() {
            GetComponent<VertexGenerator>().onVertexGenerated -= SetVerticesGizmos;
        }

        void OnValidate() {
            SetVerticesGizmos();
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

                        if(seedVertex.GetDegrees() < maxVertexDegree) {  
                            Edge edge = new Edge(seedVertex, target);
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
                if(candidate.GetDegrees() >= maxVertexDegree) continue;
                float distance = Vector3.Distance(source.transform.position, candidate.transform.position);
                if(distance > maxEdgeLength) continue;

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

        Vertex SelectRandomVertex(List<Vertex> vertices) {
            return vertices[Random.Range(0, vertices.Count)];
        }

        void SetVerticesGizmos() {
            foreach(Transform child in transform) {
                if(!child.TryGetComponent(out Vertex vertex)) continue;

                vertex.SetGizmosColor(gizmosColor);
                vertex.SetGizmosEdgeLength(maxEdgeLength);
                vertex.SetShowGizmos(showGizmos);
            }
        }
    }
}
