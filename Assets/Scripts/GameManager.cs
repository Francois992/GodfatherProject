using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
    [SerializeField] private float gameDuration = 180;

    [Serializable] public struct Spawn
    {
        public PopUps pop;
        public float spawnTime;
    }
    [SerializeField] public Spawn[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        PopUps.ChangeAnger += changeAddedValue;

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
                spawns[i].pop.gameObject.SetActive(true);
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
            AddAnger();
        }
    }

    private void AddAnger()
    {
        anger += addedAnger;

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
        StopCoroutine(UpdateAnger());

        Debug.Log("You Lose !");
    }
}
