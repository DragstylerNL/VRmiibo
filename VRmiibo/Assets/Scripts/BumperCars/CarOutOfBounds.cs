using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOutOfBounds : MonoBehaviour
{
    [SerializeField]private BumperCarsWinTracker _tracker;
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag.Equals("OutOfBounds"))
        {
            this.gameObject.SetActive(false);
            _tracker.CheckActive();
        }
    }
}
