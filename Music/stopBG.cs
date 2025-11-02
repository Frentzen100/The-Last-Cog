using UnityEngine;

public class stopBG : MonoBehaviour
{

    private AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
        audioManager.StopSFX(audioManager.background);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
