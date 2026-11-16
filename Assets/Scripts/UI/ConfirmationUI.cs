using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;

    private System.Action confirmAction;
    private System.Action cancelAction;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void Show(string title, string message, System.Action confirmAction, System.Action CancelAction)
    {
        titleText.text = title;
        messageText.text = message;
        this.confirmAction = confirmAction;
        this.cancelAction = CancelAction;
        panel.SetActive(true);
    }

    public void Confirm()
    {
        confirmAction?.Invoke();
        panel.SetActive(false);
    }

    public void Cancel()
    {
        cancelAction?.Invoke();
        panel.SetActive(false);
        confirmAction = null;
    }
}