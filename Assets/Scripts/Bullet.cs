using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int direction;
    Rigidbody2D rigid;
    int speed;
    GameObject[] enemies;
    GameObject[] playerBullet;
    GameObject[] enemyBullet;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyBullet = GameObject.FindGameObjectsWithTag("Bullet");
        speed = 7;
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enem in enemies)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enem.GetComponent<Collider2D>());
        }
        enemyBullet = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bul in enemyBullet)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bul.GetComponent<Collider2D>());
        }
        rigid.velocity = new Vector2(speed*direction, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Ramp")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
