using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private KeyCode[] directions;
    
    //Temporary movement
    void Update()
    {
        if (Input.GetKey(directions[0]))
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (Input.GetKey(directions[1]))
        {
            transform.Translate(Vector3.left* speed);
        }
        if (Input.GetKey(directions[2]))
        {
            transform.Translate(Vector3.back* speed);
        }
        if (Input.GetKey(directions[3]))
        {
            transform.Translate(Vector3.right* speed);
        }
    }
}
