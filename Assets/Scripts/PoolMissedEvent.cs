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

    public PoolMissedEvent()
    {
        Load();
    }

    public void Save()
    {
        StringArrayWrapper wrapper = new StringArrayWrapper { array = _locationStrings };
        string jsonData = JsonUtility.ToJson(wrapper);
        string filePath = Path.Combine(Application.persistentDataPath, "locationStrings.json");

        File.WriteAllText(filePath, jsonData);
        Debug.Log($"Вроде сохранил({filePath})");
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "locationStrings.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            StringArrayWrapper wrapper = JsonUtility.FromJson<StringArrayWrapper>(jsonData);
            _locationStrings = wrapper.array;
            Debug.Log("Старый список");
        }
        else
        {
            LoketionStrings = new string[]
            {
                "57.768520055575486, 40.92599575087467",//Каланча
                "57.76697615890837, 40.92477886355354",//Сусанин
                "57.76729913262451, 40.92766853590996",//Снегурочка
                "57.76657623559337, 40.9293277628552",//Юрий Долгорукий
                "57.77023610885775, 40.93150122896323",//Драм.театра Н.Островского
                "57.77261425824748, 40.93944449575506",//Монумент Славы
                "57.770261746665504, 40.91809192389704",//Место основания
                "57.76172212278889, 40.92876323614582"//Беседка Островского
            };
            Debug.Log("Новый список");
        }
    }
}
