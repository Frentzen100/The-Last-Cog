using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    private bool isNearPlayer = false; // Track if player is near
    [SerializeField] private string sceneToLoad; // Scene name to load
    private AudioManager audioManager;
    private FadeInOut fade;

    private void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    private void Awake()
    {
        // Find the AudioManager using the "Audio" tag
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found! Ensure an object with the 'Audio' tag exists.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true; // Player is close enough to interact
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
        // If the player is near and presses "W" or "Up Arrow", trigger scene transition
        if (isNearPlayer && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            StartCoroutine(_ChangeScene());
        }
    }

    private IEnumerator _ChangeScene()
    {
        // Play door open sound if available
        if (audioManager != null && audioManager.openDoor != null)
        {
            audioManager.PlaySFX(audioManager.openDoor);
        }

        yield return new WaitForSeconds(0.5f); // Wait a bit for the sound

        fade.FadeIn(); // Start fade in effect
        yield return new WaitForSeconds(1); // Wait for fade effect
        SceneManager.LoadScene(sceneToLoad); // Load the new scene
    }
}



// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections;

// public class ChangeScene : MonoBehaviour
// {
//     private bool isNearPlayer = false; // Track if player is near
//     [SerializeField] private string sceneToLoad; // Scene name to load
//     private AudioManager audioManager;
//     FadeInOut fade;

//     private void Start()
//     {
//         fade = FindObjectOfType<FadeInOut>();
//     }

//     private void Awake()
//     {
//         // Find the AudioManager using the "Audio" tag
//         audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
//         //fade = FindObjectOfType<FadeInOut>();

//         // Null check for AudioManager
//         if (audioManager == null)
//         {
//             Debug.LogWarning("AudioManager not found! Ensure an object with the 'Audio' tag exists.");
//         }
//     }

//     public IEnumerator _ChangeScene() {
//         fade.FadeIn();
//         yield return new WaitForSeconds(2);
//         SceneManager.LoadScene(sceneToLoad);
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Player"))
//         {
//            // isNearPlayer = true; // Player is close enough to interact
//            StartCoroutine(_ChangeScene());
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Player"))
//         {
//             isNearPlayer = false; // Player moved away
//         }
//     }

//     private void Update()
//     {
//         // If near the trigger and "W" is pressed, initiate scene transition
//         if (isNearPlayer && (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow))))
//         {
//             TransitionScene();
//         }
//     }

//     private void TransitionScene()
//     {
//         if (!string.IsNullOrEmpty(sceneToLoad))
//         {
//             // Play door opening sound if AudioManager and sound clip exist
//             if (audioManager != null && audioManager.openDoor != null)
//             {
//                 audioManager.StopSFX(audioManager.walking);
//                 SceneManager.LoadScene(sceneToLoad);
//                 audioManager.PlaySFX(audioManager.openDoor);
//                 SceneManager.LoadScene(sceneToLoad);
//             }
//             else
//             {
//                 Debug.LogWarning("AudioManager or openDoor clip is missing.");
//             }

//             // Delay for sound to play before switching scenes
//             //StartCoroutine(_ChangeScene());
//         }
//         else
//         {
//             Debug.LogWarning("Scene name is not set.");
//         }
//     }

// }
