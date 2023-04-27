namespace LevelGraph.Parameter {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Vertex Degree Parameter", menuName = "Edge Generator/Vertex Degree Parameter", order = 0)]
    public class VertexDegreeParameter : EdgeGeneratorParameter {
        [SerializeField] [Min(0)] int max = 5;

        public override bool Check(Edge edge) {
            if(edge.GetSource().GetDegrees() >= max) {
                return false;
            }
            if(edge.GetTarget().GetDegrees() >= max) {
                return false;
            }
            return true;
        }
    }
}