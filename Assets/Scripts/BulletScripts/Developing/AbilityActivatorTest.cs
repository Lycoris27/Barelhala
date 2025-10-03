using UnityEngine;

public class AbilityActivatorTest : MonoBehaviour
{
    [SerializeField] private AbilityBaseScript[] abilities;

    private void Start()
    {
        StartCoroutine(abilities[0].ActivateAbility());
    }
}
