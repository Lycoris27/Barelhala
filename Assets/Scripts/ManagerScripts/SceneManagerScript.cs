using UnityEngine;
using System.Collections.Generic;

public class SceneManagerScript : MonoBehaviour
{
    private HashSet<GameObject> interactibleList = new HashSet<GameObject>();
    private int interactiblesCollected = 0;


    private void OnEnable()
    {
        PlayerInputManagerScript.OnInteractPerformed += Interact;
        ObjectInteractionScript.OnPlayerEnterZone += AddObject;
        ObjectInteractionScript.OnPlayerExitZone += RemoveObject;
        

    }
    private void OnDisable()
    {
        PlayerInputManagerScript.OnInteractPerformed -= Interact;
        ObjectInteractionScript.OnPlayerEnterZone -= AddObject;
        ObjectInteractionScript.OnPlayerExitZone -= RemoveObject;
        
    }

    private void Interact()
    {
        foreach(GameObject obj in interactibleList)
        { 
            interactiblesCollected += 1;
            Destroy(obj);
        }
        interactibleList = new HashSet<GameObject>();
        Debug.Log($"Interactibles Collected = {interactiblesCollected}");
    }

    private void AddObject(GameObject obj)
    {
        if (!interactibleList.Contains(obj))
        {
            print("Object Received");
            interactibleList.Add(obj);
        }
    }
    private void RemoveObject(GameObject obj)
    {
        if (interactibleList.Contains(obj))
        {
            interactibleList.Remove(obj);
        }
    }
}
