using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;

    private void Awake()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    public void Play()
    {
        // Load the first level (index 1, since 0 is the main menu)
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)
#endif
    }
}
