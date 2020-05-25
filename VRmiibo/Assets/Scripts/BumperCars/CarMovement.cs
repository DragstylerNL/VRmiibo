using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float boostAccelerationSpeed;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 0.1f;
    [SerializeField] private float maxBoostSpeed = 0.5f;
    private bool boosting = false;
    private Vector3 direction;

    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    void Update()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {
        if (boosting){
            direction += transform.forward * boostAccelerationSpeed;
            if (direction.magnitude > maxBoostSpeed) direction = direction.normalized * maxBoostSpeed;
            transform.position += direction; 
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                TurnLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                TurnRight();
            }

            if (Input.GetKey(KeyCode.B))
            {
                SetBoost(true);
            }
            
            direction += transform.forward * accelerationSpeed;
            if (direction.magnitude > maxMoveSpeed) direction = direction.normalized * maxMoveSpeed;
            transform.position += direction;
        }
    }
    
    private void TurnLeft()
    {
        transform.Rotate(Vector3.down * rotationSpeed);
    }
    
    private void TurnRight()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }

    private void SetBoost(bool activate)
    {
        boosting = activate;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Car"))
        {
            FindObjectOfType<CarCollisionManager>().Bump(this, other.collider.GetComponent<CarMovement>());
        }
    }
}
