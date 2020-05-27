﻿using UnityEngine;

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

    private void UpdateMovement()
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
    
    public void TurnLeft()
    {
        transform.Rotate(Vector3.down * rotationSpeed);
    }
    
    public void TurnRight()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }

    public void SetBoost(bool activate)
    {
        boosting = activate;
    }
}
