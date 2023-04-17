namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    [ExecuteInEditMode]
    public class EdgeGenerator : MonoBehaviour {
        [SerializeField] Vertex seedVertex;
        [SerializeField] float maxEdgeLength = 5f;

        [Header("Editor")]
        [SerializeField] bool showGizmos = true;
        [SerializeField] Color edgeColor = new Color(0, 0, 0, 0.8f);
        [SerializeField] Color gizmosColor = Color.red;
        [SerializeField] string edgeContainerTag = "EdgeContainer";

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
            // for(int i = transform.childCount; i > 0; --i) {
            //     Transform child = transform.GetChild(i - 1);
            //     if (child.tag != edgeContainerTag) continue;

            //     DestroyImmediate(child.gameObject);
            // }
            // GameObject edgeContainer = CreateEdgeContainer();
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

                        seedVertex.Connect(target);
                        target.Connect(seedVertex);

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
                if(candidate == source) continue;
                float distance = Vector3.Distance(source.transform.position, candidate.transform.position);
                if(distance > maxEdgeLength) continue;

                neighbors.Add(candidate);
            }
            return neighbors;
        }

        GameObject CreateEdgeContainer() {
            GameObject edgeContainer = new GameObject("Edges");
            edgeContainer.transform.parent = transform;
            edgeContainer.tag = edgeContainerTag;
            return edgeContainer;
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

                vertex.SetEdgeColor(edgeColor);
                vertex.SetGizmosColor(gizmosColor);
                vertex.SetGizmosEdgeLength(maxEdgeLength);
                vertex.SetShowGizmos(showGizmos);
            }
        }
    }
}
