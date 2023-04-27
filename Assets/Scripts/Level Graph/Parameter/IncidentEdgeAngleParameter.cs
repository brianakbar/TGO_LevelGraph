namespace LevelGraph.Parameter {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Incident Edge Angle Parameter", menuName = "Edge Generator/Incident Edge Angle Parameter", order = 0)]
    public class IncidentEdgeAngleParameter : EdgeGeneratorParameter {
        [SerializeField] [Range(0, 180)] float min = 0;
        [SerializeField] [Range(0, 180)] float max = 90;

        public override bool Check(Edge edge) {
            Vector3 edgeVector = GetVector(edge);
            foreach(Edge incidentEdge in edge.GetSource().GetEdges()) {
                if(incidentEdge == edge) continue;

                Vector3 incidentEdgeVector = GetVector(incidentEdge);
                float angleBetweenEdge = Vector3.Angle(edgeVector, incidentEdgeVector);

                if(angleBetweenEdge < (min - Mathf.Epsilon) || angleBetweenEdge > (max + Mathf.Epsilon)) {
                    return false;
                }
            }
            foreach(Edge incidentEdge in edge.GetTarget().GetEdges()) {
                if(incidentEdge == edge) continue;

                Vector3 incidentEdgeVector = GetVector(incidentEdge);
                float angleBetweenEdge = Vector3.Angle(edgeVector, incidentEdgeVector);

                if(angleBetweenEdge < (min - Mathf.Epsilon) || angleBetweenEdge > (max + Mathf.Epsilon)) {
                    return false;
                }
            }
            return true;
        }

        Vector3 GetVector(Edge edge) {
            return edge.GetTarget().transform.position - edge.GetSource().transform.position;
        }
    }
}