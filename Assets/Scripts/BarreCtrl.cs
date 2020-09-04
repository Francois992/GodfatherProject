using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreCtrl : MiniGame
{
    public RectTransform background;

    RectTransform rect;

    [SerializeField] private GameObject Spacebar;

    // Start is called before the first frame update
    void Start()
    {
        rect = Spacebar.transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (rect.sizeDelta.x < background.sizeDelta.x)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2 bigger = new Vector2(background.sizeDelta.x / 20, 0);
                rect.sizeDelta += bigger;
                rect.anchoredPosition += bigger / 2;
            }
        }
        else
        {
            Debug.Log("le minijeu est fini");
            // revenir au jeu principal

            GameWin();
        }
    }

    private void GameWin()
    {
        Destroy(gameObject);

        GameManager.Instance.AddAnger(-8);

        associatedTroll.OnMiniGameWin();
    }
}
