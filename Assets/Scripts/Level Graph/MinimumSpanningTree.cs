namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    using Utils;

    [ExecuteInEditMode]
    public class MinimumSpanningTree : MonoBehaviour {
        [SerializeField] Vertex startingVertex;

        public void Compute() {
            List<Vertex> unvisited = RebuildVerticesList();
            if(startingVertex == null) startingVertex = SelectRandomVertex(unvisited);
            else if(!unvisited.Contains(startingVertex)) startingVertex = SelectRandomVertex(unvisited);
            List<Vertex> visitedVertices = new List<Vertex>();
            List<Edge> visitedEdges = new List<Edge>();
            PriorityQueue<Edge, int> priority = new PriorityQueue<Edge, int>();

            while(unvisited.Count > 0) {
                if(visitedVertices.Contains(startingVertex)) break;
                visitedVertices.Add(startingVertex);
                foreach(Edge incidentEdge in startingVertex.GetEdges()) {
                    priority.Enqueue(incidentEdge, incidentEdge.GetWeight());
                }
                while(priority.Count > 0) {
                    Edge minimal = priority.Dequeue();
                    Vertex source = minimal.GetSource();
                    Vertex target = minimal.GetTarget();
                    if(visitedVertices.Contains(source) && visitedVertices.Contains(target)) continue;
                    if(visitedVertices.Contains(source)) startingVertex = target;
                    else startingVertex = source;
                    visitedEdges.Add(minimal);
                    break;
                }
            }

            PreserveVertices(visitedVertices);
            GetComponent<EdgeList>().PreserveEdges(visitedEdges);
        }

        void PreserveVertices(List<Vertex> verticesToPreserve) {
            for (int i = transform.childCount; i > 0; --i) {
                Vertex child = transform.GetChild(i-1).GetComponent<Vertex>();
                if(!verticesToPreserve.Contains(child)) {
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        Vertex SelectRandomVertex(List<Vertex> vertices) {
            return vertices[Random.Range(0, vertices.Count)];
        }

        List<Vertex> RebuildVerticesList() {
            List<Vertex> vertices = new List<Vertex>();
            foreach(Transform child in transform) {
                if(!child.TryGetComponent(out Vertex vertex)) continue;

                vertices.Add(vertex);
            }
            return vertices;
        }
    }
}
