using UnityEngine;

public class FadeController : MonoBehaviour
{
    FadeInOut fade;
    
    void Start()
    {
        gameObject.SetActive(true);
        fade = FindObjectOfType<FadeInOut>();
        fade.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
