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

	//Number of teams, with total score inside each box
	public List <int> m_TotalTeamScoreList;
	#endregion


	#region GAME_CONTROLLER_MONO_BEHAVIOUR_METHODS
	[Server]
	private void Awake () {

		this.m_TotalTeamScoreList = new List <int> ();

	}
	#endregion


	#region METHODS
	public void ChangeTeamScore (short score, short team) {



	}


	//public void
	#endregion

}