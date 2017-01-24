using UnityEngine.Networking;

public class LobbyPlayerTeamToggle : NetworkBehaviour
{
    public short teamId;
    public LobbyScore lobbyScore;

    void OnStartLocalPlayer()
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
