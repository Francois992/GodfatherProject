using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        PopUps.ChangeAnger += changeAddedValue;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public float anger = 0;
    public float addedAnger = 0;
    private float angerMax = 100;

    private float timer = 0;

    [Serializable] public struct Spawn
    {
        public PopUps pop;
        public float spawnTime;
    }
    [SerializeField] public Spawn[] spawns;

    [SerializeField] public Canvas LooseScreen = null;
    [SerializeField] public Canvas WinScreen = null;

    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        LooseScreen.gameObject.SetActive(false);
        WinScreen.gameObject.SetActive(false);
        for (int i = 0; i < spawns.Length; i++)
        {
            
            spawns[i].pop.gameObject.SetActive(false);
        }

        StartCoroutine(UpdateAnger());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        for(int i  = 0; i < spawns.Length; i++)
        {
            if(spawns[i].spawnTime <= timer)
            {
                if (!spawns[i].pop.isActivated)
                {
                    spawns[i].pop.isActivated = true;
                    spawns[i].pop.ActivateTroll();                
                    spawns[i].pop.gameObject.SetActive(true);
                }
            }
        }

        if(anger >= angerMax)
        {
            GameOver();
        }
    }

    private IEnumerator UpdateAnger()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AddAnger(addedAnger);
        }
    }

    public void AddAnger(float addedValue)
    {
        anger += addedValue;

        HUD.Instance.AngerBar.fillAmount = anger/ 100;
    }

    private void changeAddedValue(int value)
    {
        if (value == 0) addedAnger = 0;
        else if (value == 1) addedAnger = 1;
        else if (value == 2) addedAnger = 1.5f;
        else if (value == 3) addedAnger = 1.75f;
        else if (value == 4) addedAnger = 1.9f;
        else addedAnger = 2f;
    }

    private void GameOver()
    {
        for(int i = 0; i < spawns.Length; i++)
        {
            if (spawns[i].pop.myMiniGame != null)
            {
                spawns[i].pop.myMiniGame.gameObject.SetActive(false);
            }
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        StopCoroutine(UpdateAnger());

        LooseScreen.gameObject.SetActive(true);
    }

    private void GameWin()
    {
        StopCoroutine(UpdateAnger());

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        WinScreen.gameObject.SetActive(true);
    }

    public void Restart()
    {
        anger = 0;
        addedAnger = 0;

        timer = 0;
        HUD.Instance.AngerBar.fillAmount = 0;

        LooseScreen.gameObject.SetActive(false);
        WinScreen.gameObject.SetActive(false);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
