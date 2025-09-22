using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityActivator : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> effects = new();
    //rivate int currentIndex = 0;
    [SerializeField] private bool canActivate = true;
    //[SerializeField] private float timer = 0f;
    //[SerializeField] private float timeForAbility = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;


    private void OnEnable()
    {
        LOSScript.OnPlayerEnterEnemyLOS += IsPlayerInSight;
        GlobalEvents.OnAbilityCompleted += ReactivateAbilityUsability;
    }
    private void OnDisable()
    {
        LOSScript.OnPlayerEnterEnemyLOS -= IsPlayerInSight;
        GlobalEvents.OnAbilityCompleted -= ReactivateAbilityUsability;
    }
    private void IsPlayerInSight(GameObject playerRef)
    {

        if (canActivate)
        {
            Vector3 direction = (playerRef.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, playerRef.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleLayer | playerLayer))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    ActivatePlayerDetectedAbility();
                    canActivate = false;
                }
            }
        }
    }

    private void ActivatePlayerDetectedAbility()
    {
        foreach (IEnemyAbility ability in effects)
        {
            ability.Activate(this);
            print("doing this");
        }
    }

    private void ReactivateAbilityUsability()
    {
        canActivate = true;
    }
}
