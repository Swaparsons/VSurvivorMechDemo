using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float spawnTime;
    private bool canSpawn = false;
    public GameObject player;
    public GameObject enemy;
    public Text xpText,levelText;
    float level, xp, xpNeeded, coins, timer;
    public Player_Move pMove; 
    public Player_Manager pManager;
    public LevelUpManager lum;
    
    // Start is called before the first frame update
    void Start()
    {
        xpNeeded = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            canSpawn = true;
            timer = 0;
        }

        if (canSpawn == true)
        {
            spawn();
            canSpawn = false;
        }
    }
    public void coinManager()
    {
        coins++;
    }
    public void xpManager(float xpValue)
    {
        xp = xp + xpValue;
        if(xp == xpNeeded)
        {
            xp = 0;
            xpNeeded = xpNeeded + (5 * xpValue);
            levelUP();
        }
        xpText.text = xp + "/" + xpNeeded;
    }
    void levelUP()
    {
        level++;
        levelText.text = "Level " + level;
        lum.startLevelUp();
    }
    void spawn()
    {
        Vector3 spawnPoint = new Vector3(0,0,0);
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            spawnPoint.x = player.transform.position.x + 11;
        }

        if (rand == 1)
        {
            spawnPoint.x = player.transform.position.x - 11;
        }
        spawnPoint.y = Random.Range(-5.4f, 5.4f);

        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }
}
