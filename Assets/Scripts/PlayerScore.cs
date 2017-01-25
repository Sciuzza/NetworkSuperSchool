using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar]
    public short playerPersonalScore = 0;
    [SyncVar]
    public short playerTeam;
    [SyncVar]
    public string playerName;

    private GameController gc;

    override public void OnStartServer()
    {
        gc = FindObjectOfType<GameController>();
        playerName = GetComponent<PlayerName>().playerName;

        AssignToList();
    }

    void AssignToList()
    {
        if (playerTeam == 0)
        {
            gc.m_RedTeamMembers.Add(this);
        }
        else if (playerTeam == 1)
        {
            gc.m_BlueTeamMembers.Add(this);       
        }
        FindObjectOfType<ScorePanelUI>().InitializeScoreUI();
    }

    [Server]
	public void ChangeScore (short value)
    {
        playerPersonalScore += value;
        gc.ChangeTeamScore(value, playerTeam);
	}
}
