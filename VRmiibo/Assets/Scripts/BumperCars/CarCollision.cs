using System;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private bool collided = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Car"))
        {
            collided = true;
            FindObjectOfType<CarCollisionManager>().Bump(GetComponent<CarMovement>(), other.collider.GetComponent<CarMovement>());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        collided = false;
    }
}
