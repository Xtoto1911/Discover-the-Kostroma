using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Utils;

public class EvenPoint : MonoBehaviour
{
    private LocationStatus _player;
    [SerializeField]private float activDistace;
    [SerializeField] public Vector2d _position;

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
        if (distance <= activDistace)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        OnObjectClick();
    }
}
