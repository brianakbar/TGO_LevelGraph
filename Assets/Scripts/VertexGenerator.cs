namespace LevelGraph {
    using System;
    using UnityEngine;

    public class VertexGenerator : MonoBehaviour {
        [SerializeField] Vector3 size;
        [SerializeField] Vector3 gridResolution;
        [SerializeField] [Range(0f, 1f)] float instantiationProbability = 0.5f;

        [Header("Editor")]
        [SerializeField] bool showGizmos = true;
        [SerializeField] Color gizmosColor = new Color(255, 255, 255, 0.2f);
        [SerializeField] Vertex vertexPrefab;

        public event Action onVertexGenerated;

        Vector3 jitterSize;

        void OnValidate() {
            float jitterSizeX = size.x / gridResolution.x;
            float jitterSizeY = size.y / gridResolution.y;
            float jitterSizeZ = size.z / gridResolution.z;
            jitterSize = new Vector3(jitterSizeX, jitterSizeY, jitterSizeZ);
        }

        void OnDrawGizmosSelected() {
            if(!showGizmos) return;

            Gizmos.color = gizmosColor;
            for(int x = 0; x < gridResolution.x; x++) {
                for(int y = 0; y < gridResolution.y; y++) {
                    for(int z = 0; z < gridResolution.z; z++) {
                        
                        Gizmos.DrawWireCube(GetVertexPosition(x, y, z, jitterSize), jitterSize);
                    }
                }
            }
        }

        public void RegenerateVertices() {
            for(int i = transform.childCount; i > 0; --i) {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            for(int x = 0; x < gridResolution.x; x++) {
                for(int y = 0; y < gridResolution.y; y++) {
                    for(int z = 0; z < gridResolution.z; z++) {
                        if(!(UnityEngine.Random.Range(0f, 1f) <= instantiationProbability)) continue;

                        Instantiate(vertexPrefab, GetVertexPosition(x, y, z, jitterSize), Quaternion.identity, transform);
                    }
                }
            }
            if(onVertexGenerated != null) onVertexGenerated();
        }

        Vector3 GetVertexPosition(int x, int y, int z, Vector3 jitterSize) {
            return transform.position + new Vector3(x * jitterSize.x, y * jitterSize.y, z * jitterSize.z) + jitterSize/2;
        }
    } 
}