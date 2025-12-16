using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossAnimationActivation", story: "Set [Movement] [Name] to [Bool]", category: "Action", id: "55bac8146c5079fcf0a5def6c0edf7b9")]
public partial class BossAnimationActivationAction : Action
{
    [SerializeReference] public BlackboardVariable<BossAnimScript> Movement;
    [SerializeReference] public BlackboardVariable<string> Name;
    [SerializeReference] public BlackboardVariable<bool> Bool;
    protected override Status OnStart()
    {
        Movement.Value.ModifyAnim(Name.Value, Bool.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

