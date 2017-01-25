using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FlameThrower : AbstractWeapon
{
    public BoxCollider areaFireDamage;
    public ParticleSystem fire;
    public int damageFire = 5;

    private void OnTriggerStay(Collider _other)
    {
        if (!isServer)
            return;

        PlayerState player = _other.gameObject.GetComponent<PlayerState>();

        if (player != null)
            player.ServerTakeDamage(damageFire, playerControllerId);
    }
    
    public override void OnStartServer()
    {
        Destroy(fire);
    }

    public override void Shoot(Vector3 targetPosition)
    {
        areaFireDamage.enabled = true;

        if (!isServer)
            RpcFireFeedback();
    }

    [ClientRpc]
    public void RpcFireFeedback()
    {
        StartCoroutine(BlazeCO(2));
    }

    private IEnumerator BlazeCO(float _seconds)
    {
        fire.Play();
        yield return new WaitForSeconds(_seconds);
        fire.Stop();
    }
}