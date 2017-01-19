using UnityEngine;
using UnityEngine.Networking;

public class MyLobbyNetworkManager : NetworkLobbyManager
{

    // we need to override this to avoid the manager changing scene automatically
    public override void OnLobbyServerPlayersReady()
    {
        Debug.Log("ALL READY");
    }

    void OnGUI()
    {
        // Check if everyone is ready
        bool allReady = true;
        for (int i = 0; i < lobbySlots.Length; i++)
        {
            if (!lobbySlots[i]) continue;
            if (!lobbySlots[i].readyToBegin)
            {
                allReady = false;
                break;
            }
        }

        if (allReady)
        {
            if (GUILayout.Button("START"))
            {
                ServerChangeScene(playScene);
            }
        }
    }

}
