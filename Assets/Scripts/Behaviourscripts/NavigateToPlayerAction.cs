using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;


[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Navigate To Player", story: "[Agent] attacks [Target]", category: "Action/Navigation", id: "aec337fd0b30377a5801e3072c5f5008")]
public partial class NavigateToPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Speed = new BlackboardVariable<float>(1.0f);
    [SerializeReference] public BlackboardVariable<bool> isMoving = new BlackboardVariable<bool>(true);

    private NavMeshAgent m_NavMeshAgent;
    private Animator m_Animator;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_ColliderAdjustedTargetPosition;
    [CreateProperty] private float m_OriginalStoppingDistance = -1f;
    [CreateProperty] private float m_OriginalSpeed = -1f;
    private float m_ColliderOffset;
    private float m_CurrentSpeed;

    protected override Status OnStart()
    {
        m_NavMeshAgent = this.GameObject.GetComponentInChildren<NavMeshAgent>();

        if (isMoving.Value) { m_NavMeshAgent.isStopped = false; }

        else { m_NavMeshAgent.isStopped = true; }
        
        m_NavMeshAgent.SetDestination(Target.Value.transform.position);

        m_NavMeshAgent.speed = Speed.Value;
        
        return Status.Success;
    }
}

