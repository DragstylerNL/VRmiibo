using System.Collections.Generic;
using System.Linq;
using SocketIO;
using UnityEngine;

public class NetworkClient : SocketIOComponent
{
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
        if (Input.GetKeyDown(KeyCode.Space)) ShoutName();
    }
    
    // ========================================================================================================== Update
    private void SetupEvents()
    {
        // RECIEVING
        On("open", (E) => 
        {
            print("Connection made to server ^^");
        });
        On("register", (E) => 
        {
            print(RemoveQuotes(E.data["id"].ToString()));
        }); 
        
        
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

    private void ShoutName()
    {
        jsonFile js = new jsonFile();
        Emit("updateLogin", new JSONObject(JsonUtility.ToJson(js)));
    }
}

public class jsonFile
{
    public string name = "MyName";
}