using TMPro;
using UnityEngine;

public class AlertText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        float frag = curve.Evaluate(timer / duration);
        Color col = new Color(1f, 1f, 1f, frag);
        txt.color = col;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void ShowText(string str)
    {
        txt.text = str;
        txt.enabled = true;
    }
}
