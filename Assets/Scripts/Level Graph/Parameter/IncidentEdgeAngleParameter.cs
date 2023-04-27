namespace LevelGraph.Parameter {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Incident Edge Angle Parameter", menuName = "Edge Generator/Incident Edge Angle Parameter", order = 0)]
    public class IncidentEdgeAngleParameter : EdgeGeneratorParameter {
        [SerializeField] [Range(0, 180)] float min = 0;
        [SerializeField] [Range(0, 180)] float max = 5;

        public override bool Check(Edge edge) {
            
            return true;
        }
    }
}