using UnityEngine;
using System;

public static class GlobalEvents

{
    public static event Action OnAbilityCompleted;

    public static void InvokeOnAbilityCompleted()
    {
        OnAbilityCompleted?.Invoke();
    }
}