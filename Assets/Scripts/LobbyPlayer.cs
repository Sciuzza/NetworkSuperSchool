using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkLobbyPlayer
{
    [SyncVar]
    public string playerName = "New player";

    [SyncVar]
    public int teamId = 0;

    [SyncVar]
    public Color color = Color.white;

    [SyncVar]
    public string playerFace = "o_o";


    public PlayerName playerNameComp;

    public void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (this.readyToBegin)
                {
                    SendNotReadyToBeginMessage();
                }
                else
                {
                    SendReadyToBeginMessage();
                }
                //this.readyToBegin = !readyToBegin;
            }

            this.playerName = playerNameComp.name;

            if (Input.GetKeyDown(KeyCode.C))
            {
                color = Color.green;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                teamId = teamId + 1;
                if (teamId == 2) teamId = 0;
            }

        }
    }

}
