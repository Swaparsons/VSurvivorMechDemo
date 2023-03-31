using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public GameObject bullet;
    public Player_Move pm;
    public Transform bulletOri;
    bool canFire;
    private float timer;
    public float timeBetweenFiring;

    public Transform equipPos;
    public GameObject currentWeapon;
    // rame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void newEquip(GameObject newGun)
    {
        currentWeapon.GetComponent<GunScript>().isEquipped = false;
        currentWeapon.GetComponent<Collider2D>().enabled = true;
        currentWeapon.gameObject.transform.parent = null;

        currentWeapon = newGun;
        currentWeapon.GetComponent<Collider2D>().enabled = false;
        currentWeapon.GetComponent<GunScript>().isEquipped = true;
        newGun.gameObject.transform.parent = equipPos.gameObject.transform;
        newGun.transform.position = equipPos.position;
        currentWeapon.transform.localRotation = Quaternion.Euler(0,0,0);
    }

}
