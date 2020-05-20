using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCarsUI : MonoBehaviour
{
    [SerializeField]private GameObject _player;
    private bool boosting = false;

    public int cooldown;
    
    public void Left()
    {
        //Put reference to player right function
    }

    public void Right()
    {
        //Put reference to player left function
    }

    public void Boost()
    {
        if(!boosting)
            StartCoroutine(Boosting());
    }

    private IEnumerator Boosting()
    {
        boosting = true;
        //Put reference to player boost function
        yield return new WaitForSeconds(cooldown);
        boosting = false;
    }
}
