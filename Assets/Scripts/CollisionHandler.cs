using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashObstacle;
    [SerializeField] AudioClip finishSuccess;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource audioSource; 
    // ParticleSystem particleSys;

    // State
    bool isTransitioning = false;

    bool collisionDisabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();


    }


    void Update() 
    {
        BackDoorKeys();
        
    }

    void OnCollisionEnter(Collision other)

    {   
        
        if (isTransitioning || collisionDisabled) {return;}
        switch (other.gameObject.tag)
        {
        case "Friendly":
            Debug.Log("friendly");
            break;
    
        case "Start":
            Debug.Log("Start");
            break;
    
        case "Finish":
            StartSuccessSequence();
            // GetComponent<Movement>().enabled = false;
            // Invoke("LoadNextLevel", levelLoadDelay);
            break;

        default:
            StartCrashSequence();
            break;
        }
        
    }

    void StartCrashSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crashObstacle);

            crashParticles.Play();
            
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", levelLoadDelay);
        }

    void StartSuccessSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(finishSuccess);

            successParticles.Play();

            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", levelLoadDelay);
        }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }



    void BackDoorKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
           LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
        else
        {
           return;
        }
    }
    
}
