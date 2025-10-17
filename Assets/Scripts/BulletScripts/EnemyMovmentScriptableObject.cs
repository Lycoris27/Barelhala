using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewMovementAbility", menuName = "Scriptable Objects/Create new movement")]

public class EnemyMovmentScriptableObject : AbilityBaseScript
{
    // Removed Vars
    public override float SpawnRate => 0f;
    public override float SpawnCount => 0f;
    public override float SpawnRadius => 0f;
    public override int DirectionChange => 0;
    public override int DirectionSwapCounter => 0;
    public override bool RepeatingCounter => false;
    public override float ConeAngle => 0f;
    public override float AngularSpeed => 0f;
    public override float AngularSpeedDelay => 0f;
    public override bool RotateAroundObject => false;
    public override float Decay => 0f;
    public override Vector3 Size => new Vector3();
    public override float Direction => 0f;
    public override float LinearSpeedDelay => 0f;

    // Prefab input point
    [SerializeField] private GameObject movementPosition;
    public override GameObject Prefab => movementPosition;

    [Header("Starting Variables")]
    [SerializeField] private float initialDelay;
    public override float InitialDelay => initialDelay;

    [SerializeField] private bool isTargettingPlayer; // might be desired later
    public override bool IsTargettingPlayer => isTargettingPlayer;

    [Header("Linear Velocity Variables")]
    [SerializeField] private float linearSpeed;  // private backing field
    public override float LinearSpeed => linearSpeed;   // public property fulfills abstract contract

    [Header("End Variables")]
    [Tooltip("allows the character to end movement early, to not immediately  ")]
    [SerializeField] private float movementLifetime;
    public override float Lifetime => movementLifetime;

    private GameObject playerRef; // = null;
    private GameObject selfRef;

    public override void ActivateAbility(GameObject SelfRef)
    {
        if (selfRef != null) selfRef = SelfRef;
        if (playerRef != null) playerRef = GameObject.FindWithTag("Player");
        EnemyAIScript agent = selfRef.GetComponent<EnemyAIScript>();
        GlobalEvents.StartDelay(InitialDelay, () =>
        {
            agent.SettingDestination(Prefab, IsTargettingPlayer, LinearSpeed, Lifetime);
            if(Lifetime != 0)
                GlobalEvents.StartDelay(Lifetime, TriggerNextAbility);
        });
    }
    private void TriggerNextAbility()
    {
        selfRef.GetComponent<AbilityActivatorTest>().StartActivatingAbilities();
    }
}
