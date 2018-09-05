using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemy : MonoBehaviour {

    float moveSpeed = 4.00f;
    float maxSpeed = 9.00f;
    public int direction;
    int waitTime;
    Rigidbody2D rigid;
    Vector2 movement = new Vector2(1, 0);

    void Start()
    {
        waitTime = 100;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Camina IA
    void FixedUpdate()
    {
        if (waitTime <= 0)
        {
            rigid.AddForce(movement * moveSpeed * direction);
            rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, maxSpeed * -1, maxSpeed), rigid.velocity.y);
        }
        else
        {
            waitTime -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            waitTime = 20;
            rigid.velocity = new Vector2();
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tongue")
        {
            transform.parent = collision.gameObject.transform;
            transform.position = transform.parent.transform.position;
            transform.parent.gameObject.GetComponent<Tongue>().caught = true;
            transform.parent.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(rigid);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }
}
