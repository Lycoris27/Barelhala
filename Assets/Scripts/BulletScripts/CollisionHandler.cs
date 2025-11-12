using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [Header("Player Interaction")]
    [SerializeField] private bool playerCollision = true; // deals player damage
    [SerializeField] private bool onPlayerCollideDelete = true; // kills object when interacted
    [Header("Wall Interaction")]
    [SerializeField] private bool onWallCollideDelete = false; // On wall collide, deletes object, should return
    [SerializeField] private bool onWallCollideBounce = false; // on wall collide, bounces object
    [SerializeField] private float bounceDampening = 1f;
    [Header("Goon Interaction")]
    [SerializeField] private bool goonCollision = false; // On goon collide, deals damage
    [SerializeField] private bool onGoonCollideDelete = false; // On goon collide, kills bullet
    [Header("Crate Interaction")]
    [SerializeField] private bool crateCollision = false; // On collision with a crate, deals damage to it
    [SerializeField] private bool onCrateCollideDelete = false;

    public static event System.Action OnTriggerPlayer;
    public static event System.Action OnTriggerGoon;
    private Rigidbody rb;

    private void Awake()
    {
        if (onWallCollideBounce)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerCollision)
        {
            OnTriggerPlayer?.Invoke();
            if(onPlayerCollideDelete)
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.CompareTag("Wall"))
        {
            if (onWallCollideDelete)
            {
                Destroy(this.gameObject);
            }
            if (onWallCollideBounce)
            {
                Vector3 contactPoint = other.ClosestPoint(rb.position);
                Vector3 normal = (rb.position - contactPoint).normalized;

                // Reflect velocity and apply dampening
                rb.linearVelocity = Vector3.Reflect(rb.linearVelocity, normal) * bounceDampening;

                // Optional: Reposition slightly to prevent sticking
                rb.position = contactPoint + normal * 0.01f;
            }
        }
        else if (other.CompareTag("Goon") && goonCollision)
        {
            other.GetComponent<Health>()?.OnHit.Invoke();
            if (onGoonCollideDelete)
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.CompareTag("Crate") && crateCollision)
        {
            other.GetComponent<Health>()?.OnHit.Invoke();
            if (onCrateCollideDelete)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
