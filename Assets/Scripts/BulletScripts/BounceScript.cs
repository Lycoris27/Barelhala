using UnityEngine;

public class BounceScript : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;       // Only bounce off these layers
    [SerializeField] private float bounceDampening = 0.9f; // Multiply velocity by this on each bounce

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only bounce off walls
        if (((1 << other.gameObject.layer) & wallLayer) != 0)
        {
            // Calculate approximate contact normal
            Vector3 contactPoint = other.ClosestPoint(rb.position);
            Vector3 normal = (rb.position - contactPoint).normalized;

            // Reflect velocity and apply dampening
            rb.linearVelocity = Vector3.Reflect(rb.linearVelocity, normal) * bounceDampening;

            // Optional: Reposition slightly to prevent sticking
            rb.position = contactPoint + normal * 0.01f;
        }
    }
}