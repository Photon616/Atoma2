using TMPro;
using UnityEngine;

public class ElementSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject ElementSelectionButton;
    [SerializeField] TextMeshProUGUI ElementSelectionButtonText;
    
    [SerializeField] GameObject QuickslotObject;

    void Start()
    {
        CloseElementSelectionUI();
        ChangeElement(1);
    }

    public void ChangeElement(int n)
    {
        SelectedElement.element = n;
        string displayText = ElementDataLoader.element[SelectedElement.element].symbol + " / " + ElementDataLoader.element[SelectedElement.element].number;
        ElementSelectionButtonText.text = displayText;
        Debug.Log(displayText);
    }

    public void CloseElementSelectionUI()
    {
        ElementSelectionButton.SetActive(true);
        QuickslotObject.SetActive(false);
    }

    public void OpenElementSelectionUI()
    {
        ElementSelectionButton.SetActive(false);
        QuickslotObject.SetActive(true);
    }
}
