using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BumperCarsWinTracker : MonoBehaviour
{
    List<GameObject> _bumperCars = new List<GameObject>();

    void Start()
    {
        var bumper = GameObject.FindGameObjectsWithTag("BumperCar");
        foreach (var car in bumper)
        {
            _bumperCars.Add(car);
        }
    }

    public void CheckActive()
    {
        int active = 0;
        for (int i = 0; i < _bumperCars.Count - 1; i++)
        {
            if (_bumperCars[i].activeSelf == true)
                active++;
        }

        if (active <= 1)
        {
            Destroy(GameObject.FindWithTag("DoNotDestroy"));
            SceneManager.LoadScene(1);
        }
        print(active > 1 ? "Continue" : "Finnish");
    }
}
