namespace LevelGraph {
    using UnityEngine;

    [System.Serializable]
    public class LevelGraphEdge {
        [SerializeField] int weight = 1;
        [SerializeField] string source;
        [SerializeField] string target;

        public LevelGraphEdge(string source, string target, int weight = 1) {
            this.source = source;
            this.target = target;
            this.weight = weight;
        }

        public string GetSource() {
            return source;
        }

        public string GetTarget() {
            return target;
        }

        public int GetWeight() {
            return weight;
        }
    }
}