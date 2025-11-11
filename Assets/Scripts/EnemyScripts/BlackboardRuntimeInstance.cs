using UnityEngine;
using Unity.Behavior; // adjust if your package uses a different namespace

public class BlackboardRuntimeInstance : MonoBehaviour
{
    [SerializeField] private Blackboard sourceBlackboard;  // assign your template Blackboard asset
    [SerializeField] private BehaviorGraph goonerGraph;    // assign the graph asset

}
