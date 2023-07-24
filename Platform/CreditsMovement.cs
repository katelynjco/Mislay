using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMovement : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] float speed = 5f;  // Variable speed
    [SerializeField] float duration = 3f;  // Variable duration

    [SerializeField] bool isMovingUp = true;
    [SerializeField] float timer = 0f;

    [SerializeField] ParticleSystem mechRocket;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.CreditsScreenCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

         if (timer >= (duration * 1.5f))
        {
            LoadNextLevel();
        }
    }

    void Move()
    {
        timer += Time.deltaTime;

        if (isMovingUp)
        {
            mechRocket.Play();
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (timer >= (duration - 2))
            {
                mechRocket.Stop();
            }
            if (timer >= duration)
            {
                isMovingUp = false;
            }
        }
        else
        {
            // Fall down
            transform.Translate(Vector3.down * (speed * 5) * Time.deltaTime);
        }
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        // FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }

}
