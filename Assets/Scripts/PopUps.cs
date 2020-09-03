using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{

    private float elapsedTime = 0;

    public static List<PopUps> activePopUps = new List<PopUps>();

    public static Action<int> ChangeAnger;

    public MiniGame miniGame = null;

    public MiniGame myMiniGame;

    public bool isActivated = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateTroll()
    {
        activePopUps.Add(this);
        ChangeAnger?.Invoke(activePopUps.Count);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void OnMouseEnter()
    {
        HighlightOn();
    }

    private void OnMouseExit()
    {
        HighlightOff();
    }

    private void OnMouseOver()
    {
        if (!GameManager.Instance.isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {   
                ActivateMiniGame();
            }
        }
        
    }

    private void HighlightOn()
    {
        transform.localScale *= 1.2f;

        GetComponent<Renderer>().material.color = Color.red;
    }

    private void HighlightOff()
    {
        transform.localScale = Vector3.one;

        GetComponent<Renderer>().material.color = Color.white;
    }

    private void ActivateMiniGame()
    {
        myMiniGame = Instantiate(miniGame, Vector3.zero, Quaternion.identity);
        GameManager.Instance.isPlaying = true;

        myMiniGame.associatedTroll = this;
    }

    public void OnMiniGameWin()
    {
        RemoveTroll();

        GameManager.Instance.isPlaying = false;
    }

    public void RemoveTroll()
    {
        gameObject.SetActive(false);

        activePopUps.Remove(this);

        Debug.Log(activePopUps.Count);

        ChangeAnger?.Invoke(activePopUps.Count);
    }
}
