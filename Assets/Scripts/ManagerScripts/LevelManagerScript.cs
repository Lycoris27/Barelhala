using UnityEngine;
using System;

public class LevelManagerScript : MonoBehaviour
{
    public static event Action OnResetLevel;
    private void ResetLevel()
    {
        OnResetLevel?.Invoke();
    }

}
