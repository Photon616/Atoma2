using UnityEngine;

public class AtomManager : MonoBehaviour
{
    [SerializeField] private uint id;
    public Vector2 velocity;

    public uint GetID()
    {
        return id;
    }
}
