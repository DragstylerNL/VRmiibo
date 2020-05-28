using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public List<GameObject> _objects = new List<GameObject>();
    void Awake()
    {
        foreach (var gameObject in _objects)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
