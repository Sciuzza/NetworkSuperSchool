using UnityEngine.Networking;

public class LobbyScore: NetworkBehaviour
{
    [SyncVar]
    public short playerTeam = 0;

    void OnStartLocalPlayer()
    {
        var teamToggles = FindObjectsOfType<LobbyPlayerTeamToggle>();
        foreach(var toggl in teamToggles)
        {
            toggl.lobbyScore = this;
        }
    }

    [Command]
    public void CmdChangeTeam(short teamId)
    {
        this.playerTeam = teamId;
    }

}
