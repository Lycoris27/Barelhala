using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour
{
    
    NavMeshAgent agent;
    Vector3 lastTargetPosition;
    [SerializeField] Transform target;
    [SerializeField] float repositionThreshold = 1f;
    [SerializeField] bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(target.position);
    }
    void Update()
    {
        if (canMove) { UpdateMovement();}
    }

    private void UpdateMovement()
    {
        if ((target.position - lastTargetPosition).sqrMagnitude > repositionThreshold * repositionThreshold)
        {
            agent.SetDestination(target.position);
            lastTargetPosition = target.position;
        }
    }

    public void ChangeCanMove()
    {
        if (canMove) { canMove = false;}
        if (!canMove) { canMove = true;}
    }
}
