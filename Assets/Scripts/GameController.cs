using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameController : NetworkBehaviour {

	#region GAME_CONTROLLER_PARAMETERS
	//0 = Team DeathMatch
	public int m_GameModality = 0;

	// 1 - 4
	public int m_NumberOfTeams = 0;

	//Number of Max Score for DeathMatch and Team DeathMatch modalities
	public int m_MaxDeathMatchScore = 40;

	//Number of teams, with total score inside each box
	public List <int> m_TotalTeamScoreList;

    public List<PlayerScore> m_RedTeamMembers, m_BlueTeamMembers;

    #endregion


    #region GAME_CONTROLLER_MONO_BEHAVIOUR_METHODS
    [Server] //This is executed only by server
	private void Awake () {
        this.m_RedTeamMembers = new List<PlayerScore>();
        this.m_BlueTeamMembers = new List<PlayerScore>();
        this.m_TotalTeamScoreList = new List <int> ();

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

			this.m_TotalTeamScoreList = new List <int> ();

			do {

				this.m_TotalTeamScoreList.Add (0);

			} while (++i < this.m_NumberOfTeams);

		}

	}



	[Server] //This is executed only by server
	public void ChangeTeamScore (short score, short team) {

		/*for (int i = 0; i <= m_NumberOfTeams; i++) {
			
			if (i == team) {
				
				this.m_TotalTeamScoreList [i] += score;
				this.EndGameCheck ();
				return;
				
			}
			
		}

		Debug.LogError (this.ToString () + ": Game Controller cannot select a team while attempting specific total team score variation!");*/

		switch (team) {

		case 0:
			this.m_TotalTeamScoreList [0] += score;
			break;

		case 1:
			this.m_TotalTeamScoreList [1] += score;
			break;

		case 2:
			this.m_TotalTeamScoreList [2] += score;
			break;

		case 3:
			this.m_TotalTeamScoreList [3] += score;
			break;

		default:
			Debug.LogError (this.ToString () + ": Game Controller cannot select a team while attempting specific total team score variation!");
			break;

		}

		this.EndGameCheck ();

	}


	public void EndGameCheck () {



	}
	#endregion

}