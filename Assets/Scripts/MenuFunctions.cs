using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    
    public void Restart()
    {
        gameManager.Restart();
    }

    public void Exit()
    {
        gameManager.ExitGame();
    }
}
