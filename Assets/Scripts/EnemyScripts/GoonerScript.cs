using UnityEngine;
using System.Collections.Generic;

public class GoonerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> locationNodes;
    [SerializeField] private List<GameObject> gooners;
    [SerializeField] private float goonerTimer;
    [SerializeField] private float goonerDelay;
    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > goonerTimer)
        {
            ShuffleGooners();
        }
    }

    private void ShuffleGooners()
    {
        foreach (var gooner in gooners)
        {
            // Randomly choose between 0 and 1

            // if random choice is 0, gooner moves

            // if random choice is 1, gooner shoots

            // extended scope: if random choice is 2, gooner shoots and moves

            //Delay activates before next iteration
        }
    }
}
