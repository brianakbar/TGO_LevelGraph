namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Unsatisfied Parameter Penalty", menuName = "Penalty/Unsatisfied Parameter", order = 0)]
    public class UnsatisfiedParameterPenalty : Penalty {
        [SerializeField] List<UnsatisfiedParameter> unsatisfiedParameters;

        [System.Serializable]
        class UnsatisfiedParameter {
            public EdgeGeneratorParameter parameter;
            public int penalty;
        }

        public override void CalculatePenalty(EdgeList edgeList) {
            foreach(Edge edge in edgeList.GetEdges()) {
                foreach(var unsatisfied in unsatisfiedParameters) {
                    if(!unsatisfied.parameter.Check(edge)) {
                        edge.SetWeight(edge.GetWeight() + unsatisfied.penalty);
                    }
                }
            }
        }
    }
}