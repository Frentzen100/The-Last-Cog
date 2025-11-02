
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene1 : MonoBehaviour
{
    private bool isNearPlayer = false; // Track if player is near
    [SerializeField] private string sceneToLoad; // Scene name to load
    private AudioManager audioManager;
    FadeInOut fade;

    private void Awake()
    {
        // Find the AudioManager using the "Audio" tag
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
        fade = FindObjectOfType<FadeInOut>();

        // Null check for AudioManager
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found! Ensure an object with the 'Audio' tag exists.");
        }
    }

    public IEnumerator _ChangeScene() {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true; // Player is close enough to interact
            TransitionScene();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false; // Player moved away
        }
    }

    private void Update()
    {
        // If near the trigger and "W" is pressed, initiate scene transition
        if (isNearPlayer)
        {
            TransitionScene();
        }
    }

    private void TransitionScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Play door opening sound if AudioManager and sound clip exist
            if (audioManager != null && audioManager.openDoor != null)
            {
                audioManager.StopSFX(audioManager.walking);
                SceneManager.LoadScene(sceneToLoad);
                audioManager.PlaySFX(audioManager.openDoor);
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("AudioManager or openDoor clip is missing.");
            }

            // Delay for sound to play before switching scenes
            //StartCoroutine(_ChangeScene());
        }
        else
        {
            Debug.LogWarning("Scene name is not set.");
        }
    }

}
