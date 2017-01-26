using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerWeaponUse : NetworkBehaviour
{
    public Transform weaponTr;
    private GameObject weapon;

    private const int NUMBER_OF_WEAPONS = 7;

    private AbstractWeapon[] weapons;


    // Synched on the server to clients
    // -1 means infinite
    // 0 cannot shoot
    // >0 can shoot
    public SyncListInt ammoList = new SyncListInt();

    [SyncVar(hook = "OnWeaponChange")]
    public int selectedWeaponIndex = 0;
    

    public void OnWeaponChange(int weaponIndex)
    {
        selectedWeaponIndex = weaponIndex;
        MeshRenderer meshWeapon = weapon.GetComponent<MeshRenderer>();
        meshWeapon.material.color = ColorController.GetColorForWeapon(selectedWeaponIndex);
    }

    public int CurrentWeaponAmmo
    {
        get { return ammoList[selectedWeaponIndex]; }
    }

    public void Awake()
    {
        //this.weapons = GetComponentsInChildren<AbstractWeapon>();
        weapons = new AbstractWeapon[NUMBER_OF_WEAPONS];
        weapons[0] = this.gameObject.AddComponent<BasicWeapon>();
        //weapons[1] = this.gameObject.AddComponent<ShotgunWeapon>();
        //weapons[2] = this.gameObject.AddComponent<FlamethrowerWeapons>();
        weapons[3] = this.gameObject.AddComponent<GrenadeLauncherWeapon>();
        weapons[4] = this.gameObject.AddComponent<BoomerangWeapon>();
        weapons[5] = this.gameObject.AddComponent<DucklingWeapon>();
        weapons[6] = this.gameObject.AddComponent<RocketWeapon>();

        foreach(var weapon in weapons)
        {
            if (weapon != null) weapon.weaponTr = weaponTr;
        }
    }

    public override void OnStartLocalPlayer()
    {
        weapon = transform.Find("Head/Cube").gameObject;
        FindObjectOfType<AmmoUI>().pwu = this;
    }

    public override void OnStartServer()
    {
        // Initialise the ammo list
        for (int i = 0; i < NUMBER_OF_WEAPONS; i++)
        {
            ammoList.Add(0);
        }

        // Setup initial ammo
        ServerRefillAmmo();
    }


    public void ServerAddAmmo(int weaponIndex, int ammoAmount)
    {
        if (!isServer)
            return;

        ammoList[weaponIndex] += ammoAmount;
    }

    public void ServerRefillAmmo()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
                ammoList[i] = weapons[i].StartingAmmo;
        }
    }

    [Command]
    public void CmdWeaponChange(int weaponIndex)
    {
        if ((ammoList[weaponIndex] > 0) || ammoList[weaponIndex] <0)
        {
            selectedWeaponIndex = weaponIndex;
        }
    }



    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CmdShoot(weaponTr.position + weaponTr.forward * 100);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CmdWeaponChange(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CmdWeaponChange(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CmdWeaponChange(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CmdWeaponChange(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                CmdWeaponChange(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                CmdWeaponChange(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                CmdWeaponChange(6);
            }
        }
    }
    [Command]
    public void CmdShoot(Vector3 targetPosition)
    {
        // Check if we can shoot
        bool canShoot = false;
        if (ammoList[selectedWeaponIndex] == -1)
        {
            canShoot = true;
        }
        else if (ammoList[selectedWeaponIndex] > 0)
        {
            ammoList[selectedWeaponIndex]--;
            canShoot = true;
        }

        // If we can shoot, shoot
        if (canShoot)
        {
            weapons[selectedWeaponIndex].Shoot(targetPosition);
        }
    }







}