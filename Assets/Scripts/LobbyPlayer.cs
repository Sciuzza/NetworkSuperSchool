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

            this.playerName = playerNameComp.playerName;
            this.playerFace = playerNameComp.playerFace;
        }
    }

}
