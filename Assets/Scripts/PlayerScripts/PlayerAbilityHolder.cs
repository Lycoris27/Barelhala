using UnityEngine;
using UnityEngine.Events;


public class PlayerAbilityHolder : MonoBehaviour
{
    [SerializeField] private AbilityBaseScript ability01;
    [SerializeField] private AbilityBaseScript ability02;

    public UnityEvent ActivateAbility01;
    public UnityEvent ActivateAbility02;

    private void Awake()
    {
        ActivateAbility01.AddListener(ActivateFirstAbility);
        ActivateAbility02.AddListener(ActivateSecondAbility);
    }

    public void ActivateFirstAbility()
    {
        ability01.ActivateAbility(this.gameObject);
    }
    public void ActivateSecondAbility()
    {
        ability02.ActivateAbility(this.gameObject);
    }
}
