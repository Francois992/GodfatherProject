using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreCtrl : MiniGame
{
    public RectTransform background;
    public float decaySpeed;

    RectTransform rect;

    [SerializeField] private GameObject Loadbar;
    [SerializeField] private GameObject SpaceBar;

    [SerializeField] private Sprite SpaceBarPressed;
    [SerializeField] private Sprite SpaceBarUnPressed;

    // Start is called before the first frame update
    void Start()
    {
        rect = Loadbar.transform as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (rect.sizeDelta.x > 0)
        {
            Vector2 smaller = new Vector2(decaySpeed, 0);
            rect.sizeDelta -= smaller;
            rect.anchoredPosition -= smaller / 2;
        }

        if (rect.sizeDelta.x < background.sizeDelta.x)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                Vector2 bigger = new Vector2(background.sizeDelta.x / 20, 0);
                rect.sizeDelta += bigger;
                rect.anchoredPosition += bigger / 2;
            }


            if (Input.GetKey(KeyCode.Space))
            {
                SpaceBar.GetComponent<Image>().sprite = SpaceBarPressed;
            }
            else
            {
                SpaceBar.GetComponent<Image>().sprite = SpaceBarUnPressed;
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

        GameManager.Instance.AddAnger(GameManager.Instance.removedSaltValue);

        associatedTroll.OnMiniGameWin();
    }
}
