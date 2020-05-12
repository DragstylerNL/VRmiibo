using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite playerSprite;
    public Sprite playerReadySprite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MiniGameZone")
        {
            other.GetComponent<MinigameArea>().Entered(gameObject);
        }
    }
}
