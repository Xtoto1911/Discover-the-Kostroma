using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Utils;

public class EvenPoint : MonoBehaviour
{
    public LocationStatus _player;
    [SerializeField] public float activDistace;
    [SerializeField] public Vector2d _position;

    [SerializeField]private GameObject _panel;

    public GameObject Panel
    {
        get => _panel;
        set
        {
            if (value != null)
            {
                _panel = value;
                
            }
        }
    }
    private GameObject spavner;

    public GameObject Spavner
    {
        get => spavner;
        set
        {
            if(value != null)
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
        
        var currentPlayerLoc = new GeoCoordinatePortable.GeoCoordinate(_player.GetLocationLat(), _player.GetLocationLong());
        var eventLoc = new GeoCoordinatePortable.GeoCoordinate(_position[0], _position[1]);
        var distance = currentPlayerLoc.GetDistanceTo(eventLoc);
        Debug.Log($"{distance}");
        if (distance <= activDistace && _panel != null)
        {
            _panel.SetActive(true);
        }
    }

    public void OnMouseDown()
    {
        OnObjectClick();
    }
}
