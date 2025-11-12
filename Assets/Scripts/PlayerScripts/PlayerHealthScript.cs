using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int maxPlayerHealth = 5;
    [SerializeField] private int currentHealth = 0;
    [SerializeField] private float delayBeforeCanDamage = 2f;
    [SerializeField] private bool canTakeDamage = true;

    private void Awake()
    {
        // Send a signal to the UI Manager to update the player health system, can send a signal to activate it in the first place
    }

    private void OnEnable()
    {
        PlayerCollision.OnTriggerPlayer += PlayerTakeDamage;
        LevelManagerScript.OnResetLevel += ResetScript;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        PlayerCollision.OnTriggerPlayer -= PlayerTakeDamage;
        LevelManagerScript.OnResetLevel -= ResetScript;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void PlayerTakeDamage() 
    {
        // when called, it increases the players health by 1 stage, the higher the closer to death
        // if the current health is high enough, should lose the player the game. Still yet to be hooked up
        if (canTakeDamage)
        {
            currentHealth += 1;
            //PlaytestDamage();
            if (currentHealth <= maxPlayerHealth)
            {
                UpdateHealthUI();
                StartCoroutine(ActivatePlayerImmunity());

                if(currentHealth == maxPlayerHealth - 1)
                {
                    PlayerAbilityHolder holder = FindFirstObjectByType<PlayerAbilityHolder>();
                    holder.ActivateAbility01.Invoke();
                    print("BinkBonkBonk");
                }
                if (currentHealth == maxPlayerHealth)
                {
                    PlayerAbilityHolder holder = FindFirstObjectByType<PlayerAbilityHolder>();
                    holder.ActivateAbility02.Invoke();
                    print("Bingus Tingus");
                }
            }
            else if (currentHealth > maxPlayerHealth)
            {
                //PlaytestDeaths();
                ConditionScript.LossConditionMet();
            }
        }
    }
    /*
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI deathText;
    public int damageInt;
    public int deathInt;
    private void PlaytestDamage()
    {
        damageInt += 1;
        damageText.text = $"Damage taken: {damageInt}";
    }
    private void PlaytestDeaths()
    {
        deathInt += 1;
        deathText.text = $"Deaths: {deathText}";
    }
    public void ResetPlaytest()
    {
        damageInt = 0;
        deathInt = 0;
        damageText.text = $"Damage taken: {damageInt}";
        deathText.text = $"Deaths: {deathText}";
    }
    */
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) return;
        ResetScript();
    }


    private void PlayerRecoverDamage() // when called, it reduces the players health by 1 stage, the lower the further from death
    {
        if (currentHealth > 4 )
        {
            currentHealth -= 1;
            UpdateHealthUI();
        }
    }
    private void ResetScript()
    {
        currentHealth = 0;
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        GlobalEvents.ChangePlayerHealth(currentHealth);
    }

    private IEnumerator ActivatePlayerImmunity()
    {
        Debug.Log("Can't take damage");
        canTakeDamage = false;
        yield return new WaitForSeconds(delayBeforeCanDamage);
        canTakeDamage = true;
        Debug.Log("Can take damage");
    }


}
