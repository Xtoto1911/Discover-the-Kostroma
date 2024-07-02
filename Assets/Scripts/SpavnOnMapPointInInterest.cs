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

    [SerializeField]
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

    void Start()
    {
        _poolMissedEvent = new PoolMissedEvent(SavePath);
        if (!_poolMissedEvent.Load(SavePath))
        {
            _poolMissedEvent.LoketionStrings = new string[]
            {
                "57.768520055575486, 40.92599575087467",//�������
                "57.76697615890837, 40.92477886355354",//�������
                "57.76729913262451, 40.92766853590996",//����������
                "57.76657623559337, 40.9293277628552",//���� ����������
                "57.77023610885775, 40.93150122896323",//����.������ �.�����������
                "57.77261425824748, 40.93944449575506",//�������� �����
                "57.770261746665504, 40.91809192389704",//����� ���������
                "57.76172212278889, 40.92876323614582"//������� �����������
            };
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
    }
}
