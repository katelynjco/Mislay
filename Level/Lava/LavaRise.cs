using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRise : MonoBehaviour
{
    [SerializeField] float scrollRate = 0.5f;
    [SerializeField] float lavaDuration = 85f;
    public bool lavaStart = false;
    public bool lavaDrain = false;

    // Update is called once per frame
    void Update()
    {
        if(lavaStart)
        {
            StartCoroutine(StopLavaRise(lavaDuration));
            transform.Translate(new Vector2(0f, scrollRate * Time.deltaTime));
        }

        if(lavaDrain)
        {
            StartCoroutine(StopLavaDrain((lavaDuration / 5)));
            transform.Translate(new Vector2(0f, -(scrollRate * 5) * Time.deltaTime));
        }
    }

    IEnumerator StopLavaRise(float duration)
    {
        yield return new WaitForSeconds(duration);
        lavaStart = false;
    }

    IEnumerator StopLavaDrain(float duration)
    {
        yield return new WaitForSeconds(duration);
        lavaDrain = false;
    }


}

