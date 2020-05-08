using System.Collections.Generic;
using System.Linq;
using SocketIO;
using UnityEngine;

public class NetworkClient : SocketIOComponent
{
    // =============================================================================================== Private variables
    private string NETWORKID;
    
    // ================================================================================================ public variables
    public GameObject playerPrefab;
    
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
    
    // =================================================================================================== Set up Events
    private void SetupEvents()
    {
        // RECIEVING
        On("open", (E) => 
        {
            print("Connection made to server ^^");
        });
        On("register", (E) => // ------------ when registered on the server 
        {
            Register(E);
        }); 
        On("activePlayers", (E) =>
        {
            ActivePlayers(E);
        });
        On("updatePosition", (E) =>
        {
            UpdatePosition(E);
        });
        On("disconnected", (E) =>
        {
            Disconnect(E);
        });
    }

    private void Register(SocketIOEvent E)
    {
        NETWORKID = RemoveQuotes(E.data["id"].ToString());   // get ID
        GameObject playa = Instantiate(playerPrefab);              // spawn player
        playa.name = "Client Player: " + NETWORKID;                // set player name
        playa.GetComponent<Player>().SetID(NETWORKID);             // set ID in player
        PlayerCollection.ActivePlayers.Add(NETWORKID, playa);      // add the player to the player collection
    }

    private void ActivePlayers(SocketIOEvent E)
    {
        var otherPlayerID = RemoveQuotes(E.data["id"].ToString()); // get other players ID
        GameObject otherPlayer = Instantiate(playerPrefab);                    // spawn player
        var pos = new Vector3(                                                 // get its position
            float.Parse(E.data["x"].ToString()), 
            float.Parse(E.data["y"].ToString()), 
            float.Parse(E.data["z"].ToString()));
        otherPlayer.transform.position = pos;                                  // set the position
        otherPlayer.name = "Other Player: " + otherPlayerID;                   // set name
        otherPlayer.GetComponent<BehaviourDisabler>().Disable();               // disable stuff like movement
        PlayerCollection.ActivePlayers.Add(otherPlayerID, otherPlayer);        // set player in the database
    }

    private void UpdatePosition(SocketIOEvent E)
    {
        var ID = RemoveQuotes(E.data["id"].ToString());
        var pos = new Vector3(
            float.Parse(E.data["x"].ToString()),
            float.Parse(E.data["y"].ToString()),
            float.Parse(E.data["z"].ToString()));
        PlayerCollection.ActivePlayers[ID].transform.position = pos;
    }

    private void Disconnect(SocketIOEvent E)
    {
        PlayerCollection.RemovePlayer(RemoveQuotes(E.data["id"].ToString()));
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
    
    // ============================================================================================ Update pos on server
    private JsonPosition jsonPosition = new JsonPosition();
    public void SetPosition(Vector3 pos)
    {
        jsonPosition.SetPos(pos);
        Emit("updatePosition", new JSONObject(JsonUtility.ToJson(jsonPosition)));
    }
}

public class jsonName
{
    public jsonName(string name)
    {
        this.name = name;
    }
    public string name;
}

public class JsonPosition
{
    public Vector3 pos;
    public void SetPos(Vector3 transformposition)
    {
        
        pos.x = (Mathf.Round(transformposition.x * 1000f) / 1000f);
        pos.y = (Mathf.Round(transformposition.y * 1000f) / 1000f);
        pos.z = (Mathf.Round(transformposition.z * 1000f) / 1000f);
    }
}