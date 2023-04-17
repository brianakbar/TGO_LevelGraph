namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    public class Vertex : MonoBehaviour {
        [SerializeField] List<Vertex> connectedVertices = new List<Vertex>();

        [Header("Editor")]
        [SerializeField] bool showGizmos = true;
        [SerializeField] Color edgeColor = new Color(0, 0, 0, 0.8f);
        [SerializeField] Color gizmosColor = Color.red;
        [SerializeField] float gizmosEdgeLength = 5f;

        public void Connect(Vertex connectTo) {
            connectedVertices.Add(connectTo);
        }

        public void RemoveAllEdges() {
            connectedVertices.Clear();
        }

        public int GetDegrees() {
            return connectedVertices.Count;
        }

        //EDITOR

        public void SetEdgeColor(Color color) {
            edgeColor = color;
        }

        public void SetGizmosColor(Color color) {
            gizmosColor = color;
        }

        public void SetGizmosEdgeLength(float length) {
            gizmosEdgeLength = length;
        }

        public void SetShowGizmos(bool value) {
            showGizmos = value;
        }

        void OnDrawGizmos() {
            Gizmos.color = edgeColor;
            foreach(Vertex vertex in connectedVertices) {
                Gizmos.DrawLine(transform.position, vertex.transform.position);
            }
        }

        public void OnDrawGizmosSelected() {
            if(!showGizmos) return;

            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(transform.position, gizmosEdgeLength);
        }
    }
}
