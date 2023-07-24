using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMineCart : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] Transform[] endPoints;

    MenuManager menuManager;

    int currentPointIndex;
    bool once = false;

    void Start() 
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() 
    {
        if(transform.position != endPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPoints[currentPointIndex].position, moveSpeed *Time.deltaTime);
        } 
        else
        {
            if(once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if(currentPointIndex + 1 < endPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) 
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Cart Despawn"))
        {
            Destroy(gameObject);
        }
          
    }
}
