using UnityEngine.Networking;

public class LobbyPlayerTeamToggle : NetworkBehaviour
{
    public short teamId;
    public LobbyScore lobbyScore;

    public override void OnStartLocalPlayer()
    {
        this.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener(UpdateTeam);
    }

    void UpdateTeam(bool on)
    {
        if (on)
        {
            if (isLocalPlayer)
            {
                lobbyScore.CmdChangeTeam(teamId);
            }
        }
    }
}
