using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Factories;
using System.IO;

public class PoolFog
{
    string[] _locationStrings;

    public string[] LoketionStrings
    {
        set
        {
            if (value != null)
            {
                _locationStrings = value;
                Save();
            }
        }
        get { return _locationStrings; }
    }

    public PoolFog()
    {
        Load();
    }

    public void Save()
    {
        StringArrayWrapper wrapper = new StringArrayWrapper { array = _locationStrings };
        string jsonData = JsonUtility.ToJson(wrapper);
        string filePath = Path.Combine(Application.persistentDataPath, "locationFog.json");

        File.WriteAllText(filePath, jsonData);
        Debug.Log($"Вроде сохранил({filePath})");
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "locationFog.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            StringArrayWrapper wrapper = JsonUtility.FromJson<StringArrayWrapper>(jsonData);
            _locationStrings = wrapper.array;
            Debug.Log("Старый список(Fog)");
        }
    }
}
