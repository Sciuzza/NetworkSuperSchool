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

	public void ChangeScore (short value)
    {
        playerPersonalScore += value;
        gc.ChangeTeamScore(value, playerTeam);
	}
}
