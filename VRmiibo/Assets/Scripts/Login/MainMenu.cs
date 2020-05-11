using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField NameInputField;
    public ImageViewer ImageViewer;

    public void OnPlay(int scene)
    {
        if (NameInputField.text.Length >= 6)
        {
            //Put name to json thingy here
            GoToScene(scene);
        }
    }
    
    public void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}