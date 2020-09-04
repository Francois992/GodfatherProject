using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Canvas StoryScreen = null;

    // Start is called before the first frame update
    void Start()
    {
        StoryScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            StoryScreen.gameObject.SetActive(true);
        }
    }
}
