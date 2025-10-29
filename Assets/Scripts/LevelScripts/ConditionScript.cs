using UnityEngine;

public class ConditionScript : MonoBehaviour
{
    public static ConditionScript instance;
    public static void WinConditionMet()
    {
        GlobalEvents.OnPause();
        UIManagerScript.EngageUI(3);
    }
    public static void LossConditionMet()
    {
        GlobalEvents.OnPause();
        UIManagerScript.EngageUI(4);
    }
    public static void PlaytestEnd()
    {
        GlobalEvents.OnPause();
        UIManagerScript.EngageUI(5);
    }
}
