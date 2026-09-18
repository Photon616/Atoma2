using UnityEngine;

public class AtomManager : MonoBehaviour
{
    [SerializeField] private AtomAppearance atomAppearance;
    [SerializeField] GameObject marker;
    [SerializeField] private uint id;
    [SerializeField] private float drag = 1.0f;
    [SerializeField] private float stopThreshold = 0.01f;
    public Vector2 velocity;
    // private bool isSelected = false;
    private bool isBeingDragged = false;

    // 화학적 특성들
    [SerializeField] private int elementNumber = 0;
    [SerializeField] private float mass = 1.0f;

    private void Update()
    {
        if (!isBeingDragged)
        {
            transform.position += new Vector3(velocity.x, velocity.y) * PhysicsManager.timeScale * Time.deltaTime;
            // Debug.Log(gameObject);
        }
        else
        {
            velocity = Vector2.zero;
        }
        
        velocity *= 1 - (drag * PhysicsManager.timeScale * Time.deltaTime);
        if (velocity.magnitude <= stopThreshold)
        {
            velocity = Vector2.zero;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PhysicsManager.Instance.GetMaxRepelDistance());
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, PhysicsManager.Instance.GetMinAttractDistance());
    }

    public void SetAppearance()
    {
        ElementData tmp = ElementDataLoader.element[elementNumber];
        atomAppearance.SetColor(tmp.fillColor, tmp.outlineColor);
        atomAppearance.SetSize(tmp.displaySize);
        Debug.Log(tmp.displaySize);
    }

    public void SetID(uint i)
    {
        id = i;
    }

    public uint GetID()
    {
        return id;
    }

    public void SetElement(int n)
    {
        elementNumber = n;
        SetMass(ElementDataLoader.element[elementNumber].mass);
    }

    public int GetElement()
    {
        return elementNumber;
    }

    public void SetMass(float n)
    {
        mass = n;
    }

    public float GetMass()
    {
        return mass;
    }

    public void StartDrag()
    {
        isBeingDragged = true;
    }

    public void EndDrag()
    {
        isBeingDragged = false;
    }

    public void Selected()
    {
        marker.SetActive(true);
    }

    public void Deselected()
    {
        marker.SetActive(false);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
