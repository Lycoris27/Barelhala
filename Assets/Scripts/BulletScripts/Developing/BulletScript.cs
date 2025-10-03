using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletScript : AbilityBaseScript
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

    [Header("Chain Ability Variables")]
    [SerializeField] private AbilityBaseScript chainedAbility;
    public override AbilityBaseScript ChainedAbility => chainedAbility;

    [SerializeField] private float chainAbilityStart;
    public override float ChainAbilityStart => chainAbilityStart;

    // Unique Ability Variables
    [SerializeField] private bool active = false;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<GameObject> delayedObjects = new List<GameObject>();

    private GameObject playerRef; // = null;
    public void Start() { playerRef = GameObject.FindWithTag("Player"); }

    public override IEnumerator ActivateAbility()
    {
        if (ChainedAbility != null)
        {
            StartCoroutine(ActivateChainAbility());
        }
        yield return new WaitForSeconds(InitialDelay);
        active = true;
        StartCoroutine(GenerateBullets());
        yield return new WaitForSeconds(Lifetime);
        active = false;
        foreach (GameObject obj in spawnedObjects)
        {
            GameObject.Destroy(obj);
            if (Decay != 0)
            {
                yield return new WaitForSeconds(1 / Decay);
            }
        }
        spawnedObjects.Clear();
    }
    public IEnumerator ActivateChainAbility()
    {
        yield return new WaitForSeconds(ChainAbilityStart);
        StartCoroutine(ChainedAbility.ActivateAbility());
    }

    private IEnumerator GenerateBullets()
    {
        float currentRotation = 0f;
        float halfConeAngle = coneAngle / 2f;
        bool flipRotation = false;
        int shotCounter = 0;

        while (active)
        {
            delayedObjects.Clear();
            Vector3 coneForward;
            if (IsTargettingPlayer && playerRef != null)
                coneForward = (playerRef.transform.position - transform.position).normalized;
            else
                coneForward = Quaternion.Euler(0f, direction, 0f) * Vector3.forward;

            for (int i = 0; i < SpawnCount; i++)
            {
                // Spread bullets evenly across the cone
                float spread = (SpawnCount > 1) ? (float)i / (SpawnCount - 1) : 0.5f;
                float angle = Mathf.Lerp(-halfConeAngle, halfConeAngle, spread) + currentRotation;

                // Calculate the direction this bullet will face and travel
                Vector3 bulletDirection = Quaternion.Euler(0f, angle, 0f) * coneForward;
                bulletDirection.Normalize();

                // Calculate spawn position along that direction
                Vector3 spawnPos = transform.position + bulletDirection * SpawnRadius;

                // Create a rotation that faces the shooting direction
                Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection, Vector3.up);

                // Instantiate bullet facing its firing direction
                GameObject bullet = Instantiate(Prefab, spawnPos, bulletRotation);
                bullet.transform.localScale = new Vector3(
                bullet.transform.localScale.x * Size.x,
                bullet.transform.localScale.y * Size.y,
                bullet.transform.localScale.z * Size.z
                );

                spawnedObjects.Add(bullet);
                delayedObjects.Add(bullet);
            }

            StartCoroutine(ActivateLinearMomentum());
            StartCoroutine(ActivateAngularMomentum());


            // Rotation Swapper
            shotCounter++;
            if (shotCounter == DirectionSwapCounter)
            {
                flipRotation = !flipRotation;
                if (repeatingCounter)
                {
                    shotCounter = 0;
                }
            }
            currentRotation += flipRotation ? -DirectionChange : DirectionChange;

            yield return new WaitForSeconds(1/SpawnRate);
        }
    }

    private IEnumerator ActivateLinearMomentum()
    {
        yield return new WaitForSeconds(LinearSpeedDelay);

        // Shoot the bullets
        foreach (GameObject bullet in delayedObjects)
        {
            if (bullet == null) continue;
            // Apply forward velocity using its rotation
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = bullet.transform.forward * LinearSpeed;
        }
    }

    private IEnumerator ActivateAngularMomentum()
    {
        if (RotateAroundObject)
        {
            GameObject orbitContainerPrefab = Resources.Load<GameObject>("BulletObjectContainer");
            GameObject orbitContainer = Instantiate(orbitContainerPrefab, transform.position, Quaternion.identity);
            orbitContainer.transform.parent = this.transform;
            // Shoot the bullets
            foreach (GameObject bullet in delayedObjects)
            {
                if (bullet == null) continue;
                bullet.transform.parent = orbitContainer.transform;
            }
            yield return new WaitForSeconds(AngularSpeedDelay);

            Rigidbody rb = orbitContainer.GetComponent<Rigidbody>();
            rb.angularVelocity = orbitContainer.transform.up * AngularSpeed;
        }
        else if(!RotateAroundObject)
        {
            yield return new WaitForSeconds(AngularSpeedDelay);
            foreach (GameObject bullet in delayedObjects)
            {
                if (bullet == null) continue;
                // Apply forward velocity using its rotation
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.angularVelocity = bullet.transform.up * AngularSpeed;
            }
        }
    }
}