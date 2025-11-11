using Unity.Behavior;
using UnityEngine;

public class EnemyInit : MonoBehaviour
{
    [SerializeField] private AbilityBaseScript goonAttackScript;

    void Awake()
    {
        // find the player in the scene however you prefer
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        var agent = GetComponent<BehaviorGraphAgent>();
        agent.SetVariableValue("Player", player);

        // Create a runtime instance of the ScriptableObject
        if (goonAttackScript != null)
        {
            AbilityBaseScript goonAttackInstance = ScriptableObject.CreateInstance(goonAttackScript.GetType()) as AbilityBaseScript;

            // Optionally, copy values from the original ScriptableObject to the instance
            // This is necessary if your ScriptableObject has serialized fields you want to retain
            if (goonAttackInstance != null)
            {
                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(goonAttackScript), goonAttackInstance);
                agent.SetVariableValue("GoonAttack", goonAttackInstance);
            }
        }
    }
}