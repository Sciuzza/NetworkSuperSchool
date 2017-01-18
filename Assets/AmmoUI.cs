using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public PlayerWeaponUse pwu;
    public Text text;

	void Update ()
    {
        text.text = pwu.CurrentWeaponAmmo.ToString();
	}

}
