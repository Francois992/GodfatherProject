using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DactyloManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfSuccessToWin = 3;

    [SerializeField]
    private string[] wordList;
    private string wordToCopy;
    private int wordPosition; //Position dans le mot à recopier
    private bool win;

    [SerializeField]
    private GameObject LetterPrefab;
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

        // Au cas où le clique fasse perdre le focus, on le récupère
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            inputF.Select();
            inputF.ActivateInputField();
        }
    }

    // Fonction qui initialise le mot que ce soit à l'écran ou dans la hiérarchie
    // Reset la position dans le mot
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

        InstantiateLetters();
    }

    // Fonction qui détruit les prefabs de lettre dans la hiérarchie 
    // Appelée quand un mot est initialisé
    void EmptyPreviousLetters()
    {
        foreach(Transform child in GameObject.Find("WordToCopy").transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Fonction qui instancie les prefabs de lettre dans l'UI
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

    // Fonction appelée lorsque l'évènement "onValueChange" de l'inputfield est déclenché
    void OnInputValueChange()
    {
        TransformToUpperCase();

        // Vérification afin que la fonction ne soit pas appelée plusieurs fois (défaut de onValueChange, c'est pas précis)
        if (!win && inputF.text != "")
        {
            CheckIfValid();
        }
    }

    // Fonction qui passe le texte de l'input en majuscule
    void TransformToUpperCase()
    {
        inputF.text = inputF.text.ToUpper();
    }


    // Fonction qui vérifie la valididité de l'input à chaque entrée
    // Vérifie également la condition de victoire
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
                if (currentSuccess < numberOfSuccessToWin)
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
    }

    // Fonction qui ajoute un au nombre de succès
    void AddSuccess()
    {
        currentSuccess++;
        currentSuccessText.text = currentSuccess.ToString();
    }
}
