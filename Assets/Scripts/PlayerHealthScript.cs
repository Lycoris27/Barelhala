using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int maxPlayerHealth = 5;
    [SerializeField] private int currentHealth = 0;
    [SerializeField] private bool canTakeDamage = true;

    private void Awake()
    {
        currentHealth = maxPlayerHealth;
    }

    private void OnEnable()
    {
        PlayerCollision.OnTriggerPlayer += PlayerTakeDamage;
    }
    private void OnDisable()
    {
        PlayerCollision.OnTriggerPlayer -= PlayerTakeDamage;
    }

    private void PlayerTakeDamage()
    {
        if(canTakeDamage)
        {
            currentHealth -= 1;
            if (currentHealth > 0)
            {
                StartCoroutine(ActivatePlayerImmunity());
            }
            else if (currentHealth <= 0)
            {
                print("you Lose!");
            }
        }
    }
    private IEnumerator ActivatePlayerImmunity()
    {
        Debug.Log("Can't take damage");
        canTakeDamage = false;
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
        Debug.Log("Can take damage");
    }
}
