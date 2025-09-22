using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance;
    // If I hold a list of the UI elements, I can auto assign them as I move between scenes
    [SerializeField] private List<GameObject> heldUIElements;
    [SerializeField] private static List<GameObject> HeldUIElements;

    [Tooltip("Check to ensure EventSystem is hooked up")]
    [SerializeField] private EventSystem eventSystem;

    private void Awake()
    {
        if (instance == null)
        {
            // First instance, keep it alive
            instance = this;
        }
        HeldUIElements = heldUIElements;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (eventSystem == null)
        {
            eventSystem = FindAnyObjectByType<EventSystem>();
        }
        EngageUI(0);
    }

    public static void EngageUI(int UINumber)
    {
        foreach (GameObject obj in HeldUIElements)
        {
            if(HeldUIElements[UINumber] == obj) { obj.SetActive(true); }
            else { obj.SetActive(false); }
        }
    }
    public void SetFirstSelected(GameObject obj)
    {
        eventSystem.firstSelectedGameObject = obj;
    }
}
