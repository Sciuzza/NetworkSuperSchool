using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar]
    public short playerPersonalScore = 0;
    [SyncVar]
    public short playerTeam = 0;

    private GameController gc;

    [Server]
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        playerTeam = (short)Mathf.RoundToInt(Random.Range(0f, 1f));
        AssignToList();
    }

    [Server]
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
    }

	public void ChangeScore (short value)
    {
        playerPersonalScore += value;
        gc.ChangeTeamScore(value, playerTeam);
	}
}
