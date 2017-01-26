using UnityEngine.Networking;

public class LobbyScore: NetworkBehaviour
{
    [SyncVar]
    public short playerTeam = 0;

    public override void OnStartLocalPlayer()
    {
        var teamToggles = FindObjectsOfType<LobbyPlayerTeamToggle>();
        foreach(var toggl in teamToggles)
        {
            toggl.lobbyScore = this;
        }
    }

    void Update()
    {
        GetComponent<PlayerColorer>().SetPlayerTeamColor(playerTeam);
    }

    [Command]
    public void CmdChangeTeam(short teamId)
    {
        UnityEngine.Debug.Log("TEAM: " + teamId);
        this.playerTeam = teamId;
    }

}
