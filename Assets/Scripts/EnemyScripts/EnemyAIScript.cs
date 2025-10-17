using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private bool isTargettingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //agent.SetDestination(target.position);
    }
    private void Update()
    {
        //UpdatePlayerTargetting();
    }

    public void SettingDestination(GameObject targetPos, bool targettingPlayer, float speed, float lifetime)
    {
        agent.speed = speed;
        isTargettingPlayer = targettingPlayer;
        // if not targetting the player, just set the position, go to it, and end it after the delay.
        if (!targettingPlayer)
        {
            agent.SetDestination(targetPos.transform.position);
            StartMovement();
            if(lifetime != 0) GlobalEvents.StartDelay(lifetime, StopMovement);
        }
        else
        {
            agent.SetDestination(target.position);
            StartMovement();
            if (lifetime != 0) GlobalEvents.StartDelay(lifetime, StopMovement);
        }
    }
    public void UpdatePlayerTargetting()
    {
        if(isTargettingPlayer) agent.SetDestination(target.position);
    }
    public void StopMovement()
    {
        agent.isStopped = true;
    }
    public void StartMovement()
    {
        agent.isStopped = false;
    }
}
