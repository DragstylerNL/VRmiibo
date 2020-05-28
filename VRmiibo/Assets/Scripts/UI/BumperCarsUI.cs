using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCarsUI : MonoBehaviour
{
    private CarMovement _player;
    private NetworkClient _networkClient;
    private bool _boosting = false;
    
    public int cooldown;

    private void Start()
    {
        _networkClient = GameObject.FindWithTag("NETWORKCLIENT").GetComponent<NetworkClient>();
        _player = PlayerCollection.GetPlayer(_networkClient.NETWORKID).GetComponent<CarMovement>();
    }

    public void Left()
    {
        _player.TurnLeft();
    }

    public void Right()
    {
        _player.TurnRight();
    }

    public void Boost()
    {
        if(!_boosting)
            StartCoroutine(Boosting());
    }

    private IEnumerator Boosting()
    {
        _player.SetBoost(true);
        yield return new WaitForSeconds(cooldown);
        _player.SetBoost(false);
    }
}
