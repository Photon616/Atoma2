using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance{ get; private set; }
    public static bool isPaused = false;

    [SerializeField] GameObject backgroundObject;
    [SerializeField] GameObject menuObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        menuObject.SetActive(false);
        backgroundObject.SetActive(false);
    }

    public void Toggle()
    {
        if (isPaused)
        {
            HidePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    public void ShowPauseMenu()
    {
        backgroundObject.SetActive(true);
        menuObject.SetActive(true);
        isPaused = true;
    }

    public void HidePauseMenu()
    {
        menuObject.SetActive(false);
        backgroundObject.SetActive(false);
        isPaused = false;
    }

    public void Resume()
    {
        HidePauseMenu();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
