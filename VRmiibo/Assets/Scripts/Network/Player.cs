using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BehaviourDisabler))]
public class Player : MonoBehaviour
{
    // =============================================================================================== private variables
    private NetworkClient CLIENT;
    private string _playerID;
    private int _avatar;

    // ================================================================================================ public variables
    public string nickname;
    public Sprite playerSprite;

    // ================================================================================================= Public Set vars
    public void SetID(string ID) // --------------------------------- set ID
    {_playerID = ID;}

    public void SetNick(string nickname) // ------------------------- set Name
    {GetComponentInChildren<Text>().text = this.nickname = nickname;}

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
		if(Input.GetKey(KeyCode.D))transform.position += Vector3.right * Time.deltaTime;
    }
    
    // ================================================================== on entering and exiting minigame starter areas
    private void OnTriggerEnter(Collider other)
    {
        if(CLIENT.NETWORKID != _playerID) return;
        if (other.CompareTag("MiniGameZone"))
        {
            MinigameArea mini = other.GetComponent<MinigameArea>();
            mini.Entered(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(CLIENT.NETWORKID != _playerID) return;
        if (other.CompareTag("MiniGameZone"))
        {
            MinigameArea mini = other.GetComponent<MinigameArea>();
            mini.CloseWindow();
            CLIENT.SetMinigame(mini.area, Enums.areastate.exit);
        }
    }
}
