namespace LevelGraph {
    using UnityEngine;
    
    public abstract class Penalty : ScriptableObject {
        public abstract void CalculatePenalty(EdgeList edgeList);
    }
}