using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerState ps;
    public Text text;

	void Update ()
    {
        text.text = ps.health.ToString();
	}

}
