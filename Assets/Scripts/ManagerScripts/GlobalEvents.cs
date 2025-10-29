using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GlobalEvents: MonoBehaviour
{
    public static GlobalEvents Instance;

    public static event Action OnAbilityCompleted;
    public static event Action<int> OnPlayerHealthChange;
    public static event Action OnPauseGame;
    public static event Action OnPlayGame;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // ensure only one exists
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // optional if you want it persistent
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
    }

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

    public static void StartDelay(float time, Action callback)
    {
       Instance.StartCoroutine(DelayRoutine(time, callback));
    }

    private static IEnumerator DelayRoutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }
}