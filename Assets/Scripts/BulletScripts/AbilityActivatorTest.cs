using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;


public class AbilityActivatorTest : MonoBehaviour
{
    [System.Serializable]
    public class Abilities
    {
        public List<AbilityBaseScript> abilities;
    }


    [SerializeField] private AbilityCountStyle abilityCountStyle;
    private enum AbilityCountStyle
    {
        Ascending,
        Random,
        None
    }
    [SerializeField] private bool activateAtStart = false;
    [SerializeField] private List<Abilities> abilityList;
    private int abilityNo = 0;

    private void Start()
    {
        if (!activateAtStart) return;

        foreach (AbilityBaseScript abil in abilityList[0].abilities)
        {
            abil.ActivateAbility(this.gameObject);
        }
    }
    public void StartActivatingAbilities()
    {
        foreach (AbilityBaseScript abil in abilityList[abilityNo].abilities)
        {
            abil.ActivateAbility(this.gameObject);
        }
        CheckAbilitiesToActivate();
    }

    public void CheckAbilitiesToActivate()
    {
        if (abilityCountStyle == AbilityCountStyle.None)
            return;
        if (abilityCountStyle == AbilityCountStyle.Ascending)
            ProgressAbilityCount();
        else if (abilityCountStyle == AbilityCountStyle.Random)
            RandomSetAbilityCount();
    }
    private void ProgressAbilityCount()
    {
        abilityNo++;
        if (abilityNo >= abilityList.Count)
            abilityNo = 0;
    }
    private void RandomSetAbilityCount()
    {
        abilityNo = Random.Range(0, abilityList.Count - 1);
    }

}
