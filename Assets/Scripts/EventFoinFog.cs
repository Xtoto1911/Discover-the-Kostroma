using Mapbox.Examples;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventFoinFog : MonoBehaviour
{
    private LocationStatus _player;
    [SerializeField] private float activDistace;
    [SerializeField] public Vector2d _position;
 
    private GameObject spavnerQuestions;

    public GameObject SpavnewQuestions
    {
        get
        {
            return spavnerQuestions;
        }
        set
        {
            if(value != null)
                spavnerQuestions = value;
        }
    }

    private GameObject spavner;

    public GameObject Spavner
    {
        get => spavner;
        set
        {
            if (value != null)
                spavner = value;
        }
    }

    public float ActiveDistace
    {
        get
        {
            return activDistace;
        }
        set
        {
            if (value != activDistace)
            {
                activDistace = value;
            }
        }
    }

    public void OnObjectClick()
    {
        _player = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        SpawnFog fog = Spavner.GetComponent<SpawnFog>();
        var currentPlayerLoc = new GeoCoordinatePortable.GeoCoordinate(_player.GetLocationLat(), _player.GetLocationLong());
        var eventLoc = new GeoCoordinatePortable.GeoCoordinate(_position[0], _position[1]);
        var distance = currentPlayerLoc.GetDistanceTo(eventLoc);
        Debug.Log($"{distance}");
        if (distance <= activDistace)
        {
            gameObject.SetActive(false);
            fog.Fog.gameObject.SetActive(false);
            SpavnewQuestions.SetActive(true);
        }
    }

    public void OnMouseDown()
    {
        OnObjectClick();
    }
}
