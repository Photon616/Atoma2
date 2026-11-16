using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance{ get; private set; }
    public static bool isPaused = false;

    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject[] inGameUIs = new GameObject[10];
    [SerializeField] private ConfirmationUI confirmationUI;
    [SerializeField] private SaveGame saveGame;
    [SerializeField] private LoadGame loadGame;
    private PhysicsManager physicsManager;
    private bool wasPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        physicsManager = PhysicsManager.Instance;
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

        foreach (GameObject obj in inGameUIs)
        {
            obj.SetActive(false);
        }

        wasPaused = PhysicsManager.Instance.isPaused;
        isPaused = true;
    }

    public void HidePauseMenu()
    {
        menuObject.SetActive(false);
        backgroundObject.SetActive(false);

        foreach (GameObject obj in inGameUIs)
        {
            obj.SetActive(true);
        }

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
        confirmationUI.Show("Save Simulation?", "Current Save\nwill be overwritten.", Save, Cancel);
        menuObject.SetActive(false);
    }

    private void Save()
    {
        saveGame.Save();
    }
    
    public void AskLoad()
    {
        confirmationUI.Show("Load Save?", "Current simulation\nwill be lost.", Load, Cancel);
        menuObject.SetActive(false);
    }

    private void Load()
    {
        loadGame.Load();
    }

    public void AskReset()
    {
        confirmationUI.Show("Reset Simulation?", "Current simulation\nwill be lost.", Reset, Cancel);
        menuObject.SetActive(false);
    }

    private void Reset()
    {
        physicsManager.ClearSimulation();
    }

    public void ShowMenu()
    {
        menuObject.SetActive(true);
    }

    public void Cancel()
    {
        ShowMenu();
    }
}
