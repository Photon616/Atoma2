using UnityEngine;

public class AtomAppearance : MonoBehaviour
{
    [SerializeField] private GameObject fillObject;
    [SerializeField] private GameObject outlineObject;
    [SerializeField] private SpriteRenderer fill;
    [SerializeField] private SpriteRenderer outline;
    private float outlineWidth = 0.1f;

    public void SetColor(string fillColor, string outlineColor)
    {
        Color color1;
        Color color2;
        
        ColorUtility.TryParseHtmlString(fillColor, out color1);
        ColorUtility.TryParseHtmlString(outlineColor, out color2);
        
        fill.color = color1;
        outline.color = color2;
    }

    public void SetSize(float size)
    {
        outlineObject.transform.localScale = new Vector3(size, size, 1);

        size -= outlineWidth;
        fillObject.transform.localScale = new Vector3(size, size, 1);
    }
}
