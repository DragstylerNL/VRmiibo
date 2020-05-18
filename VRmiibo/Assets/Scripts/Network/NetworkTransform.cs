using UnityEngine;

public class NetworkTransform : MonoBehaviour
{
    // =============================================================================================== Private variables
    private NetworkClient CLIENT;
    private Vector3 _oldPosition = new Vector3();
    
    // =========================================================================================================== Start
    private void Start()
    {
        CLIENT = GameObject.Find("[ NETWORKCLIENT ]").GetComponent<NetworkClient>();
    }
    // ========================================================================================================== Update
    private void Update()
    {
        Vector3 position = transform.position;
        if(_oldPosition != position)
            CLIENT.SetPosition(position);
        _oldPosition = position;
    }
}
