using TMPro;
using UnityEngine;

public class AtomElementDisplay : MonoBehaviour
{
    [SerializeField] private AtomManager atom;
    [SerializeField] private TextMeshPro text;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ElementData element = ElementDataLoader.element[atom.GetElement()];
        text.text = element.symbol;
        text.fontSize = element.textSize;
        if (element.isTextWhite)
        {
            text.color = Color.white;
        }
        else
        {
            text.color = Color.black;
        }
    }
}
