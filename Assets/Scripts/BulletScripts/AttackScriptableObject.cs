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

    [SerializeField] private float initialDelay = 0f;
    public override float InitialDelay => initialDelay;

    [SerializeField] private Vector3 objectSize = new Vector3(1f, 1f, 1f);
    public override Vector3 Size => objectSize;

    [Tooltip("if shooting multiple bullets, will determine the range over which bullets can be shot " +
"\n 0 = no deviation, will overlap all bullets " +
"\n 360 = full circle, will shoot in all directions around enemy")]
    [SerializeField] private float coneAngle = 360f;
    public override float ConeAngle => coneAngle;

    [Tooltip("determines if the direction is targetted at the player. " +
    "\n If = true | Ignore everything other than coneAngle")]
    [SerializeField] private bool isTargettingPlayer; // might be desired later
    public override bool IsTargettingPlayer => isTargettingPlayer;



    [Header("Spawning Variables")]

    [SerializeField] private float spawnRate;





    [SerializeField] private float bulletCount;
    public override float SpawnCount => bulletCount;

    [SerializeField] private float spawnRadius = 0f; // new field
    public override float SpawnRadius => spawnRadius;

    [SerializeField] private float radiusChange = 0f;

    [Header("Direction Variables - Only input if IsTargettingPlayer = False")]

    [Tooltip("When not targetting the player, determines the direction that the bullets are pointing at")]
    [SerializeField] private float direction;
    public override float Direction => direction;

    [SerializeField] private bool randomDirection = false;

    [Tooltip("After shooting out objects, the change in direction from the last")]
    [SerializeField] private int directionChange;
    public override int DirectionChange => directionChange;

    [Tooltip("After how many direction changes until it swaps direction")]
    [SerializeField] private int directionSwapCounter;
    public override int DirectionSwapCounter => directionSwapCounter;

    [Tooltip("Determines whether or not the swap counter resets and allows the direction to change multiple times")]
    [SerializeField] private bool repeatingCounter;
    public override bool RepeatingCounter => repeatingCounter;

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

    [SerializeField] private bool isLifetimeInfinite = false;
    public override bool IsLifetimeInfinite => isLifetimeInfinite;

    [SerializeField] private float abilityLifetime;
    public override float Lifetime => abilityLifetime;

    [SerializeField] private float abilityDecay;
    public override float Decay => abilityDecay;

    [SerializeField] private bool earlyDecay = false;

    [SerializeField] private float decayDelay = 0;
    public override float DecayDelay => decayDelay;

    // Unique Ability Variables
    private bool active = false;

    // Replaced List with FIFO Queue
    private Queue<GameObject> spawnedObjects = new Queue<GameObject>();

    private GameObject playerRef;
    private GameObject selfRef;

    private int newDirectChange;


    
    

    // -----------------------------------------------------------
    // ACTIVATE
    // -----------------------------------------------------------
    public override void ActivateAbility(GameObject SelfRef)
    {
        if (selfRef == null) selfRef = SelfRef;
        if (playerRef == null) playerRef = GameObject.FindWithTag("Player");
        active = true;

        //storedPlayerPos = playerRef.transform.position;
        //storedSelfPos = selfRef.transform.position;

        GenerateBullets();

        GlobalEvents.StartDelay(InitialDelay, () =>
        {
            if (!isLifetimeInfinite)
                GlobalEvents.StartDelay(Lifetime, EndAbility);

            if (earlyDecay)
                CleanupBullets();
        });
    }

    // -----------------------------------------------------------
    // END + CLEANUP
    // -----------------------------------------------------------
    private void EndAbility()
    {
        active = false;
        CleanupBullets();
    }

    private void CleanupBullets()
    {
        float delay = Decay != 0f ? 1f / Decay : 0f;

        GlobalEvents.StartDelay(DecayDelay, () =>
        {
            DestroyNextBullet(delay);
        });
        
    }

    private void DestroyNextBullet(float delay)
    {
        if (spawnedObjects.Count == 0 || (active && !earlyDecay))
            return;

        GameObject obj = spawnedObjects.Dequeue();
        if (obj != null)
            GameObject.Destroy(obj);

        GlobalEvents.StartDelay(delay, () =>
        {
            DestroyNextBullet(delay);
        });
    }

    // -----------------------------------------------------------
    // BULLET GENERATION
    // -----------------------------------------------------------
    private void GenerateBullets()
    {
        float currentRotation = 0f;
        float currentRadius = spawnRadius;
        float halfConeAngle = coneAngle / 2f;
        bool flipRotation = false;
        int shotCounter = 0;
        bool firstCounterComplete = false;

        Vector3 playerPos = playerRef.transform.position;

        Action spawnLoop = null;
        spawnLoop = () =>
        {
            if (!active) return;
            if (selfRef == null) return;

            // Track bullets spawned *only this cycle*
            List<GameObject> newlySpawned = new List<GameObject>();


            Vector3 coneForward = (IsTargettingPlayer && playerRef != null)
                ? (playerPos - selfRef.transform.position).normalized
                : Quaternion.Euler(0f, direction, 0f) * Vector3.forward;

            selfRef.transform.rotation = Quaternion.LookRotation(playerPos - selfRef.transform.position);

            for (int i = 0; i < SpawnCount; i++)
            {
                float spread = (SpawnCount > 1) ? (float)i / (SpawnCount - 1) : 0.5f;
                float angle = Mathf.Lerp(-halfConeAngle, halfConeAngle, spread) + currentRotation;

                Vector3 bulletDirection = Quaternion.Euler(0f, angle, 0f) * coneForward;
                bulletDirection.Normalize();

                Vector3 spawnPos = selfRef.transform.position + bulletDirection * currentRadius;
                Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection, Vector3.up);

                GlobalEvents.StartDelay(InitialDelay, () =>
                {
                    GameObject bullet = Instantiate(Prefab, spawnPos, bulletRotation);
                    bullet.transform.localScale = Size;

                    spawnedObjects.Enqueue(bullet); // F I F O
                    newlySpawned.Add(bullet);
                });
            }


            // Apply forces to the bullets spawned this cycle
            ActivateLinearMomentum(newlySpawned);
            ActivateAngularMomentum(newlySpawned);


            // Rotation pattern logic
            shotCounter++;
            Debug.Log(shotCounter);
            if (shotCounter == DirectionSwapCounter && !firstCounterComplete)
            {
                flipRotation = !flipRotation;
                if (RepeatingCounter) shotCounter = 0;
                firstCounterComplete = !firstCounterComplete; 
            }
            else if (shotCounter == 2 * DirectionSwapCounter && firstCounterComplete)
            {
                flipRotation = !flipRotation;
                if (RepeatingCounter) shotCounter = 0;
            }

            if (!randomDirection)
                currentRotation += flipRotation ? -DirectionChange : DirectionChange;
            else if (randomDirection)
            {
                newDirectChange = UnityEngine.Random.Range(-DirectionChange, DirectionChange);
                currentRotation = newDirectChange;
            }
            currentRadius += radiusChange;

            // Repeat
            if(spawnRate != 0)
                GlobalEvents.StartDelay(1f / spawnRate, spawnLoop);
        };

        spawnLoop.Invoke();
    }



    // -----------------------------------------------------------
    // MOMENTUM
    // -----------------------------------------------------------
    private void ActivateLinearMomentum(List<GameObject> newlySpawned)
    {
        GlobalEvents.StartDelay(LinearSpeedDelay, () =>
        {
            foreach (GameObject bullet in newlySpawned)
            {
                if (bullet == null) continue;

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.linearVelocity = bullet.transform.forward * LinearSpeed;
            }
        });
    }


    private void ActivateAngularMomentum(List<GameObject> newlySpawned)
    {
        GlobalEvents.StartDelay(AngularSpeedDelay, () =>
        {
            if (RotateAroundObject)
            {
                GameObject orbitContainerPrefab = Resources.Load<GameObject>("BulletObjectContainer");
                GameObject orbitContainer = Instantiate(orbitContainerPrefab, selfRef.transform.position, Quaternion.identity);
                orbitContainer.transform.parent = selfRef.transform;

                foreach (GameObject bullet in newlySpawned)
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
                foreach (GameObject bullet in newlySpawned)
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
