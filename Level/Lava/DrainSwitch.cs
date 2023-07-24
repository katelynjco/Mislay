using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSwitch : MonoBehaviour
{
    LavaRise lavaRise;
    AudioManager audioManager;

    [SerializeField] Sprite onSprite;

    [SerializeField] GameObject textGameObject;

    SpriteRenderer textSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lavaRise = FindObjectOfType<LavaRise>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        textSpriteRenderer = textGameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = onSprite;

            textSpriteRenderer.color = Color.green;

            audioManager.SmelterLevelCheck();

            lavaRise.lavaStart = false;
            lavaRise.lavaDrain = true;
        }  
    }
}
