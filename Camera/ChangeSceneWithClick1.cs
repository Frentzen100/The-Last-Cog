using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneWithClick1 : MonoBehaviour
{
    [SerializeField] public string sceneToLoad; // Scene name to load
    [SerializeField] private int timerToChangeToNextScene;
    [SerializeField] private int timerToChangeToNextSceneByClick;
    private int timer = 0;

    private FadeInOut fade; // Reference to FadeInOut

    void Start()
    {
        Application.targetFrameRate = 60;
        fade = FindObjectOfType<FadeInOut>(); // Find the FadeInOut script in the scene
    }

    private void Update()
    {
        timer++;

        if (timer > timerToChangeToNextScene)
        {
            StartCoroutine(TransitionScene()); // Start fading before scene change
            timer = 0;
        }

        if (Input.GetMouseButtonDown(0) && timer > timerToChangeToNextSceneByClick)
        {
            StartCoroutine(TransitionScene()); // Fade on click-based transition
            timer = 0;
        }
    }

    private IEnumerator TransitionScene()
    {
        Debug.Log("Attempting to load scene: " + sceneToLoad);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            if (fade != null)
            {
                fade.FadeIn(); // Start fade-in effect
                yield return new WaitForSeconds(fade.fadeDuration); // Wait for fade to complete
            }

            SceneManager.LoadScene(sceneToLoad); // Load new scene after fade effect
        }
        else
        {
            Debug.LogWarning("Scene name is not set.");
        }
    }
}




// using UnityEngine;
// using UnityEngine.SceneManagement; 

// public class ChangeSceneWithClick1 : MonoBehaviour
// {
//     [SerializeField] public string sceneToLoad; // Scene name to load
//     [SerializeField] private int timerToChangeToNextScene;
//     [SerializeField] private int timerToChangeToNextSceneByClick;
//     private int timer = 0;
//     void Start()
//     {
//         Application.targetFrameRate = 60; 
//     }

//     private void Update()
//     {
//         Debug.Log(sceneToLoad);
//         timer++;
//         //Debug.Log(timer + "  " + timerToChangeToNextScene);
        
//         if (timer > timerToChangeToNextScene)
//         {
//             TransitionScene();
//             timer = 0;
//         }

//         //  if (Input.GetMouseButtonDown(0) && timer > timerToChangeToNextSceneByClick)
//         // {
//         //     TransitionScene();
//         //     timer = 0;
//         // }
//     }

//      private void TransitionScene()
//     {
//         Debug.Log("Attempting to load scene: " + sceneToLoad);  
//         if (!string.IsNullOrEmpty(sceneToLoad))
//         {
//             SceneManager.LoadScene(sceneToLoad);
//         }
//         else
//         {
//             Debug.LogWarning("Scene name is not set.");
//         }
//     }
// }