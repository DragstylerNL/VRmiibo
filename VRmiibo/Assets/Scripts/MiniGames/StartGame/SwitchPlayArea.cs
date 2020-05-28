using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayArea : MonoBehaviour
{
    public GameObject newPlayArea;

    void Start()
    {
        GameObject oldArea = GameObject.FindWithTag("level");
        GameObject newobj = Instantiate(newPlayArea);
        newobj.transform.position = oldArea.transform.position;
        Destroy(oldArea);
    }
}