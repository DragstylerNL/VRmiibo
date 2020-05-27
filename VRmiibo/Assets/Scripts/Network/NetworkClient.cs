using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SocketIO;
using UnityEngine;

public class NetworkClient : SocketIOComponent
{
    // ========================================================================================================== Events 
    public Action<string, int, int> updateMinigameUI = delegate(string area, int inarea, int ready){};
    
    // =============================================================================================== Private variables
    private Enums.Areas _currentArea = Enums.Areas.hub;
    private bool _hubHasSpawned = false;
    private Transform _hub;

    // ================================================================================================ public variables
    public GameObject playerPrefab;
    public string NETWORKID;

    // =========================================================================================================== Start
    public override void Start()
    {
        base.Start();

        SetupEvents();
    }

    // ========================================================================================================== Update
    public override void Update()
    {
        base.Update();
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
    }

    IEnumerator ActivePlayers(SocketIOEvent E)
    {
        while (!_hubHasSpawned)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0);
        
        var otherPlayerID = RemoveQuotes(E.data["id"].ToString()); // get other players ID
        GameObject otherPlayer = Instantiate(playerPrefab, _hub);              // spawn player
        var pos = new Vector3(                                                 // get its position
            float.Parse(E.data["x"].ToString()),     // X
            float.Parse(E.data["y"].ToString()),     // Y
            float.Parse(E.data["z"].ToString()));    // Z
        otherPlayer.transform.position = pos;                                  // set the position
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
        PlayerCollection.ActivePlayers[ID].transform.position = pos;           // set the position
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
        yield return new WaitForSeconds(0);
        
        PlayerCollection.RemovePlayer(RemoveQuotes(E.data["id"].ToString())); // Remove disconnected player
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
        print("registering");
    }
    
    // ============================================================================================ Update pos on server
    private JsonPosition jsonPosition = new JsonPosition();
    public void SetPosition(Vector3 pos)
    {
        jsonPosition.SetPos(pos);
        Emit("updatePosition", new JSONObject(JsonUtility.ToJson(jsonPosition)));
    }
   
    // ================================================================================================== minigame areas
    public void SetMinigame(Enums.Areas area, Enums.areastate state)
    {
        _currentArea = state == Enums.areastate.exit? Enums.Areas.hub : area;
        Emit("gameZone", new JSONObject(JsonUtility.ToJson(new JsonAreaUpdate(area.ToString(), state.ToString()))));
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
    public void SetPos(Vector3 transformposition)
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