using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneWithKey : MonoBehaviour
{
    [SerializeField] public string sceneToLoadA; // Scene name to load for key A
    [SerializeField] public string sceneToLoadD; // Scene name to load for key D

    private FadeInOut fade; // Reference to the FadeInOut script
    private int timer = 0;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
        Application.targetFrameRate = 60;
        fade = FindObjectOfType<FadeInOut>(); // Find the FadeInOut script in the scene
    }

    private void Update()
    {
        timer++;

        if (Input.GetKeyDown(KeyCode.A) && timer > 0) // Press A to transition scene
        {
            StartCoroutine(TransitionScene(sceneToLoadA));
            timer = 0;
        }

        if (Input.GetKeyDown(KeyCode.D) && timer > 0) // Press D to transition scene
        {
            StartCoroutine(TransitionScene(sceneToLoadD));
            timer = 0;
        }
    }

    private IEnumerator TransitionScene(string sceneToLoad)
    {
        audioManager.StopSFX(audioManager.background);
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Starting scene transition with fade...");

            if (fade != null)
            {
                fade.FadeIn(); // Start fade-in effect
                yield return new WaitForSeconds(fade.fadeDuration); // Wait exactly for fade duration
            }

            SceneManager.LoadScene(sceneToLoad.Trim()); // Load new scene after fade
        }
        else
        {
            Debug.LogWarning("Scene name is not set.");
        }
    }
}

