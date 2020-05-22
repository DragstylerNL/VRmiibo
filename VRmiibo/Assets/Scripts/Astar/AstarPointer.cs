using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarPointer : MonoBehaviour
{
    private Pathfinding _pathfinding;
    
    void Start()
    {
        _pathfinding = this.gameObject.GetComponent<Pathfinding>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            RayCheck(Input.GetTouch(0).position);
    }

    private void RayCheck(Vector2 touchPos)
    {
        Ray raycast = Camera.main.ScreenPointToRay(touchPos);

        if (Physics.Raycast(raycast, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.CompareTag("level"))
                _pathfinding.FindPath(raycastHit.point);
        }
    }
}
