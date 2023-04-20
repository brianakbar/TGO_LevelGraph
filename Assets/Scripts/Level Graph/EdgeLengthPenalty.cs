namespace LevelGraph {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Edge Length Penalty", menuName = "Penalty/Edge Length", order = 0)]
    public class EdgeLengthPenalty : Penalty {
        [SerializeField] float allowedDeviation = 20f;
        [SerializeField] int penalty = 1;
        [SerializeField] bool perDeviation = true;

        public override void CalculatePenalty(EdgeList edgeList) {
            float averageLength = edgeList.GetAverageLength();
            foreach(Edge edge in edgeList.GetEdges()) {
                edge.SetWeight(edge.GetWeight() + GetPenalty(edge, averageLength));
            }
        }

        public int GetPenalty(Edge edgeToInspect, float averageLength) {
            float allowedLengthDeviation = averageLength * allowedDeviation / 100;
            float deviation = Mathf.Abs(edgeToInspect.GetLength() - averageLength);
            if(deviation <= allowedLengthDeviation) return 0;
            if(!perDeviation) return penalty;
            return Mathf.FloorToInt(deviation / allowedLengthDeviation) * penalty;
        }
    }
}