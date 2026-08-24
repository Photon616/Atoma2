using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance{ get; private set; }
    public static float timeScale = 1.0f;

    [SerializeField] private List<GameObject> atoms = new();
    [SerializeField] private Dictionary<ulong, GameObject> bonds = new();
    [SerializeField] private GameObject atomPrefab;
    [SerializeField] private GameObject bondPrefab;
    [SerializeField] private float maxRepelDistance = 2.0f;
    [SerializeField] private float repelForce = 2.0f;
    private uint idCounter = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < atoms.Count; i++)
        {
            GameObject atomA = atoms[i];
            AtomManager managerA = atomA.GetComponent<AtomManager>();
            Vector3 positionA = atomA.transform.position;

            for (int j = i + 1; j < atoms.Count; j++)
            { 
                GameObject atomB = atoms[j];
                AtomManager managerB = atomB.GetComponent<AtomManager>();
                Vector3 positionB = atomB.transform.position;

                float distance = Vector3.Distance(positionA, positionB);
                if (distance >= maxRepelDistance)
                {
                    continue;
                }

                Vector3 dir = positionA - positionB; // B 기준 A의 방향(선대 응용)
                dir.z = 0; // z축으로는 움직이면 안 되니까
                dir.Normalize(); // 방향이니까 정규화

                float force = (GetRepelForceByDistance(distance) * repelForce) + 0;
                managerA.velocity += force * Time.deltaTime * (Vector2)dir;
                managerB.velocity -= force * Time.deltaTime * (Vector2)dir;
                // Debug.Log($"{dir}, {force}, {distance}");
            }
        }
    }

    public void AddAtom(Vector2 position)
    {
        GameObject tmpObj = Instantiate(atomPrefab, new Vector3(position.x, position.y, -idCounter / 100f), quaternion.identity);
        AtomManager tmpManager = tmpObj.GetComponent<AtomManager>();
        tmpManager.SetID(idCounter);

        idCounter++;
    }

    public void AddBond(uint id1, uint id2)
    {
        GameObject bond = Instantiate(bondPrefab);
        ulong key = PackIDs(id1, id2);
        bonds.Add(key, bond);
    }

    private ulong PackIDs(uint id1, uint id2)
    {
        uint a, b;
        if (id1 < id2)
        {
            a = id1;
            b = id2;
        }
        else
        {
            a = id2;
            b = id1;
        }
        return ((ulong)a << 32) | b;
    }

    private float GetRepelForceByDistance(float d)
    {
        return maxRepelDistance * Mathf.Pow(Mathf.Max(0, maxRepelDistance - d), 2);

        // float epsilon = 2.0f;
        // float s = 1;

        // return Mathf.Min(4f * epsilon * (Mathf.Pow(s / d, 12f) - Mathf.Pow(s / d, 6f)), epsilon);
    }
}
