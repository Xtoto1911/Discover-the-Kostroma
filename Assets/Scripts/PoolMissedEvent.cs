using Mapbox.Examples;
using Mapbox.Unity.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using System;
using System.IO;

public class PoolMissedEvent
{
    string[] _locationStrings;
    string _path;

    public string[] LoketionStrings {
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

    public string PathFroJson
    {
        get
        {
            return _path;
        }
        set
        {
            if(value != null)
                _path = value;
        }
    }

    public PoolMissedEvent(String path)
    {
        PathFroJson = path;
    }

    public void Save()
    {
        StringArrayWrapper wrapper = new StringArrayWrapper { array = _locationStrings };
        string jsonData = JsonUtility.ToJson(wrapper);
        string filePath = Path.Combine(Application.persistentDataPath, PathFroJson);

        File.WriteAllText(filePath, jsonData);
        Debug.Log($"Вроде сохранил({filePath})");
    }

    public bool Load(String path)
    {
        string filePath = Path.Combine(Application.persistentDataPath, PathFroJson);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            StringArrayWrapper wrapper = JsonUtility.FromJson<StringArrayWrapper>(jsonData);
            _locationStrings = wrapper.array;
            Debug.Log("Старый список");
            return true;
        }
        return false;
    }
}
