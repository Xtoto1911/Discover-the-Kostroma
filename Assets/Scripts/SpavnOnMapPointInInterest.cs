using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Factories;


public class SpavnOnMapPointInInterest : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [Geocode]
    string[] _locationStrings;
    Vector2d[] _locations;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _markerPrefab;

    List<GameObject> _spawnedObjects;

    private PoolMissedEvent _poolMissedEvent;

    [SerializeField] private string SavePath;

    [SerializeField] string[] defArr;

    [SerializeField] float clickDistance;

    void Start()
    {
        _poolMissedEvent = new PoolMissedEvent(SavePath);
        if (!_poolMissedEvent.Load(SavePath))
        {
            _poolMissedEvent.LoketionStrings = defArr;
            Debug.Log("Новый список");
        }
        _locationStrings = _poolMissedEvent.LoketionStrings;
        _locations = new Vector2d[_locationStrings.Length];
        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < _locationStrings.Length; i++)
        {
            var locationString = _locationStrings[i];
            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(_markerPrefab, transform);
            instance.GetComponent<EvenPoint>()._position = _locations[i];
            instance.GetComponent<EvenPoint>().ActiveDistace = clickDistance;
            instance.GetComponent<EvenPoint>().Spavner = gameObject;
            GameObject panel = GetPanelQuestions(locationString);
            panel.GetComponent<Complete>().Point = instance; 
            instance.GetComponent<EvenPoint>().Panel = panel;


            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            _spawnedObjects.Add(instance);
        }
    }

    private GameObject Search(string tag, GameObject[] allObjects)
    {
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == tag)
            {
                return obj;
            }
        }
        return null;
    }

    private GameObject GetPanelQuestions(string location)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        switch (location)
        {
            case ("57.768520055575486, 40.92599575087467"):
                {
                    try
                    {
                        return Search("Calancha", allObjects);
                    }catch(System.Exception e)
                    { 
                        break;
                    }
                }
            case ("57.76697615890837, 40.92477886355354"):
                {
                    try
                    {
                        return Search("Sysanin", allObjects);
                    }catch(System.Exception e)
                    {
                        break;
                    }
                }
            case ("57.76729913262451, 40.92766853590996"):
                {
                    try
                    {
                        return Search("Snegyrka", allObjects);
                    }catch(System.Exception e)
                    {
                        break ;
                    }
                }
            case ("57.76657623559337, 40.9293277628552"):
                {
                    try
                    {
                        return Search("Dolgoryki", allObjects);
                    }catch(System.Exception e)
                    {
                        break;
                    }
                }
            case ("57.77023610885775, 40.93150122896323"):
                {
                    try
                    {
                        return Search("Teatr", allObjects);
                    }catch(System.Exception e)
                    {
                        break;
                    }
                }
            case ("57.77261425824748, 40.93944449575506"):
                {
                    try
                    {
                        return Search("MonymentSlavi", allObjects);
                    }catch(System.Exception e)
                    {
                        break;
                    }
                }
            case ("57.770261746665504, 40.91809192389704"):
                {
                    try
                    {
                        return Search("Osnovanie", allObjects);
                    }catch(System.Exception e)
                    {
                        break;
                    }
                }
            case ("57.76172212278889, 40.92876323614582"):
                {
                    try
                    {
                        return Search("Besedka", allObjects);
                    }catch(System.Exception e)
                    {
                        break;
                    }
                }
            default: return Search("Besedka", allObjects); ;
        }
        return null;
    }

    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
            spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
    }
}
