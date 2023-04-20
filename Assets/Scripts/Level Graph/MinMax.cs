namespace LevelGraph {
    using UnityEngine;

    [System.Serializable]
    public class MinMax {
        [SerializeField] [Min(0)] float min;
        [SerializeField] [Min(0)] float max;

        public float Min { get => min; }
        public float Max { get => max; }
    }
}