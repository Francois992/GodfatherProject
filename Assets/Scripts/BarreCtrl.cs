using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreCtrl : MonoBehaviour
{
    public RectTransform background;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = transform as RectTransform;
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
        }
    }
}
