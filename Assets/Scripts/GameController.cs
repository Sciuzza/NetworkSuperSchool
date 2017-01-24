using UnityEngine;
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
	public SyncListInt m_TotalTeamScoreList;

    public List<PlayerScore> m_RedTeamMembers, m_BlueTeamMembers;

    #endregion

    public PlayerScore GetPlayerScoreById(short playerId)
    {
        return m_RedTeamMembers.Find(x => x.playerControllerId == playerId);       
    }

    #region GAME_CONTROLLER_MONO_BEHAVIOUR_METHODS
    [Server] //This is executed only by server
	private void Awake () {
        this.m_RedTeamMembers = new List<PlayerScore>();
        this.m_BlueTeamMembers = new List<PlayerScore>();
        this.m_TotalTeamScoreList = new SyncListInt ();
        InitializeTeams(m_NumberOfTeams);

	}
	#endregion


	#region METHODS
	[Server] //This is executed only by server
	public void InitializeTeams (int teams) {

		int i = -1;


		if ((teams > 1) && (teams < 5))
			this.m_NumberOfTeams = teams - 1;
		
		else {
			
			Debug.LogError (this.ToString () + ": Game Controller cannot set the right number of teams (i. e.: from 2 to 4)!");
			return;

		}


		if (this.m_TotalTeamScoreList != null) {
			
			do {
				
				this.m_TotalTeamScoreList.Add (0);

			} while (++i < this.m_NumberOfTeams);

		} else {

			Debug.LogWarning (this.ToString () + ": Game Controller has forced the initialization of the <<TotalTeamScoreList>>");

			this.m_TotalTeamScoreList = new SyncListInt();

			do {

				this.m_TotalTeamScoreList.Add (0);

			} while (++i < this.m_NumberOfTeams);

		}

	}



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