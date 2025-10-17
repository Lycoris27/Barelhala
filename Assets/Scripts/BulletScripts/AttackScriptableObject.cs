using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewAttackAbility", menuName = "Scriptable Objects/Create new attack")]
public class AttackScriptableObject : AbilityBaseScript
{
    // Prefab input points
    [Header("Object input")]
    [Tooltip("This is the prefab for what type of bullet is shot out")]
    [SerializeField] private GameObject prefab;  // private backing field
    public override GameObject Prefab => prefab;

    [Header("Initial Variables")]
    [Tooltip("This is a delay before the start of the ability, can be useful if attaching multiple scripts in succession")]
    [SerializeField] private float initialDelay;
    public override float InitialDelay => initialDelay;

    [SerializeField] private Vector3 objectSize;
    public override Vector3 Size => objectSize;

    [Header("Direction Variables")]
    [Tooltip("determines if the direction is targetted at the player. " +
        "\n If = true | Ignore everything other than coneAngle")]
    [SerializeField] private bool isTargettingPlayer; // might be desired later
    public override bool IsTargettingPlayer => isTargettingPlayer;

    [Tooltip("if shooting multiple bullets, will determine the range over which bullets can be shot " +
    "\n 0 = no deviation, will overlap all bullets " +
    "\n 360 = full circle, will shoot in all directions around enemy")]
    [SerializeField] private float coneAngle = 360f;
    public override float ConeAngle => coneAngle;

    [Tooltip("When not targetting the player, determines the direction that the bullets are pointing at")]
    [SerializeField] private float direction;
    public override float Direction => direction;

    [Tooltip("After shooting out objects, the change in direction from the last")]
    [SerializeField] private int directionChange;
    public override int DirectionChange => directionChange;

    [Tooltip("After how many direction changes until it swaps direction")]
    [SerializeField] private int directionSwapCounter;
    public override int DirectionSwapCounter => directionSwapCounter;

    [Tooltip("Determines whether or not the swap counter resets and allows the direction to change multiple times")]
    [SerializeField] private bool repeatingCounter;
    public override bool RepeatingCounter => repeatingCounter;

    [Header("Spawning Variables")]

    [SerializeField] private float spawnRate;
    public override float SpawnRate => spawnRate;

    [SerializeField] private float bulletCount;
    public override float SpawnCount => bulletCount;

    [SerializeField] private float spawnRadius = 0f; // new field
    public override float SpawnRadius => spawnRadius;

    [Header("Linear Velocity Variables")]

    [SerializeField] private float linearSpeed;  // private backing field
    public override float LinearSpeed => linearSpeed;   // public property fulfills abstract contract

    [SerializeField] private float linearSpeedDelay;
    public override float LinearSpeedDelay => linearSpeedDelay;

    [Header("Angular Velocity Variables")]

    [SerializeField] private float angularSpeed;
    public override float AngularSpeed => angularSpeed;

    [SerializeField] private float angularSpeedDelay;
    public override float AngularSpeedDelay => angularSpeedDelay;

    [SerializeField] private bool rotateAroundObject;
    public override bool RotateAroundObject => rotateAroundObject;

    [Header("End Variables")]
    [SerializeField] private float abilityLifetime;
    public override float Lifetime => abilityLifetime;

    [SerializeField] private float abilityDecay;
    public override float Decay => abilityDecay;

    // Unique Ability Variables
    [SerializeField] private bool active = false;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<GameObject> delayedObjects = new List<GameObject>();

    private GameObject playerRef; // = null;
    private GameObject selfRef;
    //public void Start() { playerRef = GameObject.FindWithTag("Player"); }

    public override void ActivateAbility(GameObject SelfRef)
    {

        if (selfRef == null) selfRef = SelfRef;
        if (playerRef == null) playerRef = GameObject.FindWithTag("Player");
        active = true;

        Debug.Log($"self reference = {selfRef}, Self Ref = {SelfRef}");

        // Delay before spawning bullets
        GlobalEvents.StartDelay(InitialDelay, () =>
        {
            GenerateBullets();
            // Schedule lifetime cleanup
            GlobalEvents.StartDelay(Lifetime, EndAbility);
        });
    }
    private void EndAbility()
    {
        active = false;
        CleanupBullets();
    }

