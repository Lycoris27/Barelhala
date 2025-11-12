using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Attack",
    story: "[Agent] [attacks] [Target]",
    category: "Action",
    id: "f3b37987bc9971324f05980eafd5e3dc")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<AbilityBaseScript> Attacks;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        if (Agent?.Value == null)
        {
            Debug.LogWarning("AttackAction: Agent is null");
            return Status.Failure;
        }

        if (Attacks?.Value == null)
        {
            Debug.LogWarning("AttackAction: Attacks is null");
            return Status.Failure;
        }

        // Trigger the ability immediately
        
        Attacks.Value.ActivateAbility(Agent.Value);

        // Node is done, tree can progress
        return Status.Success;
    }

    // No need to override OnUpdate if node completes immediately
}