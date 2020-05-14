using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePanelElements : MonoBehaviour
{
    public Image minigameImage;
    public Button readyButton;
    public Text waitingText;
    public Image[] profilePictures;
    public Button closeWindowButton;
    [SerializeField] private Sprite ShowWindowSprite;
    [SerializeField] private Sprite HideWindowSprite;

    public void ChangeWindowSprite()
    {
        if (closeWindowButton.image.sprite == ShowWindowSprite)
        {
            closeWindowButton.image.sprite = HideWindowSprite;
        }
        else
        {
            closeWindowButton.image.sprite = ShowWindowSprite;
        }
    }
}