    private void CleanupBullets()
    {
        float totalDecayTime = Decay != 0 ? 1 / Decay : 0f;

        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            GameObject obj = spawnedObjects[i];
            if (obj != null)
            {
                float delay = totalDecayTime * i;
                GlobalEvents.StartDelay(delay, () =>
                {
                    if (obj != null)
                        GameObject.Destroy(obj);
                });
            }
        }
        spawnedObjects.Clear();
    }

    private void GenerateBullets()
    {
        if (!active) return;

        delayedObjects.Clear();

        float currentRotation = 0f;
        float halfConeAngle = coneAngle / 2f;
        bool flipRotation = false;
        int shotCounter = 0;

        Action spawnLoop = null;
        spawnLoop = () =>
        {
            if (!active) return;

            delayedObjects.Clear();

            Vector3 coneForward = (IsTargettingPlayer && playerRef != null)
                ? (playerRef.transform.position - selfRef.transform.position).normalized
                : Quaternion.Euler(0f, direction, 0f) * Vector3.forward;

            for (int i = 0; i < SpawnCount; i++)
            {
                float spread = (SpawnCount > 1) ? (float)i / (SpawnCount - 1) : 0.5f;
                float angle = Mathf.Lerp(-halfConeAngle, halfConeAngle, spread) + currentRotation;

                Vector3 bulletDirection = Quaternion.Euler(0f, angle, 0f) * coneForward;
                bulletDirection.Normalize();

                Vector3 spawnPos = selfRef.transform.position + bulletDirection * SpawnRadius;
                Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection, Vector3.up);

                GameObject bullet = Instantiate(Prefab, spawnPos, bulletRotation);
                bullet.transform.localScale = new Vector3(
                    bullet.transform.localScale.x * Size.x,
                    bullet.transform.localScale.y * Size.y,
                    bullet.transform.localScale.z * Size.z
                );

                spawnedObjects.Add(bullet);
                delayedObjects.Add(bullet);
            }
            ActivateLinearMomentum();
            ActivateAngularMomentum();

            // Handle rotation changes
            shotCounter++;
            if (shotCounter == DirectionSwapCounter)
            {
                flipRotation = !flipRotation;
                if (repeatingCounter) shotCounter = 0;
            }
            currentRotation += flipRotation ? -DirectionChange : DirectionChange;

            // Repeat after spawnRate delay
            GlobalEvents.StartDelay(1 / SpawnRate, spawnLoop);
        };
        spawnLoop.Invoke();
    }
    private void ActivateLinearMomentum()
    {
        GlobalEvents.StartDelay(LinearSpeedDelay, () =>
        {
            foreach (GameObject bullet in delayedObjects)
            {
                if (bullet == null) continue;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.linearVelocity = bullet.transform.forward * LinearSpeed;
            }
        });
    }
    private void ActivateAngularMomentum()
    {
        GlobalEvents.StartDelay(AngularSpeedDelay, () =>
        {
            if (RotateAroundObject)
            {
                GameObject orbitContainerPrefab = Resources.Load<GameObject>("BulletObjectContainer");
                GameObject orbitContainer = Instantiate(orbitContainerPrefab, selfRef.transform.position, Quaternion.identity);
                orbitContainer.transform.parent = selfRef.transform;

                foreach (GameObject bullet in delayedObjects)
                {
                    if (bullet == null) continue;
                    bullet.transform.parent = orbitContainer.transform;
                }
                Rigidbody rb = orbitContainer.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.angularVelocity = orbitContainer.transform.up * AngularSpeed;
            }
            else
            {
                foreach (GameObject bullet in delayedObjects)
                {
                    if (bullet == null) continue;
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.angularVelocity = bullet.transform.up * AngularSpeed;
                }
            }
        });
    }
}