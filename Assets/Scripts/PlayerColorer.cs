using UnityEngine;
using UnityEngine.Networking;

public class PlayerColorer : NetworkBehaviour
{
    public MeshRenderer bodyMr;
    public MeshRenderer headMr;

    public void SetPlayerTeamColor(short teamId)
    {
        SetPlayerColor(ColorController.GetColorForTeam(teamId));
    }

    public void SetPlayerColor(Color col)
    {
        bodyMr.material.color = col;
        headMr.material.color = col;
    }
}