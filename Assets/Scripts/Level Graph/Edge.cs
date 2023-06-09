namespace LevelGraph {
    using UnityEngine;

    [System.Serializable]
    public class Edge {
        [SerializeField] Vertex source;
        [SerializeField] Vertex target;
        [SerializeField] int weight = 1;

        public Edge(Vertex source, Vertex target, int weight = 1) {
            this.source = source;
            this.target = target;
            this.weight = weight;
        }

        public Vertex GetSource() {
            return source;
        }

        public Vertex GetTarget() {
            return target;
        }

        public Vector3 GetSourcePosition() {
            if(source == null) return default;
            return source.transform.position;
        }

        public Vector3 GetTargetPosition() {
            if(target == null) return default;
            return target.transform.position;
        }

        public int GetWeight() {
            return weight;
        }

        public void SetWeight(int newWeight) {
            weight = newWeight;
        }

        public float GetLength() {
            return Vector3.Distance(source.transform.position, target.transform.position);
        }
    }
}