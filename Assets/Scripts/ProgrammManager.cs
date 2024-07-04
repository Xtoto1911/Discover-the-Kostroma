using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class ProgrammManager : MonoBehaviour
{
    public GameObject planeMarkerPrefab;
    public Camera ARCamera;
    private ARRaycastManager ARRaycastManagerScript;

    private Vector2 TouchPosition;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject ObjectToSpawn;
    public GameObject SelectedObject;

    void Start()
    {
        ARRaycastManagerScript = FindAnyObjectByType<ARRaycastManager>();
        planeMarkerPrefab.SetActive(false);
    }

    void Update()
    {
        //ShowMarker();
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            Touch touch = Input.GetTouch(0);
            TouchPosition = touch.position;

            ARRaycastManagerScript.Raycast(TouchPosition, hits, TrackableType.Planes);

            Instantiate(ObjectToSpawn, hits[0].pose.position, ObjectToSpawn.transform.rotation);
        }
        
        MoveObject();
    }

    private void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            planeMarkerPrefab.transform.position = hits[0].pose.position;
            planeMarkerPrefab.SetActive(true);
        }
        else
        {
            planeMarkerPrefab.SetActive(false);
        }
    }

    void MoveObject()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            TouchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("Unselected"))
                    {
                        hitObject.collider.gameObject.tag = "Selected";
                    }
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                ARRaycastManagerScript.Raycast(TouchPosition, hits, TrackableType.Planes);
                SelectedObject = GameObject.FindWithTag("Selected");
                SelectedObject.transform.position = hits[0].pose.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (SelectedObject.CompareTag("Selected"))
                {
                    SelectedObject.tag = "Unselected";
                }
            }
        }
    }
}
