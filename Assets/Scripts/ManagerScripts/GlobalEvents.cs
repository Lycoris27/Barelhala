using UnityEngine;
using System;

public class GlobalEvents: MonoBehaviour
{
    public static GlobalEvents instance;

    public static event Action OnAbilityCompleted;
    public static event Action<int> OnPlayerHealthChange;
    public static event Action OnPauseGame;
    public static event Action OnPlayGame;

    public static void InvokeOnAbilityCompleted()
    {
        OnAbilityCompleted?.Invoke();
    }

    public static void ChangePlayerHealth(int value)
    {
        OnPlayerHealthChange?.Invoke(value);
    }
    public static void OnPause()
    {
        OnPauseGame?.Invoke();
    }
    public static void OnResume()
    {
        OnPlayGame?.Invoke();
    }
}