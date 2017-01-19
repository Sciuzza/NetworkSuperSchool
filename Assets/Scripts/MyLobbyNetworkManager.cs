﻿using UnityEngine;
using UnityEngine.Networking;

public class MyLobbyNetworkManager : NetworkLobbyManager
{

    // we need to override this to avoid the manager changing scene automatically
    public override void OnLobbyServerPlayersReady()
    {
        Debug.Log("ALL READY");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        conn.playerControllers[0].gameObject.transform.position = Random.onUnitSphere * 4;
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


    // Called when switching from the lobby scene to the game scene
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        var lobbyScript = lobbyPlayer.GetComponent<LobbyPlayer>();
        var gameScript = gamePlayer.GetComponent<PlayerName>();
        gameScript.playerName = lobbyScript.playerName;
        gameScript.playerFace = lobbyScript.playerFace;
        Debug.Log("Set player name: " + lobbyScript.playerName);
        return true;
    }

}
