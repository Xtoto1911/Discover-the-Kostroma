using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveCamera : MonoBehaviour
{

    public Transform target; // Ссылка на персонажа
    public float distance = 5.0f; // Расстояние от персонажа до камеры
    public float height = 3.0f; // Высота камеры над персонажем
    public float damping = 2.0f; // Плавность движения камеры

    void LateUpdate()
    {
        // Вычисляем целевую позицию камеры
        Vector3 targetPosition = target.position + new Vector3(0, height, -distance);

        // Плавно двигаем камеру к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);

        // Направляем камеру на персонажа
        transform.LookAt(target);
    }
}
