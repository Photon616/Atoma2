using System.IO;
using UnityEngine;

public class ElementDataLoader : MonoBehaviour
{
    [SerializeField] private TextAsset elementDataAsset;
    public static ElementData[] elements;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ElementDataWrapper data = JsonUtility.FromJson<ElementDataWrapper>(elementDataAsset.text);;

        elements = data.elements;
    }
}
