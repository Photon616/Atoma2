using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance{ get; private set; }
    public static bool isPaused = false;

    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private ConfirmationUI confirmationUI;
    [SerializeField] private SaveGame saveGame;
    [SerializeField] private LoadGame loadGame;
    private bool wasPaused = false;

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
        wasPaused = PhysicsManager.Instance.isPaused;
        isPaused = true;
    }

    public void HidePauseMenu()
    {
        menuObject.SetActive(false);
        backgroundObject.SetActive(false);
        isPaused = wasPaused;
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

    public void AskSave()
    {
        confirmationUI.Show("Save Simulation?", "Current Save\nwill be overwritten.", Save);
    }

    private void Save()
    {
        saveGame.Save();
    }
    
    public void AskLoad()
    {
        confirmationUI.Show("Load Save?", "Current simulation\nwill be lost.", Load);
    }

    private void Load()
    {
        loadGame.Load();
    }
}
