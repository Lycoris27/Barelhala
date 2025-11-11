using UnityEngine;
using System.Collections;


public abstract class AbilityBaseScript : ScriptableObject
{
    public abstract GameObject Prefab {get;}

    // Initial Variables
    [Header("Initial Variables")]
    public abstract float InitialDelay { get; } // time before ability starts
    public abstract Vector3 Size { get; } // scale of ability size
    public abstract bool IsTargettingPlayer { get; } // If ability targets player, not heat-seeking
    public abstract float ConeAngle { get; } // The range for which the objects shoot out from the character
    public abstract float Direction { get; } // the angle from the z axis the ability shoots from
    public abstract int DirectionChange { get; }

    // Durational Variables
    [Header("Spawning Variables")]
    public abstract float SpawnRate { get; } // Times the ability will trigger in 1 second
    public abstract float SpawnCount { get; } // Amount of objects spawned when activated
    public abstract float SpawnRadius { get; }

    [Header("Linear Velocity Variables")]
    public abstract float LinearSpeed { get; } // Linear Velocity 
    public abstract float LinearSpeedDelay { get; } // time before Linear Velocity Changes

    [Header("Angular Velocity Variables")]
    public abstract float AngularSpeed { get; } // Deviation from the direction, occurs after every activation
    public abstract float AngularSpeedDelay { get; } // Time it takes before the rotation occurrs
    public abstract bool RotateAroundObject { get; }

    // Might need to add a bool to determine if the bullets should rotate around the enemy

    //Something different
    public abstract int DirectionSwapCounter { get; } // Time it takes before the 
    public abstract bool RepeatingCounter { get; } // whether the rotation keeps happening

    // End Variables
    [Header("End Variables")]

    public abstract bool IsLifetimeInfinite { get;  }
    public abstract float Lifetime { get; } // Time before ability ends
    public abstract float Decay { get; } // Time after ability ends to get rid of all persisting objects

    public abstract void ActivateAbility(GameObject ownerObject);
}
