using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(sendInterval = 0.5f)]
public class PlayerState : NetworkBehaviour
{
    [SyncVar]
    public int health = 100;

    private TextMesh textMesh;

	void Start ()
    {
        textMesh = GetComponentInChildren<TextMesh>();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }

        textMesh.text = health.ToString();
    }

    public void TakeDamage(int dmg)
    {
        if (!isServer)
            return;

        health -= dmg;
    }

}
