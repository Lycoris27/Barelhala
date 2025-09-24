using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Weapons/Bullet")]
public class BulletlogicScript : ScriptableObject
{

    [SerializeField] private AbilityType abilityType;
    private enum AbilityType
    {
        Bullet,
        Explosion,
        Laser
    }

    [SerializeField] private GameObject bulletPrefab;
    public GameObject BulletPrefab => bulletPrefab;

    [SerializeField] private GameObject explosionPrefab;
    public GameObject ExplosionPrefab => ExplosionPrefab;

    [SerializeField] private GameObject laserPrefab;
    public GameObject LaserPrefab => laserPrefab;


    [Header("Modifiers")]

    // Bullet Modifiers

    [Tooltip("Determines the bullets speed")]
    [SerializeField] private float speed;
    [Tooltip("Determines the bullets rate of spawning")]
    [SerializeField] private float spawnRate;
    [Tooltip("Determines the amount of bullets that spawn when spawning occurs")]
    [SerializeField] private float spawnCount;


    // Explosion Modifiers

    // Lazer Modifiers

    // general Modifiers
    //[Tooltip("determines the size of the ability
    //\n For Bullet: the bullet size
    //\n for explosion, the blast size
    //\n For Lazer: the width of the beam")]
    //[SerializeField] private float size;
    [Tooltip("Determines the delay before the ability activates, for windups")]
    [SerializeField] private float delay;
    [Tooltip("Determines the length of the ability's lifespan")]
    [SerializeField] private float lifetime;


    /*
    [SerializeField] private SpawnPattern spawnPattern;
    public enum SpawnPattern
    {
        Cone,
        Sphere,
        Circle,
        Line
    }
    */
    

}
