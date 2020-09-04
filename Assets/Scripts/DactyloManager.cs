using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DactyloManager : MiniGame
{

    [SerializeField]
    private int numberOfSuccessToWin = 3;

    [SerializeField]
    private float timeHighlightWrong = 1;
    bool wrongAnswer;

    [SerializeField]
    private string[] wordList;
    private string wordToCopy;

    // Pour tester si le nouveau mot est différent du précédent
    private bool wordInList = true;
    private string[] previousWords;

    private int wordPosition; // Position dans le mot à recopier
    private bool win;

    [SerializeField]
    private GameObject LetterPrefab;
    private int currentSuccess;

    [SerializeField]
    private GameObject successLogos;
    private Image[] successLogosList;

    [SerializeField]
    private InputField inputF;

    [SerializeField]
    private ShakeObject objectShaker;

    [SerializeField]
    private AudioClip[] keyboardSounds;
    [SerializeField]
    private AudioClip errorSound;
    [SerializeField]
    private AudioClip validSound;
    private AudioSource audioS;

    private List<Text> letters = new List<Text>();

    [SerializeField] private float addedSalt = 5;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        successLogosList = successLogos.GetComponentsInChildren<Image>();
        previousWords = new string[3];
        // Pour le test, à supprimer
        InitNewWord();
    }

    void Update()
    {
        // Event qui appelle OnInputValueChange à chaque changement de valeur
        inputF.onValueChanged.AddListener(delegate { OnInputValueChange(); });

        // Au cas où le clique fasse perdre le focus, on le récupère
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inputF.Select();
            inputF.ActivateInputField();
        }
        
    }

    // Fonction qui initialise le mot que ce soit à l'écran ou dans la hiérarchie
    // Reset la position dans le mot
    void InitNewWord()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        EmptyPreviousLetters();

        inputF.text = "";
        inputF.Select();
        inputF.ActivateInputField();

        wordPosition = 0;

        // Retourne chiffre aléatoire entre 0 et wordlist.length => Améliorer le Random.Range si possible
        
        do
        {
            int value = Random.Range(0, wordList.Length);

            wordInList = IsWordInList(wordList[value].ToUpper());

            if (!wordInList)
            {
                wordToCopy = wordList[value].ToUpper();
                previousWords[currentSuccess] = wordToCopy;
            }

        } while (wordInList);

        wordInList = true;
        
        InstantiateLetters();
    }

    bool IsWordInList(string word)
    {
        for(int i = 0; i < previousWords.Length; i++)
        {
            if(previousWords[i] == word)
            {
                return true;
            }
        }

        return false;
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
        if(!GameObject.Find("MenuPause").GetComponent<PauseManager>().isPaused)
        {
            TransformToUpperCase();

            // Vérification afin que la fonction ne soit pas appelée plusieurs fois (défaut de onValueChange, c'est pas précis)
            if (!win && inputF.text != "")
            {
                CheckIfValid();
            }
        }
        else
        {
            inputF.text = "";
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
            //Sons de claviers
            GenerateRandomKeyboardSound();

            letters[wordPosition].text = "";
            wordPosition++;
            Debug.Log("OK");

            if(wordPosition == wordToCopy.Length)
            {
                successLogosList[currentSuccess].color = Color.green;
                audioS.PlayOneShot(validSound);

                AddSuccess();
                if (currentSuccess < numberOfSuccessToWin)
                {
                    InitNewWord();
                }
                else
                {
                    win = true;
                    GameWon();
                    Debug.Log("C'est Win");
                }
            }
        }
        else
        {
            // Augmenter jauge de sel

            audioS.PlayOneShot(errorSound);

            objectShaker.ShakeThis(timeHighlightWrong);
            
            if(!wrongAnswer)
                StartCoroutine(DisplayLetterCaseBright());

            GameManager.Instance.AddAnger(addedSalt);

            Debug.Log("NOPE");
        }

        inputF.text = "";
    }

    IEnumerator DisplayLetterCaseBright()
    {
        wrongAnswer = true;
        
        foreach (Transform child in GameObject.Find("WordToCopy").transform)
        {
            child.GetComponent<Image>().color = Color.red;
        }

        yield return new WaitForSeconds(timeHighlightWrong);

        foreach (Transform child in GameObject.Find("WordToCopy").transform)
        {
            child.GetComponent<Image>().color = Color.white;
        }

        wrongAnswer = false;
    }

    void GenerateRandomKeyboardSound()
    {
        int i = Random.Range(0, keyboardSounds.Length);

        audioS.PlayOneShot(keyboardSounds[i]);
    }

    // Fonction qui ajoute un au nombre de succès
    void AddSuccess()
    {
        currentSuccess++;
    }

    void GameWon()
    {
        // Do Something
        Destroy(gameObject);

        GameManager.Instance.AddAnger(-8);

        associatedTroll.OnMiniGameWin();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
