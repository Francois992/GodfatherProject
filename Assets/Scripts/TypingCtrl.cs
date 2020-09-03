using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingCtrl : MonoBehaviour
{

    public string[] phrases;
    public GameObject cube;
    public GameObject champDeSaisie;
    public Canvas canvas;

    public int espcCubes;
    public int espcLignes;

    bool holdingDown = false;

    int currentCase = 0;

    // Start is called before the first frame update
    void Start()
    {
        LanceMinijeu();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetKey(KeyCode.Return))
        {
            holdingDown = true;
        }

        if (!Input.anyKey && holdingDown && !Input.GetKeyUp(KeyCode.Return))
        {
            holdingDown = false;

            champDeSaisie.transform.GetChild(currentCase).GetChild(0).GetChild(0).gameObject.SetActive(false);
            currentCase++;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            foreach (string ph in phrases)
            {
                foreach (char c in ph)
                {
                }
            }
        }
    }

    void LanceMinijeu()
    {
        RectTransform champ = champDeSaisie.GetComponent<RectTransform>();

        for (int i = 0; i < phrases.Length; i++)
        {
            string ph = phrases[i];

            for (int j = 0; j < ph.Length; j++)
            {
                char c = ph[j];

                Vector2 positionRelative = new Vector2(espcCubes * j, espcLignes * i);

                GameObject newCube = Instantiate(cube, champ.position, Quaternion.identity, champ);
                Vector2 newAPos = new Vector2(champ.anchoredPosition.x - champ.sizeDelta.x / 2, champ.anchoredPosition.y + champ.sizeDelta.y / 2) + positionRelative;
                newCube.GetComponent<RectTransform>().anchoredPosition = newAPos;

                newCube.transform.GetChild(0).GetComponent<Text>().text = c.ToString();
            }
        }
    }
}
