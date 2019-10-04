using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Collider2D coll;
    private Vector3 origin;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        origin = transform.position;
    }

    void OnMouseDown()
    {
        transform.localScale *= 1.5f;
        coll.enabled = false;    
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    void OnMouseUp()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(1,1,1);
        transform.position = origin;

        GameObject temp = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(1,1), 0).gameObject;
        Debug.Log(temp.name);

        coll.enabled = true;
    }    
}
