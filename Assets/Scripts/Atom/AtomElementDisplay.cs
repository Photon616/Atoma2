using TMPro;
using UnityEngine;

public class AtomElementDisplay : MonoBehaviour
{
    [SerializeField] private AtomManager atom;
    [SerializeField] private TextMeshPro text;
    private string[] elementSymbols = {"Tx"};
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = elementSymbols[atom.GetElement()];
    }
}
