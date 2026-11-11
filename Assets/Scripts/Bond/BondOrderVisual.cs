using Unity.VisualScripting;
using UnityEngine;

public class BondOrderVisual : MonoBehaviour
{
    [SerializeField] private GameObject one;
    [SerializeField] private GameObject two;
    [SerializeField] private GameObject three;
    [SerializeField] private GameObject four;
    private BondManager bm;

    private void Awake()
    {
        bm = GetComponent<BondManager>();
    }

    public void UpdateVisual()
    {
        int order = bm.order;

        one.SetActive(order == 1);
        two.SetActive(order == 2);
        three.SetActive(order == 3);
        four.SetActive(order == 4);

        Debug.Log($"{bm.GetID()}, Updated bond visuals to: {order}!");
    }
}
