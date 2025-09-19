using UnityEngine;
using System.Collections.Generic;

public class PersistentObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> persistentObjects;

    private static PersistentObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            // First instance, keep it alive
            instance = this;

            foreach (GameObject obj in persistentObjects)
            {
                DontDestroyOnLoad(obj);
            }
        }
        else
        {
            foreach (GameObject obj in persistentObjects)
            {
                if (obj != null && obj != this.gameObject)
                {
                    Destroy(obj);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
