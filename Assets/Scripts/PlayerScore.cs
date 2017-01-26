using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar]
    public short playerPersonalScore = 0;
    [SyncVar]
    public short playerTeam;

    private GameController gc;

    override public void OnStartClient()
    {
        gc = FindObjectOfType<GameController>();
    }

    public override void OnStartServer()
    {
        gc = FindObjectOfType<GameController>();
        if (playerTeam == 0)
        {
            gc.m_RedTeamMembers.Add(playerControllerId);
        }
        else if (playerTeam == 1)
        {
            gc.m_BlueTeamMembers.Add(playerControllerId);
        }
    }

    public void ChangeScore (short value)
    {
        playerPersonalScore += value;
        gc.ChangeTeamScore(value, playerTeam);
	}
}
