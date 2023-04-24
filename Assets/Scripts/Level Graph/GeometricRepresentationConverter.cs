namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    using Parabox.CSG;

    [ExecuteInEditMode]
    public class GeometricRepresentationConverter : MonoBehaviour {
        [SerializeField] List<GameObject> walls;
        [SerializeField] List<GameObject> floors;
        [SerializeField] List<GameObject> doors;
        [SerializeField] VertexRepresentation vertexRepresentation;
        [SerializeField] EdgeRepresentation edgeRepresentation;

        [Space]
        [SerializeField] Editor editor;

        EdgeList edgeList;

        [System.Serializable]
        class Editor {
            public bool showGizmos = true;
            public Color gizmosColor = new Color(0, 255, 0, 0.3f);
        }

        [System.Serializable]
        class VertexRepresentation {
            public Vector3 size;
        }

        [System.Serializable]
        class EdgeRepresentation {
            public float width;
            public float height;
        }

        void Awake() {
            edgeList = GetComponent<EdgeList>();
        }

        public void Convert() {
            
        }

        GameObject PickRandomGameObject(List<GameObject> gameObjects) {
            return gameObjects[Random.Range(0, gameObjects.Count)];
        } 

        void OnDrawGizmos() {
            if(!editor.showGizmos) return;
            Gizmos.color = editor.gizmosColor;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            foreach(Transform child in transform) {
                if(child.TryGetComponent<Vertex>(out Vertex vertex)) {
                    Gizmos.DrawWireCube(vertex.transform.position, vertexRepresentation.size);
                }
            }
            foreach(Edge edge in edgeList.GetEdges()) {
                Vector3 source = edge.GetTargetPosition();
                Vector3 target = edge.GetSourcePosition();
                Quaternion rotation = Quaternion.LookRotation(target - source, Vector3.up);
                Gizmos.matrix = Matrix4x4.TRS(source, rotation, Vector3.one);
                float distance = Vector3.Distance(source, target);
                Vector3 center = Vector3.forward * distance / 2;
                Gizmos.DrawWireCube(center, new Vector3(edgeRepresentation.width, edgeRepresentation.height, distance));
            }
            Gizmos.matrix = oldMatrix;
        }
    }
}