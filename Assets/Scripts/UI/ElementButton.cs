using UnityEngine;

public class ElementButton : MonoBehaviour
{
    private ElementSelectionUI elementSelectionUI;

    void Start()
    {
        elementSelectionUI = GameObject.Find("ElementSelectionUI").GetComponent<ElementSelectionUI>();
    } 

    public void ChangeElement(int element)
    {
        elementSelectionUI.ChangeElement(element);
    }
}
