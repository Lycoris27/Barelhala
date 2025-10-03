using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private bool destroyOnCollide = true;

    public static event System.Action OnTriggerPlayer;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") { return; }
        OnTriggerPlayer?.Invoke();

        if (!destroyOnCollide) { return; }
        Destroy(this.gameObject);
    }
}
