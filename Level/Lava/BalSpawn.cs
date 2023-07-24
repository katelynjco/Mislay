using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalSpawn : MonoBehaviour
{
    [SerializeField] GameObject lavaDrop;
    [SerializeField] Transform dropSpawn;
    [SerializeField] float lavaDelay;

    // Start is called before the first frame update
    void Start()
    {
        float lavaDelay = Random.Range(1f, 5f);
        Invoke("OnRain", lavaDelay);
    }

    void OnRain()
    {
        Instantiate(lavaDrop, dropSpawn.position, transform.rotation);

        Invoke("OnRain", lavaDelay);
    }
}
