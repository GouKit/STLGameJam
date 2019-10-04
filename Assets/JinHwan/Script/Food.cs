using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Vector3 origin;

    void Start()
    {
        origin = transform.position;
    }

    void OnMouseDown()
    {
        transform.localScale *= 1.5f;    
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    void OnMouseUp()
    {
        transform.localScale = new Vector3(1,1,1);
        transform.position = origin;
    }    
}
