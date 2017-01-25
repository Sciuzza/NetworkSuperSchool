﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameController : NetworkBehaviour {

	#region GAME_CONTROLLER_PARAMETERS
	//0 = Team DeathMatch
	public int m_GameModality = 0;

	// 1 - 4
	private int m_NumberOfTeams = 2;

	//Number of Max Score for DeathMatch and Team DeathMatch modalities
	public int m_MaxDeathMatchScore = 40;

	//Number of teams, with total score inside each box
	public SyncListInt m_TotalTeamScoreList = new SyncListInt();
    
    public List<PlayerScore> m_RedTeamMembers, m_BlueTeamMembers;

    #endregion

    public PlayerScore GetPlayerScoreById(short playerId)
    {
        if (m_RedTeamMembers.Find(x => x.playerControllerId == playerId) != null)
        {
            return m_RedTeamMembers.Find(x => x.playerControllerId == playerId);
        }
        else if (m_BlueTeamMembers.Find(x => x.playerControllerId == playerId) != null)
        {
            return m_BlueTeamMembers.Find(x => x.playerControllerId == playerId);
        }
        return null;
    }

    #region GAME_CONTROLLER_MONO_BEHAVIOUR_METHODS
    [Server] //This is executed only by server
	public override void OnStartServer () {

        this.m_RedTeamMembers = new List<PlayerScore>();
        this.m_BlueTeamMembers = new List<PlayerScore>();
        this.m_TotalTeamScoreList.Add(0);
        this.m_TotalTeamScoreList.Add(0);

        foreach (var pScore in FindObjectsOfType<PlayerScore>())
        {
            if (pScore.playerTeam == 0)
            {
                m_RedTeamMembers.Add(pScore);
            }
            else if (pScore.playerTeam == 1)
            {
                m_BlueTeamMembers.Add(pScore);
            }
        }     
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
        FindObjectOfType<MyLobbyNetworkManager>().ServerReturnToLobby();
    }
	#endregion
}