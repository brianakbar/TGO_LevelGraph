namespace LevelGraph {
    using System.Collections.Generic;
    using UnityEngine;
    
    [ExecuteInEditMode]
    public class GraphWeightCalculator : MonoBehaviour {
        [SerializeField] List<Penalty> penalties;
        
        EdgeList edgeList;

        void Awake() {
            edgeList = GetComponent<EdgeList>();
        }

        public void CalculateAllEdgeWeight() {
            foreach(Edge edge in edgeList.GetEdges()) {
                edge.SetWeight(1);
            }
            foreach(Penalty penalty in penalties) {
                penalty.CalculatePenalty(edgeList);
            }   
        }
    }
}