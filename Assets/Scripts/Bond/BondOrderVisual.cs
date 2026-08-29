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

    public void UpdateVisual(int order)
    {
        switch (order)
        {
            case 1:
                one.SetActive(true); //
                two.SetActive(false);
                three.SetActive(false);
                four.SetActive(false);
                break;

            case 2:
                one.SetActive(false);
                two.SetActive(true); //
                three.SetActive(false);
                four.SetActive(false);
                break;

            case 3:
                one.SetActive(false);
                two.SetActive(false); 
                three.SetActive(true); //
                four.SetActive(false);
                break;

            case 4:
                one.SetActive(false);
                two.SetActive(false); 
                three.SetActive(false);
                four.SetActive(true); //
                break;

            default:
            break;
        }
    }
}
