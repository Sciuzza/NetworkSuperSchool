using UnityEngine;

public static class ColorController{

    public static Color GetColorForTeam(int teamIndex)
    {
        Color color = Color.white;
        switch (teamIndex)
        {
            case 0: color = Color.red; break;
            case 1: color = Color.blue; break;
            case 2: color = Color.yellow; break;
            case 3: color = Color.green; break;
        }
        return color;
    }

    public static Color GetColorForWeapon(int weaponindex)
    {
        Color color = Color.white;
        switch (weaponindex)
        {
            case 0: color = Color.white; break;
            case 1: color = Color.blue; break;
            case 2: color = Color.yellow; break;
            case 3: color = Color.green; break;
            case 4: color = Color.cyan; break;
            case 5: color = Color.magenta; break;
            case 6: color = Color.red; break;
            case 7: color = Color.black; break;
        }
        return color;
    }
}