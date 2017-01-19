using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerScore: MonoBehaviour
{
    [SyncVar]
    public int playerPersonalScore = 0;
    public int playerTeam = 0;

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
