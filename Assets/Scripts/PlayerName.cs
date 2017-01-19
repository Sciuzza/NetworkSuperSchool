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

            var nameField = FindObjectOfType<LobbyPlayerNameInputField>();
            if (nameField) nameField.GetComponent<UnityEngine.UI.InputField>().onEndEdit.AddListener(ChangeName);

            var faceField = FindObjectOfType<LobbyPlayerFaceInputField>();
            if (faceField) faceField.GetComponent<UnityEngine.UI.InputField>().onEndEdit.AddListener(ChangeFace);
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
