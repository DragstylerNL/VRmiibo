using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameArea : MonoBehaviour
{
    [SerializeField] private GameObject minigamePanel;
    [SerializeField] private Sprite minigameSprite;
    [SerializeField] private string minigameText;
    private int _amountOfPeopleReady = 0;
    private int _amountOfPeopleInside = 0;

    //has to be changed to multiplayer purposes
    private void UpdatePanel()
    {
        Image[] panelImages = minigamePanel.GetComponentsInChildren<Image>();
        for (int i = 0; i < panelImages.Length; i++)
        {
            if (panelImages[i].name == "Minigame Image")
            {
                panelImages[i].sprite = minigameSprite;
            }
        }
        Text[] texts = minigamePanel.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name == "MinigameText")
            {
                texts[i].text = minigameText;
            }
            else if(texts[i].name == "Players Waiting text")
            {
                texts[i].text = "" + _amountOfPeopleReady + "/" + _amountOfPeopleInside + "are ready";
            }
        }
    }
    
    //when a player enters the area
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _amountOfPeopleInside++;
        minigamePanel.SetActive(true);
        UpdatePanel();
        minigamePanel.GetComponentInChildren<Button>().onClick.AddListener(ReadyUnReady);
    }
    
    //when a player leaves the area
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _amountOfPeopleInside--;
        minigamePanel.SetActive(false);
        Text readyUnreadyButtonText = minigamePanel.GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        if (readyUnreadyButtonText.text == "Unready")
        {
            readyUnreadyButtonText.text = "Ready";
            _amountOfPeopleReady--;
        }
        minigamePanel.GetComponentInChildren<Button>().onClick.RemoveListener(ReadyUnReady);
    }

    //when people press the ready unready button
    public void ReadyUnReady()
    {
        Text readyUnreadyButtonText = minigamePanel.GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        if (readyUnreadyButtonText.text == "Ready")
        {
            readyUnreadyButtonText.text = "Unready";
            _amountOfPeopleReady++;
        }
        else
        {
            readyUnreadyButtonText.text = "Ready";
            _amountOfPeopleReady--;
        }
        UpdatePanel();
    }
}
