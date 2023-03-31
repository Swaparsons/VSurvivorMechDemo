using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public GameObject levelUpMenu;
    public Player_Move pMove;
    public Player_Manager pMan;

    public void Start()
    {
        levelUpMenu.gameObject.SetActive(false);
    }
    public void startLevelUp()
    {
        levelUpMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void levelUpChoice(string choice)
    {
        levelUpMenu.gameObject.SetActive(false);
        if(choice == "Health")
        {
            pMove.playerMaxHp += 5f;
            pMove.playerCurrentHP += (Mathf.Abs(pMove.playerMaxHp - pMove.playerCurrentHP) / 2);
            pMove.playerCurrentHP = Mathf.Round(pMove.playerCurrentHP * 1.0f);
            pMove.playerHpText.text = pMove.playerCurrentHP + "/" + pMove.playerMaxHp;

        }
        if (choice == "Speed")
        {
            pMove.playerSpeed++;
        }
        if (choice == "Radius")
        {
            pMan.GetComponent<CircleCollider2D>().radius += 0.1f;
        }
        Time.timeScale = 1f;
    }
}
