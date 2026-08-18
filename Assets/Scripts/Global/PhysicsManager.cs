using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    private static PhysicsManager instance = null;

    [SerializeField] private List<GameObject> atoms = new();
    [SerializeField] private GameObject atomPrefab;
    [SerializeField] private float maxInteractionRadius = 10f;
    private uint idCounter = 0;

    private void Awake()
    {
        instance = this;
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
                if (distance >= maxInteractionRadius)
                {
                    continue;
                }

                Vector3 dir = positionA - positionB; // B 기준 A의 방향(선대 응용)
                dir.z = 0; // z축으로는 움직이면 안 되니까
                dir.Normalize(); // 방향이니까 정규화

                float force = GetForceByDistance(distance);
                managerA.velocity += (Vector2)dir * force;
                managerB.velocity -= (Vector2)dir * force;
                Debug.Log($"{dir}, {force}, {distance}");
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

    private float GetForceByDistance(float d)
    {
        float epsilon = 2.0f;
        float s = 1;

        return Mathf.Min(4f * epsilon * (Mathf.Pow(s / d, 12f) - Mathf.Pow(s / d, 6f)), epsilon);
    }
}
