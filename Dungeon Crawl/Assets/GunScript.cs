using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    public Player_Combat pc;
    public Transform bulletOri;
    bool canFire;
    private float timer;
    public GameObject outline;
    public bool isEquipped;
    private bool isInRange;

    public int damage;
    public float fireRate;

    // rame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetButton("Fire1") && canFire && isEquipped)
        {
            canFire = false;
            Instantiate(bullet, bulletOri.position, Quaternion.identity);
            bullet.GetComponent<Projectile>().damage = damage;
        }
    }

    private void OnMouseOver()
    {
        if (!isEquipped && isInRange)
        {
            if (Input.GetButton("Fire1"))
            {
                pc.newEquip(this.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("in Range");
            isInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("out of Range");
            isInRange = false;
        }
    }
}
