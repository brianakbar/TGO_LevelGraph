namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class LevelGraphMerger : MonoBehaviour {
        [SerializeField] LevelGraphConverter mergeInto;
        [SerializeField] List<Edge> edges = new List<Edge>();

        [Space]
        [SerializeField] Editor editor;

        [System.Serializable]
        class Editor {
            public Color edgeColor = new Color(0, 255, 0, 0.3f);
        }

        public void Merge() {
            for(int i = transform.childCount; i > 0; --i) {
                if(transform.GetChild(0).TryGetComponent<Vertex>(out Vertex vertex)) {
                    vertex.transform.SetParent(mergeInto.transform);
                }
            }
            if(mergeInto.TryGetComponent<EdgeList>(out EdgeList targetEdgeList)) {
                if(TryGetComponent<EdgeList>(out EdgeList sourceEdgeList)) {
                    foreach(Edge edge in sourceEdgeList.GetEdges()) {
                        targetEdgeList.Add(edge);
                    }
                }
                foreach(Edge edge in edges) {
                    targetEdgeList.Add(edge);
                }
            }
            DestroyImmediate(gameObject);
        }

        void OnDrawGizmos() {
            Gizmos.color = editor.edgeColor;
            foreach(Edge edge in edges) {
                Vertex source = edge.GetSource();
                Vertex target = edge.GetTarget();
                if(source == null || target == null) continue;
                Gizmos.DrawLine(source.transform.position, target.transform.position);
            }
        }
    }
}