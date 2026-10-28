using UnityEngine;
using UnityEngine.UI;

public class ToolStateManager : MonoBehaviour
{
    public static ToolStateManager Instance{ get; private set; }

    public enum Tool
    {
        select,
        place,
        bond,
        delete,
        cut
    }

    [SerializeField] Button select;
    [SerializeField] Button place;
    [SerializeField] Button bond;
    [SerializeField] Button delete;
    [SerializeField] Button cut;
    [SerializeField] GameObject elementSelectionUI;
    private Tool currentTool = Tool.select;

    void Awake()
    {
        Instance = this;
        SetCurrentTool(Tool.select);
    }

    public void SetCurrentTool(Tool tool)
    {
        Debug.Log($"Switching tool to {tool}");
        currentTool = tool;
        SetButtons();
    }

    public void SetCurrentToolWithNum(int tool)
    {
        Debug.Log($"Switching tool to tool number {tool}");
        currentTool = (Tool)tool;
        SetButtons();
    }

    public Tool GetCurrentTool()
    {
        return currentTool;
    }

    public void SetButtons()
    {
        select.interactable = currentTool != Tool.select;
        place.interactable  = currentTool != Tool.place;
        bond.interactable   = currentTool != Tool.bond;
        delete.interactable = currentTool != Tool.delete;
        cut.interactable    = currentTool != Tool.cut;

        elementSelectionUI.SetActive(currentTool == Tool.place);
    }
}
