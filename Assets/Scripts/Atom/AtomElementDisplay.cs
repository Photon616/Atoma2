using TMPro;
using UnityEngine;

public class AtomElementDisplay : MonoBehaviour
{
    [SerializeField] private AtomManager atom;
    [SerializeField] private TextMeshPro text;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = ElementDataLoader.element[atom.GetElement()].symbol;
    }
}
