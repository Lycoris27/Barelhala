using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserScript : MonoBehaviour
{
    /*
    // Prefab Variables

    [SerializeField] private GameObject laserPrefab;  // private backing field
    public override GameObject Prefab => laserPrefab;

    // Time Variables
    [SerializeField] private float delay;
    public override float Delay => delay;

    [SerializeField] private float rotationalSpeed;
    public override float Speed => rotationalSpeed;

    [SerializeField] private float spawnRate;
    public override float SpawnRate => spawnRate;

    [SerializeField] private float laserCount;
    public override float SpawnCount => laserCount;

    [SerializeField] private float laserLifetime;
    public override float Lifetime => laserLifetime;

    //PositionalVariables
    public override float SpawnRadius => 0f;

    [SerializeField] private bool isTargetting;
    public override bool IsTargetting => isTargetting;

    [SerializeField] private float rotation;
    public override float Rotation => 0f;

    [SerializeField] private float direction;
    public override float Direction => 1f;

    // Scale Variable

    [SerializeField] private float laserGirth;
    public override float Size => laserGirth;

    // Unique fields to the script
    private bool active = false;

    [SerializeField] private float coneAngle = 360f;

    private GameObject playerRef;

    private List<GameObject> lasers = new List<GameObject>();

    public void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
    }

    public override IEnumerator ActivateAbility()
    {
        yield return new WaitForSeconds(Delay);
        active = true;
        StartCoroutine(GenerateLasers());
        yield return new WaitForSeconds(Lifetime);
        active = false;

        foreach (GameObject obj in lasers)
        {
            GameObject.Destroy(obj);
        }
        lasers.Clear();
    }
    private IEnumerator GenerateLasers()
    {
        float currentRotation = 0f; // spiral rotation
        float halfConeAngle = coneAngle / 2f;

        while (active)
        {
            // Determine the forward direction for the cone
            Vector3 coneForward;
            if (IsTargetting && playerRef != null)
            {
                // Aim the center of the cone at the player
                coneForward = (playerRef.transform.position - transform.position).normalized;
            }
            else
            {
                coneForward = Quaternion.Euler(0f, Direction, 0f) * Vector3.forward;
            }

            for (int i = 0; i < SpawnCount; i++)
            {
                // Spread lasers evenly across the cone
                float t = (float)(i + 0.5f) / SpawnCount;
                float angle = Mathf.Lerp(-halfConeAngle, halfConeAngle, t) + currentRotation;

                // Rotate each laser around the Y-axis relative to the cone forward
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * coneForward;
                direction.y = 0f; // keep flat
                direction.Normalize();

                // Create rotation only on Y axis
                Quaternion laserRotation = Quaternion.LookRotation(direction, Vector3.up);

                // Instantiate laser with correct orientation
                GameObject laser = Instantiate(Prefab, transform.position, laserRotation);
                lasers.Add(laser);
                laser.transform.localScale = new Vector3(Size, Size, 1);
                Rigidbody rb = laser.GetComponent<Rigidbody>();
                if (rb != null) rb.angularVelocity = Vector3.up * Speed * Mathf.Deg2Rad;
            }

            // Increment rotation for the next wave (spiral effect)
            currentRotation += Rotation;

            // Wait between waves based on SpawnRate
            yield return new WaitForSeconds(1f / SpawnRate);
        }
    }
    */
}