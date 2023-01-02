using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnCollisionEnter(Collision other)
    {
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
                GetComponent<Movement>().enabled = false;
                Invoke("LoadNextLevel", levelLoadDelay);
                break;
            
            case "Fuel":
                Debug.Log("Fuel");
                break;

            default:
                StartCrashSequence();
                break;
        }

        

    }

    void StartCrashSequence()
        {
            // todo add SFX upon crash
            // todo add particle effect upon crash
            
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", levelLoadDelay);
        }


    void StartSuccessSequence()
        {
            // todo add SFX upon crash
            // todo add particle effect upon crash

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
    
}
