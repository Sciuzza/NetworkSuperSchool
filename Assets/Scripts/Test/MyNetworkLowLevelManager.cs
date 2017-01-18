using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ChatMessage : MessageBase
{
    public string playerName;
    public string message;
}

public class MyNetworkLowLevelManager : MonoBehaviour {

    const short MSG_TYPE_SEND_CHAT_TO_SERVER = 200;
    const short MSG_TYPE_BROADCAST_CHAT_TO_CLIENTS = 201;

    public bool beServer = false;

    NetworkClient client;

    void Awake ()
    {
        if (beServer)
        {
            // Start a server on a given port
            NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnect);
            NetworkServer.RegisterHandler(MSG_TYPE_SEND_CHAT_TO_SERVER, OnServerChatMessageReceived);
            NetworkServer.Listen(7777);
            ConnectionConfig config = new ConnectionConfig();
            config.AddChannel(QosType.Reliable);
            NetworkServer.Configure(config, 40);
        }
        else
        {
            // Start a client and connect to the server
            client = new NetworkClient();
            client.RegisterHandler(MsgType.Connect, OnClientConnect);
            client.RegisterHandler(MSG_TYPE_BROADCAST_CHAT_TO_CLIENTS, OnClientChatMessageReceived);
            client.Connect("192.168.65.65", 7777);
        }
    }

    void OnServerChatMessageReceived(NetworkMessage msg)
    {
       // Debug.Log("SERVER: msg: " + msg.conn.address + ":" + msg.reader.ReadString());

        // Chat message arrived from a client
        ChatMessage receivedMessage = msg.ReadMessage<ChatMessage>();

        // Filter message ...
        receivedMessage.message = receivedMessage.message.Replace("gatto", "****");

        // Broadcast message to all clients
        NetworkServer.SendToAll(MSG_TYPE_BROADCAST_CHAT_TO_CLIENTS, receivedMessage);

    }

    void OnClientChatMessageReceived(NetworkMessage msg)
    {
        //Debug.Log("CLIENT: msg: " + msg.conn.address + ":" + msg.reader.ReadString());

        // We receive a chat message from the server (actually from a player)
        ChatMessage receivedMessage = msg.ReadMessage<ChatMessage>();

        Debug.Log(receivedMessage.playerName + ":\"" + receivedMessage.message +"\"");
    }



    void OnServerConnect(NetworkMessage msg)
    {
        Debug.Log("SERVER: connection - " + msg.channelId + " " + msg.conn.address);
    }

    void OnClientConnect(NetworkMessage msg)
    {
        Debug.Log("CLIENT: connection - " + msg.channelId + " " + msg.conn.address);
    }


    void Update()
    {
        // Send a message via chat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChatMessage newChatMessage = new ChatMessage();
            newChatMessage.playerName = "THE CORE TRAINER";
            newChatMessage.message = "Ciao a tutti";
            client.Send(MSG_TYPE_SEND_CHAT_TO_SERVER, newChatMessage);
        }
    }

}
