using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerState ps;
    public Text text;

	void Update ()
    {
        if (ps != null) text.text = ps.health.ToString();
	}

}
