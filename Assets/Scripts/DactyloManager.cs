using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DactyloManager : MonoBehaviour
{
    [SerializeField]
    private string[] wordList;
    private string wordToCopy;
    private int wordPosition; //Position dans le mot à recopier
    private bool win;

    [SerializeField]
    private GameObject LetterPrefab;

    public int successNeeded = 3;
    private int currentSuccess;

    [SerializeField]
    private InputField inputF;
    [SerializeField]
    private Text currentSuccessText;

    private List<Text> letters = new List<Text>();


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Add Hide Input Caret
        InitNewWord();
    }

    void Update()
    {
        // Event qui appelle OnInputValueChange à chaque changement de valeur
        inputF.onValueChanged.AddListener(delegate { OnInputValueChange(); });

        if (Input.anyKey && !win && inputF.text != "")
        {
            CheckIfValid();
        }

    }

    void InitNewWord()
    {
        EmptyPreviousLetters();

        inputF.text = "";
        inputF.Select();
        inputF.ActivateInputField();

        wordPosition = 0;

        // Retourne chiffre aléatoire entre 0 et wordlist.length => Améliorer le Random.Range si possible
        int value = Random.Range(0, wordList.Length - 1);
        wordToCopy = wordList[value].ToUpper();

        //wordToCopy.text = wordList[value];

        InstantiateLetters();

    }

    void EmptyPreviousLetters()
    {
        foreach(Transform child in GameObject.Find("WordToCopy").transform)
        {
            Destroy(child.gameObject);
        }
    }

    void InstantiateLetters()
    {
        letters.Clear();

        for(int i = 0; i < wordToCopy.Length; i++)
        {
            GameObject tmp = Instantiate(LetterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            tmp.transform.SetParent(GameObject.Find("WordToCopy").transform, false);
            tmp.GetComponentInChildren<Text>().text = wordToCopy[i].ToString();

            letters.Add(tmp.GetComponentInChildren<Text>());
        }

    }

    void OnInputValueChange()
    {
        TransformToUpperCase();
    }

    // Fonction qui passe le texte de l'input en majuscule a chaque changement
    void TransformToUpperCase()
    {
        inputF.text = inputF.text.ToUpper();
    }

    void CheckIfValid()
    {
        if (inputF.text == wordToCopy[wordPosition].ToString()) 
        {
            letters[wordPosition].text = "";
            wordPosition++;
            Debug.Log("OK");

            if(wordPosition == wordToCopy.Length)
            {
                AddSuccess();
                if (currentSuccess < 3)
                {
                    InitNewWord();
                }
                else
                {
                    win = true;
                    // Passer à la suite
                    Debug.Log("C'est Win");
                }
            }
        }
        else
        {
            // Faire trembler écran
            // Augmenter jauge de sel

            foreach (Transform child in GameObject.Find("WordToCopy").transform)
            {
                // Do Something to all letters
            }

            Debug.Log("NOPE");
        }

        inputF.text = "";

        /*
        if(inputF.text == wordToCopy && currentSuccess < 3)
        {
            Debug.Log("Mot Valide");
            inputF.DeactivateInputField();

            if(currentSuccess < 3)
            {
                AddSuccess();
                InitNewWord();
            }
            else
            {
                currentSuccess++;
                // Do Something
                Debug.Log("C'est Win");
            }
        }
        */
    }

    string getCurrentProgression(int length)
    {
        string res = "";
        for(int i = 0; i < length; i++)
        {
            res += wordToCopy[i].ToString();
        }

        Debug.Log(res);
        return res;
    }

    // Ajoute un au nombre de succès
    void AddSuccess()
    {
        currentSuccess++;
        currentSuccessText.text = currentSuccess.ToString();
    }
}
