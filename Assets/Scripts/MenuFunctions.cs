using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    private AudioSource audioS;
    [SerializeField]
    private AudioClip clickSound;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void Restart()
    {
        audioS.PlayOneShot(clickSound);
        gameManager.Restart();
    }

    public void Exit()
    {
        audioS.PlayOneShot(clickSound);
        gameManager.ExitGame();
    }
}
