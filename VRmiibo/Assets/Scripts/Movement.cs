﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left* speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back* speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right* speed);
        }
    }
}
