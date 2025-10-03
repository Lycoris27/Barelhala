using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstAbilityScript : MonoBehaviour, IEnemyAbility
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 90f;

    [Header("Movement Settings")]
    public Vector2 movementDirection = Vector2.up;
    public float movementSpeed = 2f;

    [Header("Projectile Settings")]
    public float spawnRate = 2f;
    public int projectilesPerBlast = 8;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;

    [Header("Lifetime Settings")]
    public float lifetime = 5f;

    [SerializeField] private float abilityDelay = 3f;

    public void Activate(EnemyAbilityActivator activator)
    {
        //activatorRef = activator;
        //isSpawning = true;
        SpawnProjectiles();
        StartCoroutine(ActivateTimer());
    }

    private void SpawnProjectiles()
    {
        float angleStep = 360f / projectilesPerBlast;

        for (int i = 0; i < projectilesPerBlast; i++)
        {
            float angle = i * angleStep;
            Quaternion spawnRotation = Quaternion.Euler(0f, transform.eulerAngles.z + angle, 0f);
            Vector3 direction = spawnRotation * Vector3.forward;

            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 normalizedDirection = direction.normalized;

                rb.linearVelocity = normalizedDirection * projectileSpeed;
            }
        }
    }
    private IEnumerator ActivateTimer()
    {
        yield return new WaitForSeconds(abilityDelay);
        GlobalEvents.InvokeOnAbilityCompleted();

    }
}
