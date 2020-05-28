using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    // ================================================================================================ Public variables
    public static Dictionary<string, GameObject> ActivePlayers = new Dictionary<string, GameObject>();

    // ================================================================================================ Public variables
    public static void RemovePlayer(string ID)
    {
        Destroy(ActivePlayers[ID]);
        ActivePlayers.Remove(ID);
    }

    public static GameObject GetPlayer(string ID)
    {
        return ActivePlayers[ID];
    }
}
