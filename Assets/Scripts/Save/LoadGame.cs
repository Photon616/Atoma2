using System;
using System.IO;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public static LoadGame Instance{get; private set;}
    public event Action OnLoadFinished;

    [SerializeField] private GameObject cam;
    private int wasLoaded = 0;
    PhysicsManager physicsManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        physicsManager = PhysicsManager.Instance;
    }

    private void Update()
    {
        if (wasLoaded > 0)
        {
            wasLoaded--;
        }
        if (wasLoaded == 1)
        {
            OnLoadFinished?.Invoke();
            wasLoaded = 0;
        }
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");

        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file does not exist.");
            return;
        }
        
        Debug.Log($"Loaded from: {path}");

        physicsManager.ClearSimulation();

        string stringData = File.ReadAllText(path);

        SimulationSaveData data = JsonUtility.FromJson<SimulationSaveData>(stringData);

        foreach (AtomData atom in data.atoms)
        {
            physicsManager.AddAtom(atom.position, atom.velocity, atom.elementNumber, atom.id, true);
        }

        foreach (BondData bond in data.bonds)
        {
            physicsManager.AddBond(bond.atomAId, bond.atomBId, bond.order);
        }

        cam.transform.position = data.cameraPosition;
        cam.GetComponent<Camera>().orthographicSize = data.cameraZoom;

        wasLoaded = 3;
    }
}
