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
