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

    public static List<PopUps> trolls = new List<PopUps>();

    private AudioSource audioS;
    [SerializeField] private AudioClip trollPopSound;
    [SerializeField] private AudioClip miniGameAppearing;
    [SerializeField] private AudioClip miniGameDisappearing;
    [SerializeField] private AudioClip miniGameCompleted;

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        audioS = GetComponentInParent<AudioSource>();

        gameObject.SetActive(false);

        trolls.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateTroll()
    {
        audioS.PlayOneShot(trollPopSound);
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
        audioS.PlayOneShot(miniGameAppearing);
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
        audioS.PlayOneShot(miniGameCompleted);
        StartCoroutine(PlayMiniGameDisappearingAfterSeconds(0.15f));
    }

    IEnumerator PlayMiniGameDisappearingAfterSeconds(float time)
    {
        Debug.Log("Courout");
        yield return new WaitForSeconds(time);
        audioS.PlayOneShot(miniGameDisappearing);

        gameObject.SetActive(false);

        activePopUps.Remove(this);

        trolls.Remove(this);

        ChangeAnger?.Invoke(activePopUps.Count);
    }
}
