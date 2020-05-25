using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionManager : MonoBehaviour
{
    [SerializeField] private float bounceStrength;

    public void Bump(CarMovement car1, CarMovement car2)
    {
        Vector3 TempDirCar1 = car1.Direction;
        Vector3 TempDirCar2 = car2.Direction;
        car1.Direction = TempDirCar2 + car1.transform.forward * -1 * bounceStrength;
        car2.Direction = TempDirCar1 + car2.transform.forward * -1 * bounceStrength;
    }
}
