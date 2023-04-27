namespace LevelGraph.Parameter {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Axis Angle Parameter", menuName = "Edge Generator/Axis Angle Parameter", order = 0)]
    public class AxisAngleParameter : EdgeGeneratorParameter {
        [SerializeField] [Range(0, 45)] float min = 0f;
        [SerializeField] [Range(0, 45)] float max = 0f;

        public override bool Check(Edge edge) {
            Vector3 edgeVector = edge.GetTarget().transform.position - edge.GetSource().transform.position;
            
            Vector3[] axes = {Vector3.right, Vector3.left, Vector3.up, Vector3.down, Vector3.forward, Vector3.back};
            foreach(Vector3 axis in axes) {
                float angle = Vector3.Angle(edgeVector, axis);
                if((min - Mathf.Epsilon) <= angle && angle <= (max + Mathf.Epsilon)) {
                    return true;
                }
            }

            return false;
        }
    }
}