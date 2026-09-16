using TMPro;
using UnityEngine;

public class ElementSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject ElementSelectionButton;
    [SerializeField] TextMeshProUGUI ElementSelectionButtonText;
    
    [SerializeField] GameObject QuickslotObject;
    
    public void ChangeElement(int n)
    {
        SelectedElement.element = n;
    }
}
