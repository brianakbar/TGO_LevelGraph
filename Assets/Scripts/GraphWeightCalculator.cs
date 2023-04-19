namespace LevelGraph {
    using UnityEngine;
    
    [ExecuteInEditMode]
    public class GraphWeightCalculator : MonoBehaviour {
        [SerializeField] float edgeLengthAllowedDeviation = 20f;
        [SerializeField] int weightPenalty = 1;
        [SerializeField] bool perDeviation = true;

        EdgeList edgeList;

        void Awake() {
            edgeList = GetComponent<EdgeList>();
        }

        public void CalculateAllEdgeWeight() {
            float averageLength = edgeList.GetAverageLength();
            foreach(Edge edge in edgeList.GetEdges()) {
                edge.SetWeight(1 + GetPenalty(edge, averageLength));
            }
        }

        int GetPenalty(Edge edgeToInspect, float averageLength) {
            float allowedLengthDeviation = averageLength * edgeLengthAllowedDeviation / 100;
            float deviation = Mathf.Abs(edgeToInspect.GetLength() - averageLength);
            if(deviation <= allowedLengthDeviation) return 0;
            if(!perDeviation) return weightPenalty;
            return Mathf.FloorToInt(deviation / allowedLengthDeviation) * weightPenalty;
        }
    }
}