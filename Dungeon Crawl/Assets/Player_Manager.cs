using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public GameManager gm;
    public GameObject shop;
    private bool canShop;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            gm.coinManager();
            Destroy(other.gameObject);
        }
        if(other.CompareTag("xpOrb"))
        {
            gm.xpManager(other.GetComponent<PickupControl>().value);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Shop"))
        {
            Debug.Log("shop");
            shop.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
