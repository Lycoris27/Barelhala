using UnityEngine;

public class ObjectInteractionScript : MonoBehaviour
{
    public static event System.Action<GameObject> OnPlayerEnterZone;
    public static event System.Action<GameObject> OnPlayerExitZone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("Sent Object to list");
            OnPlayerEnterZone?.Invoke(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            OnPlayerExitZone?.Invoke(this.gameObject);
        }
    }
}
