using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkLobbyPlayer
{

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
    }
}
