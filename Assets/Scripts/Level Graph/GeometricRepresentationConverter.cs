namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    using Parabox.CSG;

    [ExecuteInEditMode]
    public class GeometricRepresentationConverter : MonoBehaviour {
        // [SerializeField] List<GameObject> walls;
        // [SerializeField] List<GameObject> floors;
        [SerializeField] List<GameObject> rooms;
        [SerializeField] List<GameObject> corridors;
        [SerializeField] List<GameObject> doors;
        [SerializeField] VertexRepresentation vertexRepresentation;
        [SerializeField] EdgeRepresentation edgeRepresentation;
        [SerializeField] float scale = 1f;

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
            public float step = 1f;
        }

        void Awake() {
            edgeList = GetComponent<EdgeList>();
        }

        public void Convert() {
            GameObject level = new GameObject("Level");
            foreach(Transform child in transform) {
                if(child.TryGetComponent<Vertex>(out Vertex vertex)) {
                    // GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    // box.transform.position = vertex.transform.position;
                    // box.transform.localScale = vertexRepresentation.size;
                    GameObject room = Instantiate(PickRandomGameObject(rooms), vertex.transform.position, 
                                                    Quaternion.identity, level.transform);
                    room.transform.localScale *= scale;
                }
            }
            foreach(Edge edge in edgeList.GetEdges()) {
                Vector3 source = edge.GetTargetPosition();
                Vector3 target = edge.GetSourcePosition();
                int times = (int)(Vector3.Distance(target, source) / edgeRepresentation.step);
                for(int i = 0; i < times; i++) {
                    Quaternion rotation = Quaternion.LookRotation(target - source, Vector3.up);
                    GameObject corridor = Instantiate(PickRandomGameObject(corridors), source, 
                                                        rotation, level.transform);
                    corridor.transform.localPosition += corridor.transform.rotation * Vector3.forward * edgeRepresentation.step * i;
                    corridor.transform.localScale *= scale;
                }
                
            }
        }

        // List<GameObject> CreateCSGGameObject(List<GameObject> gameObjects) {
        //     List<GameObject> CSGGameObjects = new List<GameObject>();
        //     foreach(GameObject gameObject in gameObjects) {
        //         GameObject prefabInstance = Instantiate(gameObject);
        //         GameObject gameObjectUnion = null;
        //         GameObject firstGameObject = null;
        //         MeshFilter meshFilter = null;
        //         MeshRenderer meshRenderer = null;
        //         foreach(MeshFilter filter in prefabInstance.GetComponentsInChildren<MeshFilter>()) {
        //             if(gameObjectUnion == null) {
        //                 gameObjectUnion = new GameObject();
        //                 meshFilter = gameObjectUnion.AddComponent<MeshFilter>();
        //                 meshRenderer = gameObjectUnion.AddComponent<MeshRenderer>();
        //                 firstGameObject = filter.gameObject;
        //                 continue;
        //             }
        //             Model result;
        //             if(firstGameObject != null) {
        //                 result = CSG.Union(firstGameObject, filter.gameObject);
        //                 firstGameObject = null;
        //             }
        //             else {
        //                 result = CSG.Union(gameObjectUnion, filter.gameObject);
        //             }

        //             meshFilter.sharedMesh = result.mesh;
        //             meshRenderer.sharedMaterials = result.materials.ToArray();
        //         }
        //         CSGGameObjects.Add(gameObjectUnion);
        //         DestroyImmediate(prefabInstance);
        //     }
        //     return CSGGameObjects;
        // }

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