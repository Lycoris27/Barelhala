using UnityEngine;
using UnityEngine.SceneManagement;


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
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        else
            SceneManager.LoadScene(0);
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
