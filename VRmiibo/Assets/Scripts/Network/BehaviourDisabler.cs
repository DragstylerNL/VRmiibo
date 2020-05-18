using System.Collections.Generic;
using UnityEngine;

public class BehaviourDisabler : MonoBehaviour
{
    // ============================================================================================= 'Private' variables
    [SerializeField] private List<Behaviour> componentsToDisable = new List<Behaviour>();
    
    // =========================================================================================================== Start
    public void Disable()
    {
        foreach (var c in componentsToDisable)
        {
            c.enabled = false;
        }
    }
}
