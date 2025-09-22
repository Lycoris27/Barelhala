using UnityEngine;

public class ObjectInteractionScript : MonoBehaviour
{
    [SerializeField] private bool isDoor;

    public static event System.Action<GameObject, bool> OnPlayerEnterZone;
    public static event System.Action<GameObject, bool> OnPlayerExitZone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("Sent Object to list");
            OnPlayerEnterZone?.Invoke(this.gameObject, isDoor);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            OnPlayerExitZone?.Invoke(this.gameObject, isDoor);
        }
    }
}
