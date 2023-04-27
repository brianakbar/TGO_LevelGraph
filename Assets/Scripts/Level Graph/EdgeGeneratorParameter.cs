namespace LevelGraph {
    using UnityEngine;
    
    public abstract class EdgeGeneratorParameter : ScriptableObject {
        public abstract bool Check(Edge edge);
        public virtual void DrawGizmos(GameObject gameObject) { }
    }
}