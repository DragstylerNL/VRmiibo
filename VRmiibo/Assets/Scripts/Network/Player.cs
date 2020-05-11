using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BehaviourDisabler))]
public class Player : MonoBehaviour
{
    // =============================================================================================== private variables
    private NetworkClient CLIENT;
    private string _playerID;
    private string _nickname;
    private int _avatar;

    // ================================================================================================ public variables

    // ================================================================================================= Public Set vars
    public void SetID(string ID) // --------------------------------- set ID
    {_playerID = ID;}

    public void SetNick(string nickname) // ------------------------- set Name
    {GetComponentInChildren<Text>().text = _nickname = nickname;}

    public void SetAvatar(int ava) // ------------------------------- set Avatar
    {_avatar = ava;}
    
    // =========================================================================================================== Start
    private void Start()
    {
        CLIENT = GameObject.Find("[ NETWORKCLIENT ]").GetComponent<NetworkClient>();
    }

    // ========================================================================================================== Update
    private void Update()
    {
        UpdatePosition();
    }
    
    // ========================================================================================================== Update
    private void UpdatePosition()
    {
        var position = transform.position;
        position += Vector3.right * (Input.GetAxis("Horizontal") * Time.deltaTime * 20f);
        position += Vector3.up * (Input.GetAxis("Vertical") * Time.deltaTime * 20f);
        transform.position = position;
    }
}
