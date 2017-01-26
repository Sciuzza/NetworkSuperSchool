using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BoomerangAmmo : NetworkBehaviour
{
    bool hasHit;
    private Transform ammoTr;
    public Transform weaponTr;
    public short idPlayer;

    public IEnumerator Launch(GameObject go, Vector3 direction)
    {
        float timeTransition = 0;

        while ((weaponTr.position - weaponTr.position + (direction / 2)).magnitude > 0.01f)
        {
            timeTransition += Time.deltaTime;  // bullet speed

            go.transform.position = Vector3.Lerp(weaponTr.position, weaponTr.position + (direction/2) , timeTransition);
            yield return 0;
        }
 
        /*ammoTr = go.transform;
        timeTransition = 0;
        yield return new WaitForSeconds(1);

        while ((ammoTr.position - weaponTr.position).magnitude > 0.01f)
        {
            timeTransition += Time.deltaTime;

            go.transform.position = Vector3.Lerp(ammoTr.position, weaponTr.position, timeTransition);
            yield return 0;
        }*/
    }
}
