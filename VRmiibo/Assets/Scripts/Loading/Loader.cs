using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField]private Image _progressBar;
    private Pose _levelPos;
    
    void Start()
    {
        StartCoroutine(LoadAsyncOperation(/*Put in int to the right scene from server, for tests target is on 1*/ 1));
    }

    public void LoadScene(int target)
    {
        StartCoroutine(LoadAsyncOperation(target));
    } 
    IEnumerator LoadAsyncOperation(int target)
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(target);
        while (gameLevel.progress < 1)
        {
            _progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
