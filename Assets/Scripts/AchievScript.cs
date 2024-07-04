using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchievScript : MonoBehaviour
{
    public Button AchievBtn;
    // Start is called before the first frame update
    void Start()
    {
        AchievBtn.onClick.AddListener(() => OnBackAchievClick());
    }

    void OnBackAchievClick()
    {
        SceneManager.LoadScene("TsentralnyyScene");

    }

}
