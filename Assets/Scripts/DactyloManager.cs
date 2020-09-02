using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DactyloManager : MonoBehaviour
{
    [SerializeField]
    private string[] wordList;
    private string wordToCopy;

    [SerializeField]
    private GameObject LetterPrefab;

    public int successNeeded = 3;
    private int currentSuccess;

    [SerializeField]
    private InputField inputF;
    [SerializeField]
    private Text currentSuccessText;

    private bool inputFocused = true;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InitNewWord();
        Debug.Log(wordList[0]);
    }

    void Update()
    {
        // Event qui appelle TransformToUpperCase à chaque changement de valeur
        inputF.onValueChanged.AddListener(delegate { TransformToUpperCase(); });

        // Event qui appelle CheckIfValid à chaque changement de valeur
        inputF.onValueChanged.AddListener(delegate { CheckIfValid(); });

    }

    void InitNewWord()
    {
        inputF.text = "";
        inputF.Select();
        inputF.ActivateInputField();

        // Retourne chiffre aléatoire entre 0 et wordlist.length
        int value = Random.Range(0, wordList.Length - 1);
        wordToCopy = wordList[value].ToUpper();

        //wordToCopy.text = wordList[value];

        InstantiateLetters();

    }

    void InstantiateLetters()
    {
        for(int i = 0; i < wordToCopy.Length; i++)
        {
            // Change 130 par détermination par le code
            GameObject tmp = Instantiate(LetterPrefab, new Vector3(0 + (i * 130), 0, 0), Quaternion.identity) as GameObject;

            tmp.transform.SetParent(GameObject.Find("WordToCopy").transform, false);
            tmp.GetComponentInChildren<Text>().text = wordToCopy[i].ToString();
           
        }

    }

    // Fonction qui passe le texte de l'input en majuscule a chaque changement
    void TransformToUpperCase()
    {
        inputF.text = inputF.text.ToUpper();
    }

    // Ajoute un au nombre de succès
    void AddSuccess()
    {
        currentSuccess++;
        currentSuccessText.text = currentSuccess.ToString();
    }

    void CheckIfValid()
    {


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
}
