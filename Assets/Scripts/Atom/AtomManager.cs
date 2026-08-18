using UnityEngine;

public class AtomManager : MonoBehaviour
{
    [SerializeField] private uint id;
    [SerializeField] private float drag = 1.0f;
    [SerializeField] private float stopThreshold = 0.01f;
    public Vector2 velocity;

    private void Update()
    {
        transform.position += new Vector3(velocity.x, velocity.y) * Time.deltaTime;
        velocity *= 1 - (drag * Time.deltaTime);
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
}
