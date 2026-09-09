using System.IO;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    PhysicsManager physicsManager;

    void Start()
    {
        physicsManager = PhysicsManager.Instance;
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        TextAsset fileData = Resources.Load<TextAsset>(path);
        string stringData = fileData.text;

        SimulationSaveData data = JsonUtility.FromJson<SimulationSaveData>(stringData);

        foreach (AtomData atom in data.atoms)
        {
            physicsManager.AddAtom(atom.position, atom.velocity, atom.elementNumber);
        }

        foreach (BondData bond in data.bonds)
        {
            physicsManager.AddBond(bond.atomAId, bond.atomBId, bond.order);
        }

        cam.transform.position = data.cameraPosition;
    }
}
