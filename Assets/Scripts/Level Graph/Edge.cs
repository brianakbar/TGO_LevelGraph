using System;
using UnityEngine;

namespace LevelGraph {
    [System.Serializable]
    public class Edge {
        [SerializeField] Vertex source;
        [SerializeField] Vertex target;
        [SerializeField] int weight = 1;

        public Edge(Vertex source, Vertex target) {
            this.source = source;
            this.target = target;
        }

        public Vertex GetSource() {
            return source;
        }

        public Vertex GetTarget() {
            return target;
        }

        public Vector3 GetSourcePosition() {
            return source.transform.position;
        }

        public Vector3 GetTargetPosition() {
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