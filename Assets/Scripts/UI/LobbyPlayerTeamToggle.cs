using UnityEngine.Networking;

public class LobbyPlayerTeamToggle : NetworkBehaviour
{
    public short teamId;
    public LobbyScore lobbyScore;
    
    public void UpdateTeam(bool on)
    {
        if (on)
        {
            if (lobbyScore != null)
            {
                lobbyScore.ChangeTeam(teamId);
            }
        }
    }
}
