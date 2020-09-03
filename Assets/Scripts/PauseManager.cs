using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }
}
