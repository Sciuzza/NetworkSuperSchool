using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScore: NetworkBehaviour
{
    [SyncVar]
    public short playerPersonalScore = 0;
    [SyncVar]
    public short playerTeam = 0;

    private GameController gc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

	public void ChangeScore (short value)
    {
        playerPersonalScore += value;
        gc.ChangeTeamScore(value, playerTeam);
	}
}
