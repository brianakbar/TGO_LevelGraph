namespace LevelGraph {
    using UnityEngine;
    
    public class Edge : MonoBehaviour {
        [SerializeField] Vertex source;
        [SerializeField] Vertex target;

        public void Connect(Vertex source, Vertex target) {
            this.source = source;
            this.target = target;
        }
    }
}
