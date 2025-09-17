using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static event System.Action OnTriggerPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnTriggerPlayer?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
