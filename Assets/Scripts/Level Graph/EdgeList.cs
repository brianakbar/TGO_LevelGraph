namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    
    public class EdgeList : MonoBehaviour {
        [SerializeReference] List<Edge> edges;

        [Space]
        [SerializeField] Editor editor;

        [System.Serializable]
        class Editor {
            public Color edgeColor = new Color(0, 0, 0, 0.8f);
            public bool showWeight = true;
            public Color weightColor = new Color(255, 255, 255, 0.4f);
            public int weightSize;
        }

        public void Add(Edge edge) {
            if(edges.Contains(edge)) return;

            edges.Add(edge);
            edge.GetSource().onBeforeDelete = OnBeforeVertexDelete;
            edge.GetTarget().onBeforeDelete = OnBeforeVertexDelete;

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public void Clear() {
            edges.Clear();

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public IEnumerable<Edge> GetEdges() {
            return edges;
        }

        public void PreserveEdges(List<Edge> edgesToPreserve) {
            List<Edge> newEdges = new List<Edge>();
            foreach(Edge edge in edges) {
                if(edgesToPreserve.Contains(edge)) {
                    newEdges.Add(edge);
                }
                else {
                    edge.GetSource().RemoveEdge(edge);
                    edge.GetTarget().RemoveEdge(edge);
                }
            }
            edges = newEdges;

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        public float GetAverageLength() {
            return GetTotalLength() / edges.Count;
        }

        public float GetTotalLength() {
            float total = 0;
            foreach(Edge edge in edges) {
                total += edge.GetLength();
            }
            return total;
        }

        void OnBeforeVertexDelete(Vertex vertexToDelete) {
            edges.RemoveAll((edge) => {
                if(edge.GetSource() == vertexToDelete) {
                    edge.GetTarget().RemoveEdge(edge);
                    return true;
                }
                if(edge.GetTarget() == vertexToDelete) {
                    edge.GetSource().RemoveEdge(edge);
                    return true;
                }
                return false;
            });

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void OnDrawGizmos() {
            Gizmos.color = editor.edgeColor;
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = editor.weightColor;
            style.fontSize = editor.weightSize;
            foreach(Edge edge in edges) {
                Gizmos.DrawLine(edge.GetSourcePosition(), edge.GetTargetPosition());
                if(!editor.showWeight) continue;
                
                Vector3 weightPosition = (edge.GetSourcePosition() + edge.GetTargetPosition())/2;
                Handles.Label(weightPosition, edge.GetWeight().ToString(), style);
            }
        }
    }
}
