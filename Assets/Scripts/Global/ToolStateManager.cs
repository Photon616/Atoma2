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

    public Tool currentTool = Tool.select;

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
