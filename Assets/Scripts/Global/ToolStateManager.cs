using UnityEngine;

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

    private Tool currentTool = Tool.select;

    void Awake()
    {
        Instance = this;
    }

    public void SetCurrentTool(Tool tool)
    {
        Debug.Log($"Switching tool to {tool}");
        currentTool = tool;
    }

    public Tool GetCurrentTool()
    {
        return currentTool;
    }
}
