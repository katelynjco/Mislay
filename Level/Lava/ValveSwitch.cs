using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveSwitch : MonoBehaviour
{
    LavaRise lavaRise;

    [SerializeField] Sprite offSprite;

    [SerializeField] GameObject textGameObject;

    [SerializeField] GameObject fallingLava;

    SpriteRenderer textSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lavaRise = FindObjectOfType<LavaRise>();
        textSpriteRenderer = textGameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = offSprite;

            textSpriteRenderer.color = Color.red;

            lavaRise.lavaStart = false;

            fallingLava.SetActive(false);
        }  
    }
}
