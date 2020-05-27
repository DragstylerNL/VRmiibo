using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SocketIO;
using UnityEngine;

[RequireComponent(typeof(CameraPlayerPosition))]
[RequireComponent(typeof(PlayerCollection))]
public class NetworkClient : SocketIOComponent
{
    // ========================================================================================================== Events 
    #region Events
    public Action<string, int, int> updateMinigameUI = delegate(string area, int inarea, int ready){};
    
    #endregion
    
    // =============================================================================================== Private variables
    #region Private Variables
    private Enums.Areas _currentArea = Enums.Areas.hub;
    private bool _hubHasSpawned = false;
    private Transform _hub;
    #endregion

    // ================================================================================================ public variables
    #region public variables
    public GameObject playerPrefab, cameraPrefab;
    public string NETWORKID;
    #endregion

    // =========================================================================================================== Start
    public override void Start()
    {
        base.Start();

        SetupEvents();
    }

    // ========================================================================================================= set hub
    public void SetHub(bool hubHasSpawned, Transform hub)
    {
        _hubHasSpawned = hubHasSpawned;
        _hub = hub;
    }
    
    // =================================================================================================== Set up Events
    private void SetupEvents()
    {
        // RECIEVING
        On("open", (data) =>     // -------------- when connected
        {
            print("Connection made to server ^^");
        });
        On("registered", (data) => // ------------ when registered on the server 
        {
            StartCoroutine(Register(data));
        }); 
        On("activePlayers", (data) => // --------- get all other active players
        {
            StartCoroutine(ActivePlayers(data));
        });
        On("updatePosition", (data) => // -------- update positions of players
        {
            if(_hubHasSpawned)UpdatePosition(data);
        });
        On("gameZone", (data) => // -------------- area updates
        {
            if(_hubHasSpawned)UpdateGameZone(data);
        });
        On("disconnected", (data) => // ---------- on other player disconnect
        {
            StartCoroutine(Disconnect(data));
        });
        On("CameraUpdate", (data) => // ---------- on other player moved phone
        {
            CameraUpdate(data);
        });
    }

    IEnumerator Register(SocketIOEvent E)
    {
        while (!_hubHasSpawned)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0);
        
        NETWORKID = RemoveQuotes(E.data["id"].ToString());               // get ID
        GameObject playa = Instantiate(playerPrefab, _hub);                   // spawn player
        playa.name = "Client Player: " + NETWORKID;                           // set player name
        Player p = playa.GetComponent<Player>();                              // get player script
        p.SetID(NETWORKID);                                                   // set ID in player
        p.SetNick(RemoveQuotes(E.data["name"].ToString()));    // set players name
        p.SetAvatar(int.Parse(E.data["avatar"].ToString()));             // set player avatar
        PlayerCollection.ActivePlayers.Add(NETWORKID, playa);                 // add the player to the player collection

