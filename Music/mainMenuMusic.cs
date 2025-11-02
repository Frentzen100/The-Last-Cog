using UnityEngine;
using UnityEngine.UI;

public class mainMenuMusic : MonoBehaviour
{
    public AudioSource musicSource; // Assign this in the Inspector
    public Button MenuButton; // Assign the button in the Inspector

    void Start()
    {
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>(); // Find AudioSource if not assigned

        MenuButton.onClick.AddListener(StopMusic); // Add button click event
    }

    void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            Debug.Log("Music Stopped");
        }
    }
}