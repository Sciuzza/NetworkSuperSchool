using UnityEngine;

public static class ColorController{

    public static Color GetColorForWeapon(int weaponindex)
    {
        Color color = Color.white;
        switch (weaponindex)
        {
            case 0: color = Color.red; break;
            case 1: color = Color.blue; break;
            case 2: color = Color.yellow; break;
            case 3: color = Color.cyan; break;
            case 4: color = Color.red; break;
            case 5: color = Color.red; break;
            case 6: color = Color.red; break;
            case 7: color = Color.red; break;
        }
        return color;
    }
}