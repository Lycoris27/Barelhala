using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorScript : MonoBehaviour
{
    [SerializeField] private bool door1;
    [SerializeField] private bool door2;

    public static event System.Action OnPlayerProgress;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ping");
        if (!other.CompareTag("Player")) return;

        if (door1)
        {
            SceneManager.LoadScene(1); 
            return;
        }
        if (door2)
        {
            SceneManager.LoadScene(0);
        }
    }
}
