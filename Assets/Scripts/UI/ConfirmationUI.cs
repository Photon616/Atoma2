using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;

    private System.Action confirmAction;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void Show(string title, string message, System.Action action)
    {
        titleText.text = title;
        messageText.text = message;
        confirmAction = action;
        panel.SetActive(true);
    }

    public void Confirm()
    {
        confirmAction?.Invoke();
        panel.SetActive(false);
    }

    public void Cancel()
    {
        panel.SetActive(false);
        confirmAction = null;
    }
}