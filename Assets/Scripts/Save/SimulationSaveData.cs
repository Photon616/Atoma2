using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimulationSaveData
{
    public int version = 1;
    public int atomCount;

    public string saveName;
    public string savedAt;
    
    public List<AtomData> atoms;
    public List<BondData> bonds;
    public Vector2 cameraPosition;
}
