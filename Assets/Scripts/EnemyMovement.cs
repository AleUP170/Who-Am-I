using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    float moveSpeed = 20.00f;
    float maxSpeed = 0.50f;
    int direction;
    int dispar;
    int isShooting;
    Rigidbody2D rigid;
    GameObject bullet;
    Vector2 movement = new Vector2(1, 0);

    void Start () {
        isShooting = 0;
        direction = 1;
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Escupe IA
	void FixedUpdate () {
        dispar = Random.Range(0, 256);
        print(dispar);
        if (dispar == 20 && isShooting == 0)
        {
            //isShooting = Escupir();
        }
        if (isShooting > 0)
        {
            rigid.velocity = new Vector2();
            isShooting -= 1;
        }
        else
        {
            rigid.AddForce(movement * moveSpeed * direction);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, maxSpeed * -1, maxSpeed), rigid.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            rigid.velocity = new Vector2();
            direction *= -1;
        }
    }

    /* Disparo
    int Escupir()
    {
        bullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), transform.position, Quaternion.identity);
        return (50);
    }
    */
}
