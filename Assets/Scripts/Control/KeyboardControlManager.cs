using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardControlManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction placeAction;
    private InputAction bondAction;
    private InputAction deleteAction;
    private InputAction cutAction;
    private InputAction escapeAction;
    private InputAction pauseAction;
    // private bool wasPaused = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        placeAction = playerInput.actions["Place"];
        bondAction = playerInput.actions["Bond"];
        deleteAction = playerInput.actions["Delete"];
        cutAction = playerInput.actions["Cut"];
        escapeAction = playerInput.actions["Escape"];
        pauseAction = playerInput.actions["Pause"];
    }

    // Update is called once per frame
    void Update()
    {
        if (placeAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.SetCurrentTool(ToolStateManager.Tool.place);
        }
        if (bondAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.SetCurrentTool(ToolStateManager.Tool.bond);
        }
        if (deleteAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.SetCurrentTool(ToolStateManager.Tool.delete);
        }
        if (cutAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.SetCurrentTool(ToolStateManager.Tool.cut);
        }
        if (escapeAction.WasPressedThisFrame())
        {
            PauseMenu.Instance.Toggle();
            // if (PauseMenu.isPausedWithESC)
            // {
            //     wasPaused = PhysicsManager.Instance.isPaused;
            //     PhysicsManager.Instance.PauseSimulation();
            // }
            // else
            // {
            //     if (wasPaused)
            //     {
            //         PhysicsManager.Instance.PauseSimulation();
            //     }
            //     else
            //     {
            //         PhysicsManager.Instance.ResumeSimulation();
            //     }
            // }
        }
        if (pauseAction.WasPressedThisFrame())
        {
            PhysicsManager.Instance.ToggleSimulationSpeed();
        }
    }
}
