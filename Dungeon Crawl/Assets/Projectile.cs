using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public ParticleSystem impact;
    public int damage;
    public bool canPen;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized;
        rb.velocity = rb.velocity* force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        transform.position = new Vector3(transform.position.x,transform.position.y, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impact, gameObject.transform.position, transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Instantiate(impact, gameObject.transform.position, Quaternion.identity);
        EnemyMove enemy = hitInfo.GetComponent<EnemyMove>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
