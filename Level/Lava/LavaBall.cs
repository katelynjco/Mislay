using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float duration = 1f;

    [SerializeField] bool isMovingUp = true;
    [SerializeField] float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        timer += Time.deltaTime;

        if (isMovingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    
            if (timer >= duration)
            {
                isMovingUp = false;
            }
        }
        else
        {
            // Fall down
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (timer >= duration * 2f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
            else if (other.CompareTag("Platforms") || other.CompareTag("Hazards"))
        {
            Destroy(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
