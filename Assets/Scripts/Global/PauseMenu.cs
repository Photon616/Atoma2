using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance{ get; private set; }
    public static bool isPaused = false;

    [SerializeField] GameObject pauseMenuObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pauseMenuObject.SetActive(false);
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
        pauseMenuObject.SetActive(true);
        isPaused = true;
    }

    public void HidePauseMenu()
    {
        pauseMenuObject.SetActive(false);
        isPaused = false;
    }
}
