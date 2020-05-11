using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();

    [SerializeField]private Image _image;
    private int current = 0;

    private void Start()
    {
        _image.sprite = Sprites[current];
    }

    public void OnNextImage()
    {
        current++;
        if (current > Sprites.Count)
            current = 0;
        
        _image.sprite = Sprites[current];
    }

    public void OnPreviousImage()
    {
        current--;
        if (current < 0)
            current = Sprites.Count;

        _image.sprite = Sprites[current];
    }
}
