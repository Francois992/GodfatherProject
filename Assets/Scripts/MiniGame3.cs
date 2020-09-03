using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame3 : MiniGame
{
    private string winOrder;
    private string numberToOrganize;

    [SerializeField]
    private List<int> allNumbersArray = new List<int>();
    [SerializeField]
    private List<int> winOrders = new List<int>();
    [SerializeField] 
    private InputField inputPlayer;
    [SerializeField] 
    private GameObject LetterPrefab;

    [SerializeField]
    private ShakeObject objectShaker;
    [SerializeField]
    private float timeHighlightWrong = 1;

    // Start is called before the first frame update
    void Start()
    {
        RandomOrder();
        AutoInputSelector();
        InstantiateNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            InstantiateNumberPlayer();

            if (inputPlayer.text.Length == 4)
            {
                
                if (winOrder == inputPlayer.text)
                {
                    Debug.Log("WIN!!");
                    GameWin(); 
                }
                else
                {
                    //essai raté ! 
                    objectShaker.ShakeThis(timeHighlightWrong);
                    EmptyPreviousNumber();
                    inputPlayer.text = "";
                    Debug.Log("No");
                }
            }

            if (inputPlayer.text.Length > 4)
            {
                EmptyPreviousNumber();
                inputPlayer.text = "";
            }

            Debug.Log(inputPlayer.text);
        }
        
        // Au cas où le clique fasse perdre le focus, on le récupère
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            AutoInputSelector();
        }

    }

    // Fonction qui instancie les prefabs de lettre dans l'UI
    void InstantiateNumber()
    {
        for (int i = 0; i < numberToOrganize.Length; i++)
        {
            GameObject tmp = Instantiate(LetterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            tmp.transform.SetParent(GameObject.Find("NumbersToOrganize").transform, false);
            tmp.GetComponentInChildren<Text>().text = numberToOrganize[i].ToString();
        }
    }

    // Fonction qui instancie les prefabs de lettre du joueur dans l'UI
    void InstantiateNumberPlayer()
    {
        GameObject tmp = Instantiate(LetterPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        tmp.transform.SetParent(GameObject.Find("NumbersPlayer").transform, false);
        tmp.GetComponentInChildren<Text>().text = inputPlayer.text[inputPlayer.text.Length - 1].ToString();

    }

    // Fonction qui détruit les prefabs de lettre dans la hiérarchie 
    // Appelée quand un mot est initialisé
    void EmptyPreviousNumber()
    {
        foreach (Transform child in GameObject.Find("NumbersPlayer").transform)
        {
            Destroy(child.gameObject);
        }
    }

    //Choisi un ordre aleatoire dans la list "Orders"
    private void RandomOrder()
    {
        var r = Random.Range(0, allNumbersArray.Count);
        numberToOrganize = allNumbersArray[r].ToString();
        winOrder = winOrders[r].ToString();
        Debug.Log(numberToOrganize);
        Debug.Log(winOrder);
    }


    //permet de select automatiquement l'inputField
    private void AutoInputSelector()
    {
            inputPlayer.Select();
            inputPlayer.ActivateInputField();
    }


    private void GameWin()
    {
        Destroy(gameObject);

        associatedTroll.OnMiniGameWin();
    }

}
