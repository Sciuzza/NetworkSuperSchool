using UnityEngine;
using UnityEngine.Networking;

public class PlayerName : NetworkBehaviour
{
    [SyncVar]
    public string playerName = "PIPPO";

    private TextMesh textMesh;

	void Start ()
    {
        textMesh = GetComponentInChildren<TextMesh>();
     
        if (isLocalPlayer)
        {
            var ifield = FindObjectOfType<UnityEngine.UI.InputField>();
            ifield.onEndEdit.AddListener(ChangeName);
        }
    }
	
    void Update()
    {
        textMesh.text = playerName;
    }

    public void ChangeName(string newName)
    {
        if (isLocalPlayer)
        {
            CmdChangeName(newName);
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        this.playerName = newName;
    }


}
