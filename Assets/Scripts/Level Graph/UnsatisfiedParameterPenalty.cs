namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Unsatisfied Parameter Penalty", menuName = "Penalty/Unsatisfied Parameter", order = 0)]
    public class UnsatisfiedParameterPenalty : Penalty {
        [SerializeField] List<UnsatisfiedParameter> unsatisfiedParameters;

        [System.Serializable]
        class UnsatisfiedParameter {
            // public UnsatisfiedParameterType type;
            // public MinMax value;
            public EdgeGeneratorParameter parameter;
            public int penalty;
        }

        public override void CalculatePenalty(EdgeList edgeList) {
            foreach(Edge edge in edgeList.GetEdges()) {
                foreach(var unsatisfied in unsatisfiedParameters) {
                    //HandleUnsatisfiedParameter(unsatisfied, edge);
                    if(!unsatisfied.parameter.Check(edge)) {
                        edge.SetWeight(edge.GetWeight() + unsatisfied.penalty);
                    }
                }
            }
        }

        // void HandleUnsatisfiedParameter(UnsatisfiedParameter unsatisfied, Edge edge) {
        //     if(unsatisfied.type == UnsatisfiedParameterType.EdgeLength) {
        //         HandleEdgeLengthPenalty(unsatisfied, edge);
        //     }
        // }

        // void HandleEdgeLengthPenalty(UnsatisfiedParameter unsatisfied, Edge edge) {
        //     if(edge.GetLength() < unsatisfied.value.Min || edge.GetLength() > unsatisfied.value.Max) {
        //         edge.SetWeight(edge.GetWeight() + unsatisfied.penalty);
        //     }
        // }
    }
}