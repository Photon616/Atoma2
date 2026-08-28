using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardControlManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction placeAction;
    private InputAction bondAction;
    private InputAction deleteAction;
    private InputAction cutAction;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (placeAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.currentTool = ToolStateManager.Tool.place;
        }
        if (bondAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.currentTool = ToolStateManager.Tool.bond;
        }
        if (deleteAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.currentTool = ToolStateManager.Tool.delete;
        }
        if (cutAction.WasPressedThisFrame())
        {
            ToolStateManager.Instance.currentTool = ToolStateManager.Tool.cut;
        }
    }
}
