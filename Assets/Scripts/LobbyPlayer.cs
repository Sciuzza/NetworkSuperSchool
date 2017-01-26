using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkLobbyPlayer
{
    [SyncVar]
    public string playerName = "New player";

    [SyncVar]
    public short teamId = 0;

    [SyncVar]
    public Color color = Color.white;

    [SyncVar]
    public string playerFace = "o_o";


    public PlayerName playerNameComp;
    public LobbyScore playerScoreComp;

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

        }

        this.playerName = playerNameComp.playerName;
        this.playerFace = playerNameComp.playerFace;
        this.teamId = playerScoreComp.playerTeam;
    }

}
