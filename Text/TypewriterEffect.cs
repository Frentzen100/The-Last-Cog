using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    [SerializeField] public string nextSceneName; // Set this in Unity Inspector
    public GameObject additionalText;
    private TextMeshProUGUI textComponent;
    private string fullText;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private FadeInOut fade; // Reference to FadeInOut script

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        fullText = textComponent.text;
        textComponent.text = "";
        additionalText.SetActive(false);
        fade = FindObjectOfType<FadeInOut>(); // Find FadeInOut in the scene

        typingCoroutine = StartCoroutine(ShowText());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping) 
            {
                // Skip typing effect
                StopCoroutine(typingCoroutine);
                textComponent.text = fullText;
                isTyping = false;
                additionalText.SetActive(true);
            }
            else
            {
                // If text is fully displayed, go to next scene with fade
                StartCoroutine(TransitionToNextScene());
            }
        }
    }

    IEnumerator ShowText()
    {
        isTyping = true;
        textComponent.text = "";

        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
        yield return new WaitForSeconds(0.5f);
        additionalText.SetActive(true);
    }

    IEnumerator TransitionToNextScene()
    {
        if (fade != null)
        {
            fade.FadeIn(); // Start fade-in effect
            yield return new WaitForSeconds(fade.fadeDuration); // Wait for fade to complete
        }

        SceneManager.LoadScene(nextSceneName.Trim()); // Load next scene after fade
    }
}



// using TMPro;
// using UnityEngine;
// using System.Collections; 
// using UnityEngine.SceneManagement; 

// public class TypewriterEffect : MonoBehaviour
// {
//     public float delay = 0.1f;
//     [SerializeField] public string nextSceneName; // Set this in Unity Inspector
//     public GameObject additionalText;
//     private TextMeshProUGUI textComponent;
//     private string fullText;
//     private bool isTyping = false;
//     private Coroutine typingCoroutine;

//     void Start()
//     {
//         textComponent = GetComponent<TextMeshProUGUI>();
//         fullText = textComponent.text;
//         textComponent.text = "";
//         additionalText.SetActive(false);
//         typingCoroutine = StartCoroutine(ShowText());
//     }

//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             if (isTyping) 
//             {
//                 // Skip typing effect
//                 StopCoroutine(typingCoroutine);
//                 textComponent.text = fullText;
//                 isTyping = false;
//                 additionalText.SetActive(true);
//             }
//             else if (!isTyping) {
//                 StartCoroutine(TransitionToNextScene()); // Load next scene after skipping
//             }
//             else
//             {
//                 // If text is fully displayed, go to next scene
//                 StartCoroutine(TransitionToNextScene());
//             }
//         }
//     }

//     IEnumerator ShowText()
//     {
//         isTyping = true;
//         textComponent.text = "";

//         foreach (char c in fullText)
//         {
//             textComponent.text += c;
//             yield return new WaitForSeconds(delay);
//         }

//         isTyping = false;
//         yield return new WaitForSeconds(0.5f);
//         additionalText.SetActive(true);
//         StartCoroutine(TransitionToNextScene()); // Load scene after delay
//     }

//     IEnumerator TransitionToNextScene()
//     {
//         yield return new WaitForSeconds(1f); // Short delay before transitioning
//         SceneManager.LoadScene(nextSceneName.Trim()); // Load the next scene
//     }
// }
