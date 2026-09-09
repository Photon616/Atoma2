using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    PhysicsManager physicsManager;

    void Start()
    {
        physicsManager = PhysicsManager.Instance;
    }

    public void Save()
    {
        SimulationSaveData saveData = new SimulationSaveData();
        saveData.atoms = new List<AtomData>();
        saveData.bonds = new List<BondData>();
        saveData.cameraPosition = new Vector2(cam.transform.position.x, cam.transform.position.y);

        foreach (GameObject atom in physicsManager.atoms)
        {
            AtomManager manager = atom.GetComponent<AtomManager>();
            AtomData atomData = new();

            atomData.id = manager.GetID();
            atomData.elementNumber = manager.GetElement();
            atomData.position = atom.transform.position;
            atomData.velocity = manager.velocity;

            saveData.atoms.Add(atomData);

            foreach (GameObject atom2 in physicsManager.atoms)
            {
                uint idA = manager.GetID();
                uint idB = atom2.GetComponent<AtomManager>().GetID();
                ulong key = PackIDs(idA, idB);

                if (physicsManager.bonds.ContainsKey(key))
                {
                    BondData bondData = new();

                    bondData.id = key;
                    bondData.atomAId = idA;
                    bondData.atomBId = idB;
                    bondData.order = physicsManager.bonds[key].GetComponent<BondManager>().order;
                }
            }
        }

        string path = Path.Combine(Application.persistentDataPath, "save.json");
        string jsonData = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, jsonData);
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
}
