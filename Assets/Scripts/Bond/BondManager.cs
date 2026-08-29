using Unity.Mathematics;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] private ulong id;
    [SerializeField] private float width;
    private BondOrderVisual bondOrderVisual;
    // private LineRenderer lr;
    public int order = 1;

    private void Awake()
    {
        // lr = GetComponent<LineRenderer>();
        bondOrderVisual = GetComponent<BondOrderVisual>();
    }

    public ulong GetID()
    {
        return id;
    }

    public void SetID(ulong i)
    {
        id = i;
    }

    public void UpdatePosition(Vector3 p1, Vector3 p2)
    {
        p1.z = transform.position.z;
        p2.z = transform.position.z;
        // lr.SetPosition(0, p1);
        // lr.SetPosition(1, p2);

        transform.position = (p1 + p2) / 2;
        transform.localScale = new Vector3(width, Vector3.Distance(p1, p2), width);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(p1.x - p2.x, p1.y - p2.y) * Mathf.Rad2Deg);

        // Debug.Log("updated bond position");
    }
    
    public void PlusOrder()
    {
        if (order < 4)
        {
            order++;
            bondOrderVisual.UpdateVisual(order);
        }
    }

    public void MinusOrder()
    {
        if (order > 1)
        {
            order--;
            bondOrderVisual.UpdateVisual(order);
        }
        else
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
