using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneManagerScript : MonoBehaviour
{

    private void OnEnable()
    {
        GlobalEvents.OnPauseGame += Pause;
        GlobalEvents.OnPlayGame += Resume;
    }
    private void OnDisable()
    {
        GlobalEvents.OnPauseGame -= Pause;
        GlobalEvents.OnPlayGame -= Resume;
    }

    public void LoadNewScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }
    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneRoutine());
    }
    private IEnumerator LoadNextSceneRoutine()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        bool isLastScene = currentScene.buildIndex + 1 >= SceneManager.sceneCountInBuildSettings;

        int nextIndex = isLastScene
            ? 0
            : currentScene.buildIndex + 1;

        // Always load the next scene
        SceneManager.LoadSceneAsync(nextIndex);

        // If we're wrapping from the last scene, ONLY engage UI 2
        if (isLastScene)
        {
            UIManagerScript.EngageUI(2);
            yield break;
        }

        // Normal transition flow
        UIManagerScript.EngageUI(5);
        GlobalEvents.OnPause();

        // Unscaled delay – works even when timeScale == 0
        yield return new WaitForSecondsRealtime(3f);

        GlobalEvents.OnResume();
        UIManagerScript.EngageUI(2);
    }



    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void Resume()
    {
        Time.timeScale = 1;
    }
    private void Pause()
    {
        Time.timeScale = 0;
    }
}