        GameObject cam = Instantiate(cameraPrefab, _hub);
    }

    IEnumerator ActivePlayers(SocketIOEvent E)
    {
        while (!_hubHasSpawned)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0);
        
        var otherPlayerID = RemoveQuotes(E.data["id"].ToString()); // get other players ID
        GameObject otherPlayer = Instantiate(playerPrefab, _hub);               // spawn player
        var pos = new Vector3(                                                 // get its position
            float.Parse(E.data["x"].ToString()),     // X
            float.Parse(E.data["y"].ToString()),     // Y
            float.Parse(E.data["z"].ToString()));    // Z
        otherPlayer.transform.localPosition = pos;                             // set the position
        otherPlayer.name = "Other Player: " + otherPlayerID;                   // set name
        Player p = otherPlayer.GetComponent<Player>();                         // get player script
        p.SetID(NETWORKID);                                                    // set ID in player
        p.SetNick(RemoveQuotes(E.data["name"].ToString()));    // set players name
        p.SetAvatar(int.Parse(E.data["avatar"].ToString()));              // set player avatar
        otherPlayer.GetComponent<BehaviourDisabler>().Disable();               // disable stuff like movement
        
        PlayerCollection.ActivePlayers.Add(otherPlayerID, otherPlayer);        // set player in the database
    }

    private void UpdatePosition(SocketIOEvent E)
    {
        var ID = RemoveQuotes(E.data["id"].ToString());            // get ID
        var pos = new Vector3(                                                 // get its position
            float.Parse(E.data["x"].ToString()),     // X
            float.Parse(E.data["y"].ToString()),     // Y
            float.Parse(E.data["z"].ToString()));    // Z
        PlayerCollection.ActivePlayers[ID].transform.localPosition = pos;      // set the position
    }

    private void UpdateGameZone(SocketIOEvent E)
    {
        var area = RemoveQuotes(E.data["area"].ToString());
        if (_currentArea.ToString() != area) return;

        updateMinigameUI(area, int.Parse(E.data["inarea"].ToString()), int.Parse(E.data["ready"].ToString()));
    }
    
    IEnumerator Disconnect(SocketIOEvent E)
    {
        while (!_hubHasSpawned)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        
        PlayerCollection.RemovePlayer(RemoveQuotes(E.data["id"].ToString())); // Remove disconnected player
    }

    private void CameraUpdate(SocketIOEvent E)
    {
        var ID = RemoveQuotes(E.data["id"].ToString());            // get ID
        var pos = new Vector3(                                                 // get its position
            float.Parse(E.data["x"].ToString()),     // X
            float.Parse(E.data["y"].ToString()),     // Y
            float.Parse(E.data["z"].ToString()));    // Z
        var rot = new Vector3(                                                 // get its Rotation
            float.Parse(E.data["cx"].ToString()),     // X
            float.Parse(E.data["cy"].ToString()),     // Y
            float.Parse(E.data["cz"].ToString()));    // Z
        //Vector3 HubRot = _hub.rotation.eulerAngles;
        Transform cam = CameraPlayerPosition.ActivePlayerCameras[ID].transform;
        cam.rotation = Quaternion.Euler(rot);
        cam.localPosition = pos;
    }

    // ================================================================================================ Remove Quotation
    private string RemoveQuotes(string data)
    {
        List<char> array = data.ToList();
        array.RemoveAt(0);
        array.RemoveAt(array.Count-1);
        data = "";
        for (int i = 0; i < array.Count; i++)
        {
            data += array[i];
        }
        return data;
    }
    
    // ============================================================================================== Register on server
    public void RegisterOnServer(string username, int arrayPos)
    {
        Emit("registered", new JSONObject(JsonUtility.ToJson(new JsonRegister(username, arrayPos))));
    }
    
    // ============================================================================================ Update pos on server
    public void SetPosition(Vector3 pos)
    {
        Emit("updatePosition", new JSONObject(JsonUtility.ToJson(new JsonPosition(pos))));
    }
   
    // ================================================================================================== minigame areas
    public void SetMinigame(Enums.Areas area, Enums.areastate state)
    {
        _currentArea = state == Enums.areastate.exit? Enums.Areas.hub : area;
        Emit("gameZone", new JSONObject(JsonUtility.ToJson(new JsonAreaUpdate(area.ToString(), state.ToString()))));
    }
    
    // ========================================================================================== Phone pos and rotation
    public void SetCamera(Vector3 pos, Vector3 rot)
    {
        Emit("CameraUpdate", new JSONObject(JsonUtility.ToJson( new JsonCameraUpdate(pos, rot))));
    }
    
}

public class JsonRegister
{
    public string name;
    public int avatar;
    public JsonRegister(string name, int avatar)
    {
        this.name = name;
        this.avatar = avatar;
    }
}

public class JsonPosition
{
    public Vector3 pos;
    public JsonPosition(Vector3 transformposition)
    {
        pos.x = (Mathf.Round(transformposition.x * 100f) / 100f);
        pos.y = (Mathf.Round(transformposition.y * 100f) / 100f);
        pos.z = (Mathf.Round(transformposition.z * 100f) / 100f);
    }
}

public class JsonAreaUpdate 
{
    public string area;
    public string state;
    public JsonAreaUpdate(string area, string state)
    {
        this.area = area;
        this.state = state;
    }
}

public class JsonCameraUpdate
{
    public Vector3 pos, rot;
    public JsonCameraUpdate(Vector3 pos, Vector3 rot)
    {
        this.pos.x = (Mathf.Round(pos.x * 100f) / 100f);
        this.pos.y = (Mathf.Round(pos.y * 100f) / 100f);
        this.pos.z = (Mathf.Round(pos.z * 100f) / 100f);
        this.rot.x = (Mathf.Round(rot.x * 100f) / 100f);
        this.rot.y = (Mathf.Round(rot.y * 100f) / 100f);
        this.rot.z = (Mathf.Round(rot.z * 100f) / 100f);
    }
}