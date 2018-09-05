using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour  {

    float moveSpeed = 20.00f;
    float maxSpeed = 0.50f;
    public int direction;
    int dispar;
    int isShooting;
    int cooldown;
    Rigidbody2D rigid;
    public GameObject bullet;
    GameObject newBullet;
    Vector2 movement = new Vector2(1, 0);
    public bool walks;

    void Start()
    {
        isShooting = 0;
        cooldown = 0;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Escupe IA
    void FixedUpdate()
    {
        dispar = Random.Range(0, 100);
        if (dispar == 20 && isShooting == 0 && cooldown <=0)
        {
            isShooting = Escupir();
            cooldown = 100;
        }
        if (isShooting > 0)
        {
            rigid.velocity = new Vector2();
            isShooting -= 1;
        }
        if (walks)
        {
            cooldown -= 1;
            rigid.AddForce(movement * moveSpeed * direction);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, maxSpeed * -1, maxSpeed), rigid.velocity.y);
        }
        else
        {
            cooldown -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            rigid.velocity = new Vector2();
            direction *= -1;
        }

        if (collision.gameObject.tag == "Tongue")
        {
            transform.parent = collision.gameObject.transform;
            transform.position = transform.parent.transform.position;
            transform.parent.gameObject.GetComponent<Tongue>().caught = true;
            Destroy(rigid);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }

    //Disparo
    int Escupir()
    {
        newBullet = Instantiate(bullet, transform.position + new Vector3(0.30f*direction,0,0),transform.rotation);
        newBullet.GetComponent<Bullet>().direction = direction;
        return (50);
    }
    
}
