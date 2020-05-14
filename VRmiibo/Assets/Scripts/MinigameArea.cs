using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameArea : MonoBehaviour
{
    [SerializeField] private MinigamePanelElements minigamePanelElements;
    [SerializeField] private Sprite minigameSprite;
    [SerializeField] private string minigameText;
    [SerializeField] private int maxPlayersAllowed;
    private int _amountOfPeopleReady = 0;
    private int _amountOfPeopleInside = 0;
    private GameObject colliding;
    private bool visible;
    private int profileSpot;

    //has to be changed to multiplayer purposes
    private void UpdatePanel()
    {
        minigamePanelElements.minigameImage.sprite = minigameSprite;
        minigamePanelElements.waitingText.text = "" + _amountOfPeopleReady + "/" + _amountOfPeopleInside + " are ready to play " + minigameText;
    }
    
    //when a player enters the area
    public void Entered(GameObject other)
    {
        visible = true;
        colliding = other;
        minigamePanelElements.gameObject.SetActive(true);
        minigamePanelElements.readyButton.GetComponentInChildren<Text>().text = "Join lobby";
        minigamePanelElements.readyButton.onClick.AddListener(JoinLobby);
        UpdatePanel();
    }

    private void JoinLobby()
    {
        if (_amountOfPeopleInside >= maxPlayersAllowed) return;
        _amountOfPeopleInside++;
        minigamePanelElements.readyButton.onClick.RemoveListener(JoinLobby);
        minigamePanelElements.readyButton.onClick.AddListener(ReadyUnReady);
        minigamePanelElements.readyButton.GetComponentInChildren<Text>().text = "Ready";
        minigamePanelElements.closeWindowButton.onClick.AddListener(VisibleWindow);
        AddProfileSprite(colliding.GetComponent<Player>());
        UpdatePanel();
    }
    
    //when the player closes the window
    private void CloseWindow()
    {
        _amountOfPeopleInside--;
        minigamePanelElements.gameObject.SetActive(false);
        Text readyUnreadyButtonText = minigamePanelElements.readyButton.GetComponentInChildren<Text>();
        if (readyUnreadyButtonText.text == "Unready")
        {
            readyUnreadyButtonText.text = "Ready";
            _amountOfPeopleReady--;
        }
        minigamePanelElements.readyButton.onClick.RemoveListener(ReadyUnReady);
        RemoveProfileSprite(colliding.GetComponent<Player>());
        colliding = null;
    }

    //when people press the ready unready button
    public void ReadyUnReady()
    {
        Text readyUnreadyButtonText = minigamePanelElements.readyButton.GetComponentInChildren<Text>();
        if (readyUnreadyButtonText.text == "Ready")
        {
            readyUnreadyButtonText.text = "Unready";
            ChangeProfileSprite(true);
            _amountOfPeopleReady++;
        }
        else
        {
            readyUnreadyButtonText.text = "Ready";
            ChangeProfileSprite(false);
            _amountOfPeopleReady--;
        }
        UpdatePanel();
    }

    //adding a profile sprite to the first empty image
    private void AddProfileSprite(Player player)
    {
        for (int i = 0; i < minigamePanelElements.profilePictures.Length; i++)
        {
            if (minigamePanelElements.profilePictures[i].sprite == null)
            {
                profileSpot = i;
                minigamePanelElements.profilePictures[i].sprite = player.playerSprite;
                minigamePanelElements.profilePictures[i].color = Color.white;
                minigamePanelElements.profilePictures[i].GetComponentInChildren<Text>().text = AdjustName(player.playerName);
                minigamePanelElements.profilePictures[i].GetComponentInChildren<Text>().color = Color.white;
                break;
            }
        }
    }
    
    //change the sprite to a ready or unready sprite
    private void ChangeProfileSprite(bool ready)
    {
        if (!ready)
        {
            minigamePanelElements.profilePictures[profileSpot].color = Color.white;
            Image[] images = minigamePanelElements.profilePictures[profileSpot].GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] != minigamePanelElements.profilePictures[profileSpot])
                {
                    images[i].color = Color.clear;
                    break;
                }
            }
        }
        else
        {
            minigamePanelElements.profilePictures[profileSpot].color = Color.white/2;
            Image[] images = minigamePanelElements.profilePictures[profileSpot].GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] != minigamePanelElements.profilePictures[profileSpot])
                {
                    images[i].color = Color.white;
                    break;
                }
            }
        }
    }
    
    //remove the player sprite
    private void RemoveProfileSprite(Player player)
    {
        for (int i = 0; i < minigamePanelElements.profilePictures.Length; i++)
        {
            if (minigamePanelElements.profilePictures[i].sprite == player.playerSprite)
            {
                minigamePanelElements.profilePictures[i].sprite = null;
                minigamePanelElements.profilePictures[i].color = Color.clear;
                minigamePanelElements.profilePictures[i].GetComponentInChildren<Text>().text = "";
                minigamePanelElements.profilePictures[i].GetComponentInChildren<Text>().color = Color.clear;
                break;
            }
        }
    }

    //adjust the name to a shorter one if the username is to long
    private string AdjustName(string playerName)
    {
        int maxNameLength = 6;
        if (playerName.Length <= maxNameLength) return playerName;
        string adjustedName = "";
        for (int i = 0; i < maxNameLength; i++)
        {
            if (i < maxNameLength - 2) adjustedName += playerName[i];
            else adjustedName += ".";
        }

        return adjustedName;
    }

    //hides and shows the window and changes the hide/show window button
    private void VisibleWindow()
    {
        visible = !visible;
        minigamePanelElements.ChangeWindowSprite();
        minigamePanelElements.gameObject.GetComponent<Animator>().SetBool("Visible", visible);
    }
}
