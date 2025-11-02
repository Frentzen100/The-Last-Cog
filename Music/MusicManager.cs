using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // Singleton pattern
    private AudioSource audioSource;
    public string stopMusicSceneName = "Level1"; // Scene where music should stop

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate music managers
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == stopMusicSceneName)
        {
            Destroy(gameObject); // Stop music by destroying this object
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent memory leaks
    }
}
