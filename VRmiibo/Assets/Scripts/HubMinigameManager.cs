using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubMinigameManager : MonoBehaviour
{
    public static Dictionary<string, MinigameArea> MinigameAreas = new Dictionary<string, MinigameArea>();
    
    // =========================================================================================================== Start
    private void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        DelayedStart();
    }
    

    // =========================================================================================================== Start
    private void DelayedStart()
    {
        GameObject.Find("[ NETWORKCLIENT ]").GetComponent<NetworkClient>().updateMinigameUI += UpdateMinigamesUI;
    }
    
    // ======================================================================================================== UpdateUI
    private void UpdateMinigamesUI(string area, int inarea, int ready)
    {
        MinigameAreas[area].SetAmount(inarea, ready);
    }
    
    // =============================================================================================== Add to dictionary
    public static void AddMe(string miniName, MinigameArea mini)
    {
        MinigameAreas.Add(miniName, mini);
    }
    
}
