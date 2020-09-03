using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame3 : MonoBehaviour
{
    public string[] Orders;
    private string winOrder;

    
    [SerializeField] private InputField inputPlayer;
            

    // Start is called before the first frame update
    void Start()
    {
        var randomInt = Random.Range(0, Orders.Length);
        Debug.Log(randomInt);
        winOrder = Orders[randomInt];
        Debug.Log(winOrder);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            var inputPlayerText = inputPlayer.text;

            if(inputPlayerText.Length == 4)
            {
                
                if (winOrder == inputPlayerText)
                {
                    Debug.Log("Win");
                }
                else
                {
                    Debug.Log("No");
                }
            }

            //Debug.Log(inputPlayer.text);
        }
    }

}
