using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private NetworkClient _networkClient;//Network will need to send data when player enters the play zone to give the player a player number ID(Player 1, player 2, etc)
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            /*Player enters ready zone, send state(ready/not ready) to server
            Server updates the amount of players ready and the amount of players not ready and sends this back and shows it on the UI element
            If enough players are ready start countdown for begining the game(Activate onGameStart)*/
        }
    }

    //Load load scene on button call
    public void OnGameStart(int target)
    {
        SceneManager.LoadScene(2);
        GameObject.FindWithTag("Loader").GetComponent<Loader>().LoadScene(target);
        Destroy(this);
    }
}
