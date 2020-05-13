using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static bool created = false;
    void Start()
    {
        if(!created){
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else Destroy(gameObject);
    }
}
