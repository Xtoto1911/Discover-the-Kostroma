using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Utils;

public class EvenPoint : MonoBehaviour
{
    LocationStatus _player;
    [SerializeField] public Vector2d _position;


    public void OnObjectClick()
    {
        _player = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        var currentPlayerLoc = new GeoCoordinatePortable.GeoCoordinate(_player.GetLocationLat(), _player.GetLocationLong());
        var eventLoc = new GeoCoordinatePortable.GeoCoordinate(_position[0], _position[1]);
        var distance = currentPlayerLoc.GetDistanceTo(eventLoc);
        Debug.Log($"{distance}");
        if (distance <= 30)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        OnObjectClick();
    }
}
