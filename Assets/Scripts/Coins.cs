using Mapbox.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> testList;

    private int count;

    public int Count
    {
        get => count;
        set
        {
            if (value < 0)
            {
                count = 0;
            }
            else
            {
                count = value;
            }
        }
    }

    private SaveData saveData;

    [System.Serializable]
    private class SaveData
    {
        public int count;
    }

    private void Start()
    {
        LoadCount();
    }

    public void SaveCount()
    {
        saveData = new SaveData();
        saveData.count = count;

        string json = JsonConvert.SerializeObject(saveData);

        string path = Application.persistentDataPath + "/coins.json";
        File.WriteAllText(path, json);
    }

    public void LoadCount()
    {
        string path = Application.persistentDataPath + "/coins.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonConvert.DeserializeObject<SaveData>(json);
            count = saveData.count;
        }
        else
        {
            count = 1000;
        }
    }

    private void Update()
    {
        foreach (TMP_Text text in testList)
        {
            text.text = count.ToString();
        }
    }
}
