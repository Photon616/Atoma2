using UnityEngine;

public class AtomAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fill;
    [SerializeField] private SpriteRenderer outline;

    public void SetColor(string fillColor, string outlineColor)
    {
        Color color1;
        Color color2;
        
        ColorUtility.TryParseHtmlString(fillColor, out color1);
        ColorUtility.TryParseHtmlString(outlineColor, out color2);

        fill.color = color1;
        outline.color = color2;
    }
}
