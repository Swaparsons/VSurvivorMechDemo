using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Move : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;

    public float playerMaxHp;
    public float playerCurrentHP;
    public Text playerHpText;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHP = playerMaxHp;
        rb = GetComponent<Rigidbody2D>();
        playerHpText.text = playerCurrentHP + "/" + playerMaxHp;
    }
    // Update is called once per frame
    void Update()
    {
        //player move
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(directionX, directionY).normalized;

        //character face mouse pos
        if(Time.timeScale == 1)
        {
            faceMouse();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
    }

    public void faceMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x,mousePos.y - transform.position.y);

        transform.up = direction;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerCurrentHP--;
            playerHpText.text = playerCurrentHP + "/" + playerMaxHp;
            if(playerCurrentHP <= 0)
            {
                Debug.Log("dead");
            }
        }
    }
}
