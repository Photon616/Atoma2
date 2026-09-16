using System.IO;
using UnityEngine;

public class ElementDataLoader : MonoBehaviour
{
    [SerializeField] private TextAsset elementDataAsset;
    public static ElementData[] element;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ElementDataWrapper data = JsonUtility.FromJson<ElementDataWrapper>(elementDataAsset.text);

        element = data.elements;
        // Debug.Log(element.Length);
    }
}
