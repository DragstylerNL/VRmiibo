using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public int playerAmount;
    
    private NetworkClient _networkClient;//Network will need to send data when player enters the play zone to give the player a player number ID(Player 1, player 2, etc)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //other.getComponent<PlayerData>().GameID == playerAmount++;
            //Call _networkclient player amount send
        }
    }
}
