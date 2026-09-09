using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager Instance{ get; private set; }
    public static float timeScale = 1.0f;
    public List<GameObject> atoms = new();
    public Dictionary<ulong, GameObject> bonds = new();

    [SerializeField] private GameObject atomPrefab;
    [SerializeField] private GameObject bondPrefab;
    [SerializeField] private float maxRepelDistance = 2.0f;
    [SerializeField] private float repelForce = 2.0f;
    [SerializeField] private float minAttractDistance = 1.5f;
    [SerializeField] private float attractForceEaseRange = 2.0f;
    [SerializeField] private float attractForce = 2.0f;
    private uint idCounter = 0;
    private bool isPaused = false;

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

                Vector3 dir = positionA - positionB; // B 기준 A의 방향(선대 응용)
                dir.z = 0; // z축으로는 움직이면 안 되니까
                dir.Normalize(); // 방향이니까 정규화

                float force = 0;
                
                if (distance <= maxRepelDistance)
                {
                    force += GetRepelForceByDistance(distance) * repelForce;
                }
                
                if (distance >= minAttractDistance)
                {
                    ulong key = PackIDs(managerA.GetID(), managerB.GetID());
                    bool doesBondExist = bonds.ContainsKey(key);
                    if (doesBondExist)
                    {
                        force -= GetAttractForceByDistance(distance) * attractForce;
                        bonds[key].GetComponent<BondManager>().UpdatePosition(atomA.transform.position, atomB.transform.position);
                    }
                }
                
                Vector2 forceApplied = force * timeScale * Time.deltaTime * (Vector2)dir;
                managerA.velocity += forceApplied;
                managerB.velocity -= forceApplied;
                // Debug.Log($"{dir}, {force}, {distance}");
            }
        }
    }

    public void AddAtom(Vector2 position, Vector2 vel = new Vector2(), int element = 0)
    {
        GameObject tmpObj = Instantiate(atomPrefab, new Vector3(position.x, position.y, -idCounter / 100f), quaternion.identity);
        AtomManager tmpManager = tmpObj.GetComponent<AtomManager>();
        tmpManager.SetID(idCounter);
        tmpManager.SetElement(element);
        tmpManager.velocity = vel;

        atoms.Add(tmpObj);

        idCounter++;
    }

    public void AddBond(uint id1, uint id2, int order = 1)
    {
        ulong key = PackIDs(id1, id2);

        if (!bonds.ContainsKey(key)) // 그 key값의 결합이 없으면 새 결합 오브젝트를 만들고
        {
            GameObject bond = Instantiate(bondPrefab);
            BondManager tmpManager = bond.GetComponent<BondManager>();

            bonds.Add(key, bond);

            tmpManager.SetID(key);
            tmpManager.order = order;
        }
        else // 있으면 그 결합 오브젝트를 수정
        {
            bonds[key].GetComponent<BondManager>().PlusOrder();
        }

    }
    
    public void DeleteAtom(uint id)
    {
        GameObject atom = atoms.Find(x => x.GetComponent<AtomManager>().GetID() == id);
        atoms.Remove(atom);

        // 이 원자의 모든 결합 삭제
        AtomManager tmpManager = atom.GetComponent<AtomManager>();
        for (int i = 0; i < atoms.Count(); i++)
        {
            AtomManager iManager = atoms[i].GetComponent<AtomManager>();
            ulong bondID = PackIDs(tmpManager.GetID(), iManager.GetID());
            if (bonds.ContainsKey(bondID))
            {
                GameObject bond = bonds[bondID];
                bonds.Remove(bondID);
                bond.GetComponent<BondManager>().DestroySelf();
            }
        }

        tmpManager.DestroySelf();
    }

    public void DeleteBond(ulong id)
    {
        GameObject bond = bonds[id];
        BondManager manager = bond.GetComponent<BondManager>();

        if (manager.order == 1)
        {
            bonds.Remove(id);
        }
        
        bond.GetComponent<BondManager>().MinusOrder();
    }

    public void ToggleSimulationSpeed()
    {
        if (isPaused)
        {
            ResumeSimulation();
        }
        else
        {
            PauseSimulation();
        }
    }

    public void PauseSimulation()
    {
        timeScale = 0f;
        isPaused = true;
    }

    public void ResumeSimulation()
    {
        timeScale = 1f;
        isPaused = false;
    }

    private ulong PackIDs(uint id1, uint id2)
    {
        uint a, b;
        if (id1 < id2) // 작은 쪽이 왼쪽으로
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

    private float GetAttractForceByDistance(float d)
    {
        return Mathf.Pow((d - minAttractDistance) / 2, 2);
    }
}
