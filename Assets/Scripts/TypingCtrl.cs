﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingCtrl : MiniGame
{

    public string[] phrases;
    public GameObject champDeSaisie;
    public Canvas canvas;

    public GameObject cube;
    public GameObject line;

    public int espcCubes;
    public int espcLignes;

    bool holdingDown = false;

    int currentCase = 0;
    int currentLine = 0;
    int linesWon = 0;
    bool alreadyWon = false;
    float elapsedTime = 0; Color baseColor;

    [SerializeField] private float addedSalt = 5;

    private AudioSource audioS;
    [SerializeField] private AudioClip[] keyboardSounds;
    [SerializeField] private AudioClip errorSound;


    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        LanceMinijeu();
        baseColor = champDeSaisie.transform.GetChild(currentLine).GetChild(0).GetChild(1).GetComponent<Text>().color;
        baseColor.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            GenerateRandomKeyboardSound();
        }
        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            holdingDown = true;
        }

        if (!Input.anyKey && holdingDown)
        {
            holdingDown = false;

            if (!Input.GetKeyUp(KeyCode.Return))
            {
                alreadyWon = false;
                if (currentCase < champDeSaisie.transform.GetChild(currentLine).childCount)
                {
                    Color opaque = baseColor;
                    opaque.a = 1f;
                    champDeSaisie.transform.GetChild(currentLine).GetChild(currentCase).GetChild(1).GetComponent<Text>().color = opaque;
                    currentCase++;
                }
                else
                {
                    currentCase = 0;
                    foreach (Transform child in champDeSaisie.transform.GetChild(currentLine))
                    {
                        Color transparent = baseColor;
                        transparent.a = 0f;
                        child.GetChild(1).GetComponent<Text>().color = transparent;
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log(currentCase);
                Debug.Log(champDeSaisie.transform.GetChild(currentLine).childCount);
                if (currentCase == champDeSaisie.transform.GetChild(currentLine).childCount)
                {
                    if (!alreadyWon)
                    {
                        currentLine++;
                        currentCase = 0;

                        foreach (Transform child in champDeSaisie.transform.GetChild(currentLine - 1))
                        {
                            Color green = new Color(0, 255, 0);
                            child.GetChild(1).GetComponent<Text>().color = green;
                        }
                        linesWon++;
                        alreadyWon = true;

                        if (currentLine == phrases.Length)
                        {
                            Debug.Log("mini jeu fini");

                            GameWin();
                            // retour au menu
                        }
                    }
                }
                else
                {
                    audioS.PlayOneShot(errorSound);
                    Debug.Log("loseLine");
                    elapsedTime = 0f;
                    StartCoroutine("FlashRed");
                    // la jauge de sel augmente de 5

                    GameManager.Instance.AddAnger(addedSalt);

                    //if (currentLine == phrases.Length)
                    //{
                    //    Debug.Log("mini jeu fini");
                    //    // retour au menu
                    //}
                }
            }

        }
        elapsedTime += Time.deltaTime;
    }

    IEnumerator FlashRed()
    {
        while (elapsedTime < .8f)
        {
            foreach (Transform child in champDeSaisie.transform.GetChild(currentLine))
            {
                if (child.GetChild(1).GetComponent<Text>().color.a == 1f)
                {
                    Color red = new Color(255, 0, 0);
                    child.GetChild(1).GetComponent<Text>().color = red;
                }
            }
            yield return new WaitForSeconds(0.1f);
            foreach (Transform child in champDeSaisie.transform.GetChild(currentLine))
            {
                if (child.GetChild(1).GetComponent<Text>().color.a == 1f)
                {
                    child.GetChild(1).GetComponent<Text>().color = baseColor;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void LanceMinijeu()
    {
        RectTransform champ = champDeSaisie.GetComponent<RectTransform>();

        for (int i = 0; i < phrases.Length; i++)
        {
            string ph = phrases[i];

            GameObject newLine = Instantiate(line, champ.position, Quaternion.identity, champ);

            for (int j = 0; j < ph.Length; j++)
            {
                char c = ph[j];

                Vector2 positionRelative = new Vector2(espcCubes * j, espcLignes * i);

                GameObject newCube = Instantiate(cube, champ.position, Quaternion.identity, newLine.transform);
                Vector2 newAPos = new Vector2(champ.anchoredPosition.x - champ.sizeDelta.x / 2, champ.anchoredPosition.y + champ.sizeDelta.y / 2) + positionRelative;
                newCube.GetComponent<RectTransform>().anchoredPosition = newAPos;

                newCube.transform.GetChild(1).GetComponent<Text>().text = c.ToString();
            }
        }
    }

    private void GameWin()
    {
        Destroy(gameObject, 0.3f);

        GameManager.Instance.AddAnger(GameManager.Instance.removedSaltValue);

        associatedTroll.OnMiniGameWin();
    }



    void GenerateRandomKeyboardSound()
    {
        int i = UnityEngine.Random.Range(0, keyboardSounds.Length);

        //Debug.Log(keyboardSounds[i].name);
        audioS.PlayOneShot(keyboardSounds[i]);
    }
}
