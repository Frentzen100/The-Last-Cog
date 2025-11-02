using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneWithClick : MonoBehaviour
{
    [SerializeField] public string sceneToLoad; // Scene name to load
    [SerializeField] private int timerToChangeToNextScene;
    private int timer = 0;

    private FadeInOut fade; // Reference to FadeInOut script

    void Start()
    {
        Application.targetFrameRate = 60; 
        fade = FindObjectOfType<FadeInOut>(); // Find the FadeInOut script in the scene
    }

    private void Update()
    {
        timer++;

        // Click-based transition with fade
        if (Input.GetMouseButtonDown(0) && timer > timerToChangeToNextScene)
        {
            StartCoroutine(TransitionScene()); // Use coroutine to handle fade
            timer = 0;
        }

        // Timer-based transition with fade
        if (timer + 100 > timerToChangeToNextScene)
        {
            StartCoroutine(TransitionScene()); // Use coroutine to handle fade
            timer = 0;
        }
    }

    private IEnumerator TransitionScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Starting scene transition with fade...");

            if (fade != null)
            {
                fade.FadeIn(); // Start fade-in effect
                yield return new WaitForSeconds(fade.fadeDuration); // Wait for fade to complete
            }

            SceneManager.LoadScene(sceneToLoad.Trim()); // Load new scene after fade
        }
        else
        {
            Debug.LogWarning("Scene name is not set.");
        }
    }
}


// using UnityEngine;
// using UnityEngine.SceneManagement; 

// public class ChangeSceneWithClick : MonoBehaviour
// {
//     [SerializeField] public string sceneToLoad; // Scene name to load
//     [SerializeField] private int timerToChangeToNextScene;
//     private int timer = 0;
//     void Start()
//     {
//         Application.targetFrameRate = 60; 
//     }

//     private void Update()
//     {
//         timer++;
//         //Debug.Log(timer + "  " + timerToChangeToNextScene);
        
//         if (Input.GetMouseButtonDown(0) && timer > timerToChangeToNextScene)
//         {
//             SceneManager.LoadScene(sceneToLoad.Trim());
//             timer = 0;
//         }

//         if (timer + 100 > timerToChangeToNextScene) {
//             SceneManager.LoadScene(sceneToLoad.Trim());
//             timer = 0;
//         } 
//     }

//      private void TransitionScene()
//     {
//         if (!string.IsNullOrEmpty(sceneToLoad))
//         {
//             SceneManager.LoadScene(sceneToLoad.Trim());
//         }
//         else
//         {
//             Debug.LogWarning("Scene name is not set.");
//         }
//     }
// }