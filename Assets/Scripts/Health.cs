using UnityEngine;
using UnityEngine.Events;

public class Health: MonoBehaviour
{
    public UnityEvent OnHit;
    [SerializeField] private int healthTotal = 2;
    [SerializeField] private int healthCurrent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        OnHit.AddListener(ReduceHealth);
        healthCurrent = healthTotal;
    }

    private void ReduceHealth()
    {
        healthCurrent--;
        Debug.Log("burnt toast");

        if (healthCurrent <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
