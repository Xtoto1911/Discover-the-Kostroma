using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveCamera : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float height = 3.0f; 
    public float damping = 2.0f; 

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + new Vector3(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        transform.LookAt(target);
    }
}
