using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete : MonoBehaviour
{
    private GameObject point;

    public GameObject Point
    {
        get { return point; }
        set
        {
            if(value != null)
                point = value;
        }
    }

    public void BackBtn()
    {
        gameObject.SetActive(false);
    }


}
