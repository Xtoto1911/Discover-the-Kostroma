using Mapbox.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ProgressScenter : MonoBehaviour
{
    [SerializeField] private TMP_Text progressText;

    private float progress;

    public float Progress
    {
        get => Progress;
        set
        {
            if (value < 0)
            {
                Progress = 0;
            }
            else
            {
                Progress = value;
                SaveProgress();
            }
        }
    }

    private SaveDataProgress saveData;

    [System.Serializable]
    private class SaveDataProgress
    {
        public float progress;
    }

    private void Start()
    {
        LoadProgress();
    }

    public void SaveProgress()
    {
        saveData = new SaveDataProgress();
        saveData.progress = Progress;

        string json = JsonConvert.SerializeObject(saveData);

        string path = Application.persistentDataPath + "/progressCenter.json";
        File.WriteAllText(path, json);
    }

    public void LoadProgress()
    {
        string path = Application.persistentDataPath + "/progressCenter.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonConvert.DeserializeObject<SaveDataProgress>(json);
            Progress = saveData.progress;
        }
        else
        {
            Progress = 0;
        }
    }

    private void Update()
    {
        progressText.text = progress.ToString()+"%";
    }
}
