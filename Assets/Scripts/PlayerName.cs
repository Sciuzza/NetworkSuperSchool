using UnityEngine;
using UnityEngine.Networking;

public class PlayerName : NetworkBehaviour
{
    [SyncVar]
    public string playerName = "PIPPO";

    [SyncVar]
    public string playerFace = "O_O";

    public TextMesh textName;
    public TextMesh textFace;

    void Start ()
    {
        if (isLocalPlayer)
        {
            this.gameObject.name = playerName;

            var ifield = FindObjectOfType<UnityEngine.UI.InputField>();
            ifield.onEndEdit.AddListener(ChangeFace);
        }
    }
	
    void Update()
    {
        textName.text = playerName;
        textFace.text = playerFace;
    }

    public void ChangeName(string newName)
    {
        if (isLocalPlayer)
        {
            CmdChangeName(newName);
        }
    }

    public void ChangeFace(string newFace)
    {
        if (isLocalPlayer)
        {
            CmdChangeFace(newFace);
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        this.playerName = newName;
    }

    [Command]
    public void CmdChangeFace(string newFace)
    {
        this.playerFace = newFace;
    }

}
