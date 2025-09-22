using UnityEngine;
using UnityEngine.UI;

public class ChangeHealthValue : MonoBehaviour
{

    [SerializeField] private Slider slider;
    private void OnEnable()
    {
        GlobalEvents.OnPlayerHealthChange += ChangeHealth;
    }
    private void OnDisable()
    {
        GlobalEvents.OnPlayerHealthChange -= ChangeHealth;
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void ChangeHealth(int value)
    {
        slider.value = value;
    }
}