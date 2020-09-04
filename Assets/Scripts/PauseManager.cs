using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused;

    [SerializeField]
    private GameObject pauseCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                pauseCanvas.GetComponent<Canvas>().enabled = true;
                isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseCanvas.GetComponent<Canvas>().enabled = false;
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }
}
