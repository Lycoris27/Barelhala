using UnityEngine;

public class WallCollisionScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {

            Destroy(this.gameObject);
        }
    }
}
