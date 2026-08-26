using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] private ulong id;
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
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
        lr.SetPosition(0, p1);
        lr.SetPosition(1, p2);

        transform.position = (p1 + p2) / 2;

        // Debug.Log("updated bond position");
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
