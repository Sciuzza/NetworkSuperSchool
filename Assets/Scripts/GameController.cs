using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameController : NetworkBehaviour
{

	#region GAME_CONTROLLER_PARAMETERS
	//0 = Team DeathMatch
	public int m_GameModality = 0;

	// 1 - 4
	private int m_NumberOfTeams = 2;

	//Number of Max Score for DeathMatch and Team DeathMatch modalities
	public int m_MaxDeathMatchScore = 40;

	//Number of teams, with total score inside each box
	public SyncListInt m_TotalTeamScoreList = new SyncListInt();
    public SyncListInt m_RedTeamMembers = new SyncListInt();
    public SyncListInt m_BlueTeamMembers = new SyncListInt();
    #endregion
    
    public GameObject GetPlayerGoById(int controllerId)
    {
        foreach (var connections in NetworkServer.connections)
        {
            if (connections.playerControllers[0].playerControllerId == controllerId)
            {
                return connections.playerControllers[0].gameObject;
            }
        }
        return null;
    }

    public PlayerScore GetPlayerScoreById(short playerId)
    {
        return GetPlayerGoById(playerId).GetComponent<PlayerScore>();
    }
     

    #region GAME_CONTROLLER_MONO_BEHAVIOUR_METHODS
    [Server] //This is executed only by server
	public override void OnStartServer () {

        this.m_TotalTeamScoreList.Add(0);
        this.m_TotalTeamScoreList.Add(0);      
    }
	#endregion


	#region METHODS

	[Server] //This is executed only by server
	public void ChangeTeamScore (short score, short team)
    {
		this.m_TotalTeamScoreList [team] += score;

		this.EndGameCheck ();
	}

	public void EndGameCheck () {

        foreach (var totScore in m_TotalTeamScoreList)
        {
            if (totScore >= m_MaxDeathMatchScore)
            {
                ReturnToLobby();
            }
        }
	}

    public void ReturnToLobby()
    {
        mlRef.ServerReturnToLobby();
    }
	#endregion
}