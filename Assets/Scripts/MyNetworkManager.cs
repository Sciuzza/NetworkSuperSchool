using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{
    public void Awake()
    {
        // Start as a server
        StartServer();

        // Start as a client
        //networkAddress = "192.168.65.65";
        //StartClient();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("SERVER: got connection " + conn.connectionId + " " + conn.address);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("CLIENT: completed connection " + conn.connectionId + " " + conn.address);
    }

}
