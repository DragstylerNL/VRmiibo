using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayArea : MonoBehaviour
{
    public GameObject newPlayArea;

    void Start()
    {
        GameObject oldArea = GameObject.FindWithTag("PlayArea");
        Instantiate(newPlayArea, oldArea.transform);
    }
}