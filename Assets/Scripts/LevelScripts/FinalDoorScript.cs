using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorScript : MonoBehaviour
{
    [SerializeField] private bool door1;
    [SerializeField] private bool door2;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (door1)
        {
            SceneManager.LoadScene(1); 
            return;
        }
    }
}
