using UnityEngine;
using System.Collections.Generic;
using System;

public class InteractibleManagerScript : MonoBehaviour
{
    private HashSet<GameObject> interactibleList = new HashSet<GameObject>();
    [SerializeField] private int interactiblesCollected = 0;
    [SerializeField] private int interactiblesRequired = 5;
    [SerializeField] private bool isNearDoor = false;

    private void OnEnable()
    {
        PlayerInputManagerScript.OnInteractPerformed += Interact;
        ObjectInteractionScript.OnPlayerEnterZone += AddObject;
        ObjectInteractionScript.OnPlayerExitZone += RemoveObject;
        LevelManagerScript.OnResetLevel += ResetScript;
        

    }
    private void OnDisable()
    {
        PlayerInputManagerScript.OnInteractPerformed -= Interact;
        ObjectInteractionScript.OnPlayerEnterZone -= AddObject;
        ObjectInteractionScript.OnPlayerExitZone -= RemoveObject;
        LevelManagerScript.OnResetLevel -= ResetScript;
    }

    private void Interact()
    {
        if (isNearDoor && interactiblesCollected == interactiblesRequired)
        {
            ConditionScript.WinConditionMet();
        }
        foreach(GameObject obj in interactibleList)
        { 
            interactiblesCollected += 1;
            Destroy(obj);
        }
        interactibleList = new HashSet<GameObject>();
        Debug.Log($"Interactibles Collected = {interactiblesCollected}");
    }
    private void AddObject(GameObject obj, bool value)
    {
        if (value) { isNearDoor = true; return; }
        else if (!interactibleList.Contains(obj))
        {
            print("Object Received");
            interactibleList.Add(obj);
        }
    }
    private void RemoveObject(GameObject obj, bool value)
    {
        if (value) { isNearDoor = false; return; }
        if (interactibleList.Contains(obj))
        {
            interactibleList.Remove(obj);
        }
    }

    private void ResetScript()
    {
        interactiblesCollected = 0;
    }
}
