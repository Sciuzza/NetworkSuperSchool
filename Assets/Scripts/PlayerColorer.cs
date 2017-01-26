using UnityEngine;
using UnityEngine.Networking;

public class PlayerColorer : NetworkBehaviour
{
    public MeshRenderer bodyMr;
    public MeshRenderer headMr;

    public void SetPlayerTeamColor(short teamId)
    {
        switch (teamId)
        {
            case 0:
                SetPlayerColor(Color.red);
                break;
            case 1:
                SetPlayerColor(Color.blue);
                break;
        }
    }

    public void SetPlayerColor(Color col)
    {
        bodyMr.material.color = col;
        headMr.material.color = col;
    }
}