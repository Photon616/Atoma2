using UnityEngine;

public class IsMouseOnUI : MonoBehaviour
{
    public static bool isMouseOnUI;

    public void MouseHover()
    {
        isMouseOnUI = true;
    }

    public void MouseHoverEnd()
    {
        isMouseOnUI = false;
    }
}
