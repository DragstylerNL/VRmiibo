using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField NameInputField;
    public ImageViewer ImageViewer;

    [SerializeField] private NetworkClient NetworkClient;

    public void OnPlay(int scene)
    {
        if (NameInputField.text.Length >= 6)
        {
            NetworkClient.RegisterOnServer(NameInputField.text, ImageViewer.current);
            GoToScene(scene);
        }
        else
        {
            Debug.Log("Error R1: To few characters in name");
        }
    }
    
    public void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}