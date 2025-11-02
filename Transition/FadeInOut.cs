using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.3f; // Set fade duration
    private float targetAlpha;
    private bool isFading = false;
    private Coroutine fadeRoutine;

   void Update()
    {
        if (isFading)
        {
            Debug.Log("⏳ Fading... Alpha: " + canvasGroup.alpha + " → Target: " + targetAlpha);
            
            float fadeSpeed = Time.deltaTime / fadeDuration;
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed);

            if (Mathf.Approximately(canvasGroup.alpha, targetAlpha))
            {
                Debug.Log("✅ Fade Completed! Final Alpha: " + canvasGroup.alpha);
                canvasGroup.alpha = targetAlpha;
                isFading = false;
            }
        }
    }


    public void FadeIn()
    {
        targetAlpha = 1;
        isFading = true;
    }

    public void FadeOut()
    {
        Debug.Log("🚨 FadeOut() triggered! Current alpha: " + canvasGroup.alpha);
        targetAlpha = 0;
        isFading = true;
    }

    public void StartFadeSequence(float delayBetweenFades)
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeSequence(delayBetweenFades));
    }

   private IEnumerator FadeSequence(float delay)
{
    FadeIn(); // Start FadeIn
    Debug.Log("🌓 FadeIn started...");

    yield return new WaitUntil(() => !isFading); // Wait until FadeIn completes
    Debug.Log("✅ FadeIn completed!");

    yield return new WaitForSeconds(delay); // Wait before FadeOut

    Debug.Log("🔥 Starting FadeOut...");
    FadeOut(); // Start FadeOut
}
}




//  using UnityEngine;
//  using UnityEngine.UI;

// public class FadeInOut : MonoBehaviour
// {

//     public CanvasGroup canvasgroup;
//     public bool fadeIn = false;
//     public bool fadeOut = false;

//     public float TimeToFade;
//    // public Image fadeTemplate;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//       //  fadeTemplate = FindObjectOfType<Image>();
//       //  fadeTemplate.gameObject.SetActive(true);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (fadeIn == true) 
//         {
//             if(canvasgroup.alpha < 1) 
//             {
//                 canvasgroup.alpha += TimeToFade * Time.deltaTime;
//                 if(canvasgroup.alpha >= 1) 
//                 {
//                     fadeIn = false;
//                 }
//             }
//         }

//         if (fadeOut == true) 
//         {
//             if(canvasgroup.alpha >= 0) 
//             {
//                 canvasgroup.alpha -= TimeToFade * Time.deltaTime;
//                 if(canvasgroup.alpha == 0) 
//                 {
//                     fadeOut = false;
//                 }
//             }
//         }
        
//     }

//     public void FadeIn() {
//         fadeIn = true;
//     }

//     public void FadeOut() {
//         fadeOut = true;
//     }
// }
