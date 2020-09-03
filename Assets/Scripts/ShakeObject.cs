using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    Vector3 originalPos;

    private bool shaking;

    [SerializeField]
    private float shakeAmt;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (shaking)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * (Time.deltaTime * shakeAmt);
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }

    public void ShakeThis(float time)
    {
        StartCoroutine(Shake(time));
    }

    IEnumerator Shake(float time)
    {
        if (!shaking)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(time);

        shaking = false;
        transform.position = originalPos;
    }
}
