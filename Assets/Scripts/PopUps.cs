using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public float duration = 10f;

    private float elapsedTime = 0;

    public static List<PopUps> activePopUps = new List<PopUps>();

    public static Action<int> ChangeAnger;

    public Canvas miniGame = null;

    // Start is called before the first frame update
    void Start()
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
        if (Input.GetMouseButtonDown(0))
        {
            for(int i = GameManager.Instance.spawns.Length - 1; i >= 0; i--)
            {
                if (GameManager.Instance.spawns[i].pop = this) gameObject.SetActive(false);

                activePopUps.Remove(this);

                ChangeAnger?.Invoke(activePopUps.Count);
            }

            ActivateMiniGame();
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
        Debug.Log("Mini jeu !");
    }
}
