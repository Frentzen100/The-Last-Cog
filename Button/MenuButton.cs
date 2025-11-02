using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MenuButton : MonoBehaviour
{

    private AudioManager audioManager;
    public Button myButton; // Assign this in the Inspector

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        SceneManager.LoadScene("Exposition1");
    }




}
