using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour
{
    /*
    // Ability Base Script Variables

    [SerializeField] private GameObject explosionPrefab;  // private backing field
    public override GameObject Prefab => explosionPrefab;

    [SerializeField] private float explosionLifetime; 
    public override float Lifetime => explosionLifetime;

    [SerializeField] private bool isTargetting; 
    public override bool IsTargetting => isTargetting;

    [SerializeField] private float explosionSize;
    public override float Size => explosionSize;

    // Nulled Base Script Elements
    public override float SpawnRadius => 0f;
    public override float SpawnCount => 0f; // nulled //[SerializeField] private float bulletCount;
    public override float SpawnRate => 0f; //[SerializeField] private float spawnRate;
    public override float Speed => 0f;  //[SerializeField] private float speed;
    public override float Delay => 0f; // [SerializeField] private float delay;
    public override float Rotation => 0f;    //[SerializeField] private float rotation;
    public override float Direction => 0f;    //[SerializeField] private float direction;

    // Unique fields to the script
    private bool active = false;

    [SerializeField] private float coneAngle = 360f;

    private GameObject playerRef;// = null;


    public override IEnumerator ActivateAbility()
    {
        yield return new WaitForSeconds(Delay);
        active = true;
        StartCoroutine(GenerateExplosions());
        yield return new WaitForSeconds(Lifetime);
        active = false;
    }

    private IEnumerator GenerateExplosions()
    {

        yield return new WaitForSeconds(1f / SpawnRate);
    }
    */
}
