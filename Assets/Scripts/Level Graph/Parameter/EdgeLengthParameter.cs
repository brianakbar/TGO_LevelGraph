namespace LevelGraph.Parameter {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Edge Length Parameter", menuName = "Edge Generator/Edge Length Parameter", order = 0)]
    public class EdgeLengthParameter : EdgeGeneratorParameter {
        [SerializeField] [Min(0)] float min = 0f;
        [SerializeField] [Min(0)] float max = 5f;

        public override bool Check(Edge edge) {
            float length = edge.GetLength();
            if(min <= length && length <= max) {
                return true;
            }
            return false;
        }
    }
}