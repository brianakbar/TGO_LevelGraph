namespace LevelGraph {
    using System;
    using UnityEngine;

    [System.Serializable]
    public class LevelGraphVertex {
        [SerializeField] Vector3 position;
        [SerializeField] string id;

        public LevelGraphVertex(Vector3 position) {
            this.id = Guid.NewGuid().ToString();
            this.position = position;
        }

        public string GetID() {
            return id;
        }

        public Vector3 GetPosition() {
            return position;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            LevelGraphVertex toCompare = obj as LevelGraphVertex;
            return position.Equals(toCompare.position);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}