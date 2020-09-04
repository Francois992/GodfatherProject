using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
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
        GameManager.Instance.Restart();
    }

    public void Exit()
    {
        audioS.PlayOneShot(clickSound);
        GameManager.Instance.ExitGame();
    }
}
