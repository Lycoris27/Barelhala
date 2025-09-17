using UnityEngine;

public class LOSScript : MonoBehaviour
{
    public static event System.Action<GameObject> OnPlayerEnterEnemyLOS;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            OnPlayerEnterEnemyLOS?.Invoke(other.gameObject);
        }
    }
}
