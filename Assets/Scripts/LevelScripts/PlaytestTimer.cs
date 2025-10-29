using UnityEngine;

public class PlaytestTimer : MonoBehaviour
{
    [SerializeField] private float timerEnd = 120f;
    private float timer = 0f;
    private bool playtestRunning = false;

    public void StartPlaytestTimer()
    {
        playtestRunning = true;
    }
    public void EndPlaytest()
    {
        playtestRunning = false;
        ConditionScript.PlaytestEnd();
    }
    private void Update()
    {
        if (playtestRunning)
        {
            timer += Time.deltaTime;
            if (timer >= timerEnd)
            {
                EndPlaytest();
                timer = 0f;
            }
        }

    }
}
