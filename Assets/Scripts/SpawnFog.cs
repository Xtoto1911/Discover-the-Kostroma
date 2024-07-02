using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Factories;

public class SpawnFog : MonoBehaviour
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

    [SerializeField] private GameObject _rectanglePrefab;

    private bool flag = false;

    [SerializeField] private string SavePath;

    [SerializeField] string[] defArr;

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
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            _spawnedObjects.Add(instance);
        }
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
        if (!flag && _spawnedObjects[0].transform.position != new Vector3(0, 5.14f, 0))
        {
            GameObject rectangleInstance = Instantiate(_rectanglePrefab, transform);
            Vector3 minPoint = _spawnedObjects[0].transform.localPosition;
            Vector3 maxPoint = _spawnedObjects[0].transform.localPosition;
            foreach (var obj in _spawnedObjects)
            {
                minPoint = Vector3.Min(minPoint, obj.transform.localPosition);
                maxPoint = Vector3.Max(maxPoint, obj.transform.localPosition);
            }
            Vector3 center = (minPoint + maxPoint) / 2f;
            Vector3 size = maxPoint - minPoint;
            rectangleInstance.transform.position = new Vector3(center.x,center.y + 15,center.z);
            rectangleInstance.transform.localScale = new Vector3(size.x, 400f, size.z);
            flag = true;
        }
    }
}
