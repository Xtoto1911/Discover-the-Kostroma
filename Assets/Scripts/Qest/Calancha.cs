using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calancha : MonoBehaviour
{
    private GameObject point;

    [SerializeField] TMP_InputField text;
    [SerializeField]
    AbstractMap _map;
    [SerializeField] GameObject _finPanel;
    public GameObject _prefabFinish;
    public GameObject Point
    {
        get => point;
        set
        {
            if (value != null)
            {
                point = value;
            }
        }
    }


    public void Answer()
    {
        Point = gameObject.GetComponent<Complete>().Point;
        if (text.text == "6" || text.text.ToLower() == "шесть")
        {
            gameObject.SetActive(false);
            Vector2d _pos = Point.GetComponent<EvenPoint>()._position;
            var instance = Instantiate(_prefabFinish);
            instance.GetComponent<EventFinish>()._position = _pos;
            instance.GetComponent<EventFinish>().ActiveDistace = Point.GetComponent<EvenPoint>().activDistace;
            instance.GetComponent<EventFinish>().Panel = _finPanel;
            instance.transform.localPosition = _map.GeoToWorldPosition(_pos, true);
            instance.transform.localScale = new Vector3(5, 5, 5);
            Point.SetActive(false);
            GameObject.Find("Canvas").GetComponent<ProgressScenter>().Progress += 12.5f;
        }
        Debug.Log($"{text.text}");
    }
}
