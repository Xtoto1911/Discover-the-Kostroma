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

    [SerializeField] float clickDistance;

    [SerializeField] GameObject spantQues;

    private GameObject fog;

    public GameObject Fog
    {
        private set => fog = value;
        get => fog;
    }

    void Start()
    {
        _poolMissedEvent = new PoolMissedEvent(SavePath);
        if (!_poolMissedEvent.Load(SavePath))
        {
            _poolMissedEvent.LoketionStrings = defArr;
            Debug.Log("����� ������");
        }
        _locationStrings = _poolMissedEvent.LoketionStrings;
        _locations = new Vector2d[_locationStrings.Length];
        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < _locationStrings.Length; i++)
        {
            var locationString = _locationStrings[i];
            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(_markerPrefab, transform);
            instance.GetComponent<EventFoinFog>()._position = _locations[i];
            instance.GetComponent<EventFoinFog>().ActiveDistace = clickDistance;
            instance.GetComponent<EventFoinFog>().Spavner = gameObject;
            instance.GetComponent<EventFoinFog>().SpavnewQuestions = spantQues;
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
            if (spawnedObject != _spawnedObjects[count - 1])
            {
                _spawnedObjects[i].SetActive(false);
            }
            spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
            spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
        if (!flag && count > 0 && _spawnedObjects[0].transform.position != new Vector3(0, 5.14f, 0))
        {
            fog = Instantiate(_rectanglePrefab, transform);
            Vector3 minPoint = _spawnedObjects[0].transform.localPosition;
            Vector3 maxPoint = _spawnedObjects[0].transform.localPosition;
            foreach (var obj in _spawnedObjects)
            {
                minPoint = Vector3.Min(minPoint, obj.transform.localPosition);
                maxPoint = Vector3.Max(maxPoint, obj.transform.localPosition);
            }
            Vector3 center = (minPoint + maxPoint) / 2f;
            Vector3 size = maxPoint - minPoint;
            fog.transform.position = new Vector3(center.x,center.y + 5,center.z);
            fog.transform.localScale = new Vector3(size.x, size.x - size.x % 20, size.z);
            flag = true;
        }
    }
}
