using UnityEngine.Networking;

public class LobbyScore : NetworkBehaviour
{
    [SyncVar]
    public short playerTeam = 0;

    public override void OnStartLocalPlayer()
    {
        var teamToggles = FindObjectsOfType<LobbyPlayerTeamToggle>();
        foreach(var toggl in teamToggles)
        {
            toggl.lobbyScore = this;
            toggl.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener(toggl.UpdateTeam);
        }

        CmdChangeTeam((short)UnityEngine.Random.Range(0, 1));
    }

    void Update()
    {
        GetComponent<PlayerColorer>().SetPlayerTeamColor(playerTeam);
    }

    public void ChangeTeam(short teamId)
    {
        CmdChangeTeam(teamId);
    }

    [Command]
    public void CmdChangeTeam(short teamId)
    {
        this.playerTeam = teamId;
    }

}
