using UnityEngine;

public class AtomManager : MonoBehaviour
{
    [SerializeField] private uint id;
    [SerializeField] private float drag = 1.0f;
    [SerializeField] private float stopThreshold = 0.01f;
    public Vector2 velocity;
    private bool isSelected = false;
    private bool isBeingDragged = false;

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

    public void SetID(uint i)
    {
        id = i;
    }

    public uint GetID()
    {
        return id;
    }

    public void StartDrag()
    {
        isBeingDragged = true;
    }

    public void EndDrag()
    {
        isBeingDragged = false;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
